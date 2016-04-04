using System;
using Xamarin.Forms;

namespace RoomiesCalc
{
	public class BalanceView : ContentPage
	{
		public BalanceView ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}

