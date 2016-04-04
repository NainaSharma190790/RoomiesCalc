using RoomiesCalc.App;
using RoomiesCalc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace RoomiesCalc
{
	public partial class LoginPage : LoginPageXaml
    {
		public LoginPage ()
		{
			InitializeComponent ();
		}

        #region Attempt Login

        async public Task<bool> AttemptToAuthenticateAthlete(bool force = false)
        {
            await ViewModel.AuthenticateCompletely();

            if (RoomieApp.CurrentRoomie != null)
            {
                MessagingCenter.Send<RoomieApp>(RoomieApp.Current, Messages.AuthenticationComplete);
            }

            return RoomieApp.CurrentRoomie != null;
        }

        #endregion
    }
    public partial class LoginPageXaml : BaseContentPage<LoginViewModel>
    {
    }
}
