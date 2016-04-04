using System;
using Xamarin.Forms;

namespace RoomiesCalc
{
	public class BalanceViewPage : ContentPage
	{
		public BalanceViewPage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}

