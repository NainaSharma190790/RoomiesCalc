using System;

using Xamarin.Forms;

namespace RoomiesCalc
{
	public class GroupCell : ViewCell
	{
		public GroupCell ()
		{
			var label = new Label
			{
				XAlign = TextAlignment.Center
			};
			label.SetBinding (Label.TextProperty, "Name");

			var layout = new StackLayout {
				Padding = new Thickness (20, 0, 0, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { label }
			};
			View = layout;
		}
	}
}


