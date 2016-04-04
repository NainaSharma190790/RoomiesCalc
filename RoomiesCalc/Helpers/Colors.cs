using System;

namespace RoomiesCalc
{
	public class Colors
	{
		public static readonly Colors RC_Pink = 0xFE2E64;//R254 G46 B100
		public static readonly Colors RC_Green = 0x8BC34A;//R139 G195 B74
		public double R, G, B;

		public static Colors FromHex(int hex)
		{
			Func<int, int> at = offset => (hex >> offset) & 0xFF;
			return new Colors
			{
				R = at(16) / 255.0,
				G = at(8) / 255.0,
				B = at(0) / 255.0
			};
		}

		public static implicit operator Colors(int hex)
		{
			return FromHex(hex);
		}

		public Xamarin.Forms.Color ToFormsColor()
		{
			return Xamarin.Forms.Color.FromRgb((int)(255 * R), (int)(255 * G), (int)(255 * B));
		}

		#if __ANDROID__
		public global::Android.Graphics.Color ToAndroidColor()
		{
			return global::Android.Graphics.Color.Rgb((int)(255 * R), (int)(255 * G), (int)(255 * B));
		}

		public static implicit operator global::Android.Graphics.Color(Colors color)
		{
			return color.ToAndroidColor();
		}
		#endif
	}
}
