using System;

using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace RoomiesCalc
{
	public class AddGroupViewModel:BaseViewModel
	{
		private INavigation _navigation; // HERE

		public AddGroupViewModel() 
		{

		}

		private ObservableCollection<Group> _groupList;

		public ObservableCollection<Group> GroupList
		{
			get { return _groupList; }
			set
			{
				_groupList = value;
				OnPropertyChanged();
			}
		}
		private Group _groups;

		public Group Groups
		{
			get { return _groups; }
			set
			{
				_groups = value;
				OnPropertyChanged();
			}
		}
		#region Load and add group  Command
		private Command _addGroupCommand;

		public Command AddGroupCommand
		{
			get
			{
				return _addGroupCommand ?? (_addGroupCommand = new Command(async (param) => await ExecuteAddGroupCommand(param)));
			}
		}
		Group G = new Group();

		private async Task ExecuteAddGroupCommand(object param)
		{
			try
			{
				G.GroupID = App.Database.SaveItem<Group>(Groups);
			}
			catch (Exception ex)
			{
				Console.WriteLine("An Exception Occured During Save Record {0}", ex.Message);
				return;
			}
		}


		private Command _LoadAllGroups;

		public Command LoadAllGroups
		{
			get
			{
				return _LoadAllGroups ?? (_LoadAllGroups = new Command(async () => await ExecuteLoadCommand()));
			}
		}

		private async Task ExecuteLoadCommand()
		{
			try
			{

				_groupList = App.Database.GetItems<Group>(); //From Local DB

			}
			catch (Exception ex)
			{
				Console.WriteLine("An Exception Occured During Save Record {0}", ex.Message);
			}
		}

		#endregion
	}
}


