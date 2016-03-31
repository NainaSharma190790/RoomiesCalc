using System;

using Xamarin.Forms;

namespace RoomiesCalc
{
	public class App : Application
	{
		#region All Fields 
		public static int ScreenHeight;// for device height
		public static int ScreenWidth;// for device width
		static RoomiesCalcDataBase database;

		#endregion
		public App ()
		{
			Database.CreateTables<Group> ();
			MainPage = new AddGroupView (); 
		}
		public static RoomiesCalcDataBase Database 
		{
			get 
			{ 
				if (database == null) 
				{
					database = new RoomiesCalcDataBase ();
				}
				return database; 
			}
		}

	protected override void OnStart ()
	{
		// Handle when your app starts
	}

	protected override void OnSleep ()
	{
		// Handle when your app sleeps
	}

	protected override void OnResume ()
	{
		// Handle when your app resumes
	}

	}
}



