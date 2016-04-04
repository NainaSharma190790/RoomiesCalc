using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace RoomiesCalc.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();
			//Get width and hieght from current device
			App.ScreenHeight = (int)UIScreen.MainScreen.Bounds.Height;
	        App.ScreenWidth = (int)UIScreen.MainScreen.Bounds.Width;

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}

