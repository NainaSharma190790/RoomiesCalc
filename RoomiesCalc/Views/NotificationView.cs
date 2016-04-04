using System;
using Xamarin.Forms;

namespace RoomiesCalc
{
	public class NotificationView: ContentPage
	{
		public NotificationView ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}
