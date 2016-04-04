using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace RoomiesCalc.Pages
{
	public class ThemedNavigationPage : NavigationPage
	{
		public ThemedNavigationPage ()
		{
		}
        public ThemedNavigationPage(ContentPage root) : base(root)
        {
        }
    }
}
