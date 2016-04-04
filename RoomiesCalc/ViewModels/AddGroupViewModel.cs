using System;

using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace RoomiesCalc
{
	public class AddPlaceViewModel:BaseViewModel
	{
		private INavigation _navigation; // HERE

		public AddPlaceViewModel() 
		{
			_places = new Place();
			_placeList = new ObservableCollection<Place>();


		}

		private ObservableCollection<Place> _placeList;

		public ObservableCollection<Place> PlaceList
		{
			get { return _placeList; }
			set
			{
				_placeList = value;
				OnPropertyChanged();
			}
		}
		private Place _places;

		public Place Places
		{
			get { return _places; }
			set
			{
				_places = value;
				OnPropertyChanged();
			}
		}
		#region Load and add group  Command
		private Command _addPlaceCommand;

		public Command AddPlaceCommand
		{
			get
			{
				return _addPlaceCommand ?? (_addPlaceCommand = new Command(async (param) => await ExecuteAddPlaceCommand(param)));
			}
		}

		private async Task ExecuteAddPlaceCommand(object param)
		{
			try
			{
				int i= App.Database.SaveItem<Place>(Places);
			}
			catch (Exception ex)
			{
				Console.WriteLine("An Exception Occured During Save Record {0}", ex.Message);
				return;
			}
		}


		private Command _LoadAllPlaces;

		public Command LoadAllPlaces
		{
			get
			{
				return _LoadAllPlaces ?? (_LoadAllPlaces = new Command(async () => await ExecuteLoadCommand()));
			}
		}

		private async Task ExecuteLoadCommand()
		{
			try
			{

				_placeList = App.Database.GetItems<Place>(); //From Local DB

			}
			catch (Exception ex)
			{
				Console.WriteLine("An Exception Occured During Save Record {0}", ex.Message);
			}
		}

		#endregion
	}
}


