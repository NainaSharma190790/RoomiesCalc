using System;

using Xamarin.Forms;

namespace RoomiesCalc
{
	public class Group :BaseModel
	{
		private string _GroupName = string.Empty;
		public  string GroupName
		{
			get { return _GroupName; }
			set { _GroupName = value; OnPropertyChanged("GroupName"); }
		}

		private int _GroupID;
		public int GroupID
		{
			get { return _GroupID; }
			set { _GroupID = value; OnPropertyChanged("GroupID"); }
		}
	}

}