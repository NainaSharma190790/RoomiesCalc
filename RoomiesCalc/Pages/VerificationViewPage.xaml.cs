using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RoomiesCalc
{
	public partial class VerificationViewPage : ContentPage
	{
		private LoginViewModel ViewModel
		{
			get { return BindingContext as LoginViewModel; } //Type cast BindingContex as HomeViewModel to access binded properties
		}

		public VerificationViewPage ()
		{
			BindingContext = new LoginViewModel(this.Navigation);
			InitializeComponent ();
			BackgroundImage = "Bg1.png";
		}
	}
}

