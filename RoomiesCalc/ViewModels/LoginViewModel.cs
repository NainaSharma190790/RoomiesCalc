using System;

using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RoomiesCalc
{
	public class LoginViewModel :BaseViewModel
	{
		private INavigation _navigation; // HERE

		public LoginViewModel(INavigation navigation)
		{
			_navigation = navigation;
		}

		#region Command for Login button

		public ICommand LoginClick
		{
			get
			{
				return new Command (async () =>
					{
						_navigation.PushModalAsync(new VerificationView ());
					}
				);
			}
		}

		#endregion

		#region Command for Verfication button

		public ICommand VerficationClick
		{
			get
			{
				return new Command (async () =>
					{
						_navigation.PushModalAsync(new AddGroupView ());
					}
				);
			}
		}

		#endregion
	}
}


