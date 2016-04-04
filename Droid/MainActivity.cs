using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace RoomiesCalc.Droid
{
	[Activity (Label = "RoomiesCalc.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
			//Get width and hieght from current device
			int Width = Convert.ToInt32(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);
			int Height = Convert.ToInt16(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);
		    App.ScreenHeight = Height;
	        App.ScreenWidth = Width;

			LoadApplication (new App ());
		}
	}
}

