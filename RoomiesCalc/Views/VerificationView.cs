using System;
using Xamarin.Forms;

namespace RoomiesCalc
{
	public class VerificationView: ContentPage
	{
		public VerificationView ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}
