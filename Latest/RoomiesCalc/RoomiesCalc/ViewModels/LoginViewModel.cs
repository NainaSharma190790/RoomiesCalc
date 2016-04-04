using Microsoft.WindowsAzure.MobileServices;
using RoomiesCalc.App;
using RoomiesCalc.Helper;
using RoomiesCalc.Interfaces;
using RoomiesCalc.Models;
using RoomiesCalc.Services;
using RoomiesCalc.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoginViewModel))]
namespace RoomiesCalc.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Properties

        IAuthentication _authenticator = DependencyService.Get<IAuthentication>();
        string _authenticationStatus;

        public string AuthenticationStatus
        {
            get
            {
                return _authenticationStatus;
            }
            set
            {
                SetPropertyChanged(ref _authenticationStatus, value);
            }
        }

        internal GoogleUserProfile AuthUserProfile
        {
            get;
            set;
        }

        #endregion

        #region Authenticate User

        /// <summary>
        /// Performs a complete authentication pass
        /// </summary>
        public async Task<bool> AuthenticateCompletely()
        {
            await AuthenticateWithGoogle();

            if (AuthUserProfile != null)
                await AuthenticateWithBackend();

            return RoomieApp.CurrentRoomie != null;
        }

        #endregion

        #region Auth With Google

        /// <summary>
		/// Attempts to get the user's profile and will use WebView form to authenticate if necessary
		/// </summary>
		async Task<bool> AuthenticateWithGoogle()
        {
            await ShowGoogleAuthenticationView();

            if (RoomieApp.AuthToken == null)
                return false;

            await GetUserProfile();
            return AuthUserProfile != null;
        }

        #endregion

        #region Show Google Auth View

        /// <summary>
		/// Shows the Google authentication web view so the user can authenticate
		/// </summary>
		async Task ShowGoogleAuthenticationView()
        {
            if (RoomieApp.AuthToken != null && Settings.Instance.User != null)
            {
                var success = await GetUserProfile();

                if (success)
                {
                    AzureService.Instance.Client.CurrentUser = Settings.Instance.User;
                    return;
                }
            }

            try
            {
                AuthenticationStatus = "Loading...";
                MobileServiceUser user = await _authenticator.DisplayWebView();

                var identity = await AzureService.Instance.Client.InvokeApiAsync("getUserIdentity", null, HttpMethod.Get, null);

                RoomieApp.AuthToken = identity.Value<string>("accessToken");
                //Utility.SetSecured("AuthToken", App.AuthToken, "vdoers.roomie", "authentication");

                Settings.Instance.User = user;
                await Settings.Instance.Save();

                if (RoomieApp.CurrentRoomie != null && RoomieApp.CurrentRoomie.Id != null)
                {
                    var task = AzureService.Instance.SaveRoomie(RoomieApp.CurrentRoomie);
                    await RunSafe(task);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("**ROOMIE AUTHENTICATION ERROR**\n\n" + e.GetBaseException());
               // InsightsManager.Report(e);
            }
        }

        #endregion

        #region Auth With backend

        /// <summary>
		/// Authenticates the athlete against the Azure backend and loads all necessary data to begin the app experience
		/// </summary>
		async Task<bool> AuthenticateWithBackend()
        {
            Roomie roomie;
            using (new Busy(this))
            {
                AuthenticationStatus = "Getting athlete's profile";
                roomie = await GetAthletesProfile();

                if (roomie == null)
                {
                    //Unable to get athlete - try registering as a new athlete
                    roomie = await RegisterAthlete(AuthUserProfile);
                }
                else
                {
                    roomie.ProfileImageUrl = AuthUserProfile.Picture;

                    if (roomie.IsDirty)
                    {
                        var task = AzureService.Instance.SaveRoomie(roomie);
                        await RunSafe(task);
                    }
                }

                Settings.Instance.RoomieID = roomie?.Id;
                await Settings.Instance.Save();

                if (RoomieApp.CurrentRoomie != null)
                {
                    //await GetAllLeaderboards();
                    RoomieApp.CurrentRoomie.IsDirty = false;
                    MessagingCenter.Send<LoginViewModel>(this, Messages.UserAuthenticated);
                }

                AuthenticationStatus = "Done";
                return RoomieApp.CurrentRoomie != null;
            }
        }

        #endregion

        #region Get Roomie Profile

        /// <summary>
		/// Gets the roomie's profile from the Azure backend
		/// </summary>
		async Task<Roomie> GetAthletesProfile()
        {
            Roomie roomie = null;

            //Let's try to load based on email address
            if (roomie == null && AuthUserProfile != null && !AuthUserProfile.Email.IsEmpty())
            {
                var task = AzureService.Instance.GetAthleteByEmail(AuthUserProfile.Email);
                await RunSafe(task);

                if (task.IsCompleted && !task.IsFaulted)
                    roomie = task.Result;
            }

            return roomie;
        }

        #endregion

        #region Register Roomie

        /// <summary>
		/// Registers an athlete with the backend and returns the new roomie profile
		/// </summary>
		async Task<Roomie> RegisterAthlete(GoogleUserProfile profile)
        {
            AuthenticationStatus = "Registering Roomie";
            var roomie = new Roomie(profile);

            var task = AzureService.Instance.SaveRoomie(roomie);
            await RunSafe(task);

            if (task.IsCompleted && task.IsFaulted)
                return null;

            "You're now an registered as roomie!".ToToast();
            return roomie;
        }

        #endregion

        #region Get User Profile

        /// <summary>
		/// Attempts to get the user profile from Google. Will use the refresh token if the auth token has expired
		/// </summary>
		async public Task<bool> GetUserProfile()
        {
            //Can't get profile w/out a token
            if (RoomieApp.AuthToken == null)
                return false;

            if (AuthUserProfile != null)
                return true;

            using (new Busy(this))
            {
                AuthenticationStatus = "Getting Google user profile";
                var task = GoogleApiService.Instance.GetUserProfile();
                await RunSafe(task, false);

                if (task.IsFaulted && task.IsCompleted)
                {

                }

                if (task.IsCompleted && !task.IsFaulted && task.Result != null)
                {
                    AuthenticationStatus = "Authentication complete";
                    AuthUserProfile = task.Result;

                    //InsightsManager.Identify(AuthUserProfile.Email, new Dictionary<string, string> {
                    //    {
                    //        "Name",
                    //        AuthUserProfile.Name
                    //    }
                    //});

                    Settings.Instance.AuthUserID = AuthUserProfile.Id;
                    await Settings.Instance.Save();
                }
                else
                {
                    AuthenticationStatus = "Unable to authenticate";
                }
            }

            return AuthUserProfile != null;
        }

        #endregion

        #region LogOut

        public void LogOut(bool clearCookies)
        {
           // Utility.SetSecured("AuthToken", string.Empty, "xamarin.sport", "authentication");
            AzureService.Instance.Client.Logout();

            RoomieApp.AuthToken = null;
            AuthUserProfile = null;
            Settings.Instance.RoomieID = null;
            Settings.Instance.AuthUserID = null;

            if (clearCookies)
            {
                Settings.Instance.RegistrationComplete = false;
                _authenticator.ClearCookies();
            }

            Settings.Instance.Save();
        }

        #endregion
    }
}
