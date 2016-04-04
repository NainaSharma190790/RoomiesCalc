using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Plugin.Connectivity;
using RoomiesCalc.Helper;
using RoomiesCalc.Interfaces;
using RoomiesCalc.Models;
using RoomiesCalc.Pages;
using RoomiesCalc.Services;
using RoomiesCalc.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace RoomiesCalc.App
{
	public partial class RoomieApp : Application
	{
        #region Properties

        public IHUDProvider _hud;
        public static int AnimationSpeed = 250;

        public static NotificationPayload PendingNotificationPayload
        {
            get;
            private set;
        }

        public IHUDProvider Hud
        {
            get
            {
                return _hud ?? (_hud = DependencyService.Get<IHUDProvider>());
            }
        }

        public new static RoomieApp Current
        {
            get
            {
                return (RoomieApp)Application.Current;
            }
        }

        public static Roomie CurrentRoomie
        {
            get
            {
                return Settings.Instance.RoomieID == null ? null : DataManager.Instance.Roomies.Get(Settings.Instance.RoomieID);
            }
        }

        public static bool IsNetworkRechable
        {
            get;
            set;
        }

        public static List<string> PraisePhrases
        {
            get;
            set;
        }

        public static List<string> AvailableLeagueColors
        {
            get;
            set;
        }

        public Dictionary<string, string> UsedLeagueColors
        {
            get;
            set;
        } = new Dictionary<string, string>();

        public static string AuthToken
        {
            get;
            set;
        }

        public static string AuthTokenAndType
        {
            get
            {
                return AuthToken == null ? null : "{0} {1}";//.Fmt("Bearer", AuthToken);
            }
        }

        #endregion

        public RoomieApp()
        {
            try
            {
                //App.AuthToken = Utility.GetSecured("AuthToken", "xamarin.sport", "authentication", null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            SetDefaultPropertyValues();

            InitializeComponent();
            MessagingCenter.Subscribe<BaseViewModel, Exception>(this, "ExceptionOccurred", OnAppExceptionOccurred);
            IsNetworkRechable = CrossConnectivity.Current.IsConnected;

            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                IsNetworkRechable = args.IsConnected;
            };

            if (Settings.Instance.RoomieID == null || !Settings.Instance.RegistrationComplete)
            {
                StartRegistrationFlow();
            }
            else
            {
                StartAuthenticationFlow();
            }
        }

        protected override void OnSleep()
        {
            //MessagingCenter.Unsubscribe<App, NotificationPayload>(this, Messages.IncomingPayloadReceivedInternal);
            base.OnSleep();
        }

        protected override void OnStart()
        {
            //MessagingCenter.Subscribe<App, NotificationPayload>(this, Messages.IncomingPayloadReceivedInternal, (sender, payload) => OnIncomingPayload(payload));
            base.OnStart();
        }

        void StartAuthenticationFlow()
        {
            //Create our entry page and add it to a NavigationPage, then apply a randomly assigned color theme
            var page = new LoginPage();
            var navPage = new ThemedNavigationPage(page);
            page.ApplyTheme(navPage);
            MainPage = navPage;

            page.EnsureUserAuthenticated();
        }

        internal void StartRegistrationFlow()
        {
            MainPage = new RegistrationPage(); //new WelcomeStartPage().WithinNavigationPage();
        }

        void OnAppExceptionOccurred(BaseViewModel viewModel, Exception exception)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (_hud != null)
                    {
                        _hud.Dismiss();
                    }

                    var msg = exception.Message;
                    var mse = exception as MobileServiceInvalidOperationException;

                    if (mse != null)
                    {
                        var body = await mse.Response.Content.ReadAsStringAsync();
                        var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(body);

                        if (dict != null && dict.ContainsKey("message"))
                            msg = dict["message"].ToString();
                    }

                    if (msg.Length > 300)
                        msg = msg.Substring(0, 300);

                    msg.ToToast(ToastNotificationType.Error, "Uh oh");
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            });
        }

        //internal async Task OnIncomingPayload(NotificationPayload payload)
        //{
        //	if(payload == null)
        //		return;

        //	if(App.CurrentAthlete == null)
        //	{
        //		PendingNotificationPayload = payload;
        //	}
        //	else
        //	{
        //		MessagingCenter.Send<App, NotificationPayload>(App.Current, Messages.IncomingPayloadReceived, payload);
        //	}
        //}

        //internal void ProcessPendingPayload()
        //{
        //	if(PendingNotificationPayload == null)
        //		return;

        //	MessagingCenter.Send<App, NotificationPayload>(App.Current, Messages.IncomingPayloadReceived, PendingNotificationPayload);
        //	PendingNotificationPayload = null;
        //}

        #region Theme

        /// <summary>
        /// Assigns a league a randomly-chosen theme from an existing finite list
        /// </summary>
        /// <returns>The theme.</returns>
        //public ColorTheme GetTheme(League league)
        //{
        //	if(league.Id == null)
        //		return null;

        //	league.Theme = null;
        //	var remaining = App.AvailableLeagueColors.Except(App.Current.UsedLeagueColors.Values).ToList();
        //	if(remaining.Count == 0)
        //		remaining.AddRange(App.AvailableLeagueColors);

        //	var random = new Random().Next(0, remaining.Count - 1);
        //	var color = remaining[random];

        //	if(App.Current.UsedLeagueColors.ContainsKey(league.Id))
        //	{
        //		color = App.Current.UsedLeagueColors[league.Id];
        //	}
        //	else
        //	{
        //		App.Current.UsedLeagueColors.Add(league.Id, color);
        //	}

        //	var theme = GetThemeFromColor(color);

        //	if(App.Current.Resources.ContainsKey("{0}Medium".Fmt(color)))
        //		theme.Medium = (Color)App.Current.Resources["{0}Medium".Fmt(color)];

        //	return theme;
        //}

        //public ColorTheme GetThemeFromColor(string color)
        //{
        //	return new ColorTheme {
        //		Primary = (Color)App.Current.Resources["{0}Primary".Fmt(color)],
        //		Light = (Color)App.Current.Resources["{0}Light".Fmt(color)],
        //		Dark = (Color)App.Current.Resources["{0}Dark".Fmt(color)],
        //	};
        //}

        void SetDefaultPropertyValues()
        {
            AvailableLeagueColors = new List<string> {
                    "red",
                    "green",
                    "blue",
                    "asphalt",
                    "yellow",
                    "purple"
            };

            PraisePhrases = new List<string> {
                    "sensational",
                    "crazmazing",
                    "stellar",
                    "ill",
                    "peachy keen",
                    "the bees knees",
                    "the cat's pajamas",
                    "the coolest kid in the cave",
                    "killer",
                    "aces",
                    "spiffy",
                    "wicked awesome",
                    "kinda terrific",
                    "top notch",
                    "impressive",
                    "legit",
                    "nifty",
                    "spectaculawesome",
                    "supernacular",
                    "bad to the bone",
                    "radical",
                    "neat",
                    "a hoss boss",
                    "mad chill",
                    "super chill",
                    "a beast",
                    "funky fresh",
                    "slammin it",
            };
        }

        #endregion
    }
}
