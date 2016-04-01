using System;
using Xamarin.Forms;

namespace RoomiesCalc
{
	public class NotificationViewPage: ContentPage
	{
		public NotificationViewPage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}
