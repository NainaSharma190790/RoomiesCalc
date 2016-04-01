using System;

using Xamarin.Forms;

namespace RoomiesCalc
{
	public class Place :BaseModel
	{
		private string _PlaceName = string.Empty;
		public  string PlaceName
		{
			get { return _PlaceName; }
			set { _PlaceName = value; OnPropertyChanged("PlaceName"); }
		}

		private int _PlaceID;
		public int PlaceID
		{
			get { return _PlaceID; }
			set { _PlaceID = value; OnPropertyChanged("PlaceID"); }
		}
	}

}