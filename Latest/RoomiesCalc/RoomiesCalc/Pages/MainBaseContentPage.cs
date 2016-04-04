using RoomiesCalc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RoomiesCalc
{
	public class MainBaseContentPage : ContentPage
	{
        #region Private Fields

        bool _hasSubscribed;

        #endregion

        public MainBaseContentPage ()
		{
		}

        #region Public Properties

        public Color BarTextColor
        {
            get;
            set;
        }

        public Color BarBackgroundColor
        {
            get;
            set;
        }

        public bool HasInitialized
        {
            get;
            private set;
        }

        #endregion

        #region Apply Theme

        public void ApplyTheme(NavigationPage nav)
        {
            nav.BarBackgroundColor = BarBackgroundColor;
            nav.BarTextColor = BarTextColor;
        }

        #endregion

        #region Add Donw Button

        public void AddDoneButton(string text = "Done", ContentPage page = null)
        {
            var btnDone = new ToolbarItem
            {
                Text = text,
            };

            btnDone.Clicked += async (sender, e) =>
            await Navigation.PopModalAsync();

            page = page ?? this;
            page.ToolbarItems.Add(btnDone);
        }

        #endregion

        #region Track Page

        protected virtual void TrackPage(Dictionary<string, string> metadata)
        {
            var identifier = GetType().Name;
            //InsightsManager.Track(identifier, metadata);
        }

        #endregion

        #region Authenticate Stuff

        public async Task EnsureUserAuthenticated()
        {
            if (Navigation == null)
                throw new Exception("Navigation is null so unable to show auth form");

            var authPage = new LoginPage();
            await Navigation.PushModalAsync(authPage, true);

            await Task.Delay(300);
            var success = await authPage.AttemptToAuthenticateAthlete();

            if (success && Navigation.ModalStack.Count > 0)
            {
                await Navigation.PopModalAsync();
            }
        }

        async protected void LogoutUser()
        {
            var decline = await DisplayAlert("For ultra sure?", "Are you sure you want to log out?", "Yes", "No");

            if (!decline)
                return;

            var authViewModel = DependencyService.Get<LoginViewModel>();
            authViewModel.LogOut(true);

            // App.Current.StartRegistrationFlow();
        }

        #endregion
    }
}
