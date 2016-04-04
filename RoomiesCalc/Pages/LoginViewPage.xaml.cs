using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RoomiesCalc
{
	public partial class LoginViewPage : ContentPage
	{
		private LoginViewModel ViewModel
		{
			get { return BindingContext as LoginViewModel; } //Type cast BindingContex as HomeViewModel to access binded properties
		}


		public LoginViewPage ()
		{
			BindingContext = new LoginViewModel(this.Navigation);
			this.BackgroundImage = "Bg1.png";
			InitializeComponent ();
		}
	}
}

