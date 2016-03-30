using System;

using Xamarin.Forms;

namespace RoomiesCalc
{
	public class AddGroupView : ContentPage
	{
		public Button btn_Add,btn_Ok,btn_Cancel;
		public Label lbl_Grouptitle;
		public Entry txt_Grouptitle;
		public StackLayout popup;
		public ListView list_Group;
		public Group GroupInfo=new Group(); 
		private AddGroupViewModel ViewModel
		{
			get { return new AddGroupViewModel(); } //Type cast BindingContex as HomeViewModel to access binded properties
		}
		public AddGroupView ()
		{
			BindingContext = new AddGroupViewModel();


			btn_Add =	new Button 
			{ 
				Text = "Add" 
			};
			btn_Ok =	new Button 
			{ 
				Text = "OK" 
			};
			btn_Cancel =	new Button 
			{ 
				Text = "Cancel" 
			};

			txt_Grouptitle = new Entry 
			{ 
				Placeholder = "Group Name" 
			};

			lbl_Grouptitle = new Label 
			{ 
				Text = "Make your Group" 
			};
			txt_Grouptitle.SetBinding(Entry.TextProperty,"Name");

			list_Group = new ListView
			{ 
				VerticalOptions=LayoutOptions.FillAndExpand,
				BackgroundColor=Color.Gray,
			};

			popup = new StackLayout
			{
				Spacing=10,
				HorizontalOptions=LayoutOptions.StartAndExpand,
				IsVisible=false,
				HeightRequest=200,
				WidthRequest=200,
				BackgroundColor=Color.Red,
				VerticalOptions=LayoutOptions.CenterAndExpand,
				Children = 
				{
					lbl_Grouptitle,
					txt_Grouptitle,
					btn_Ok,
					btn_Cancel
				}				
			};
			list_Group.ItemTemplate = new DataTemplate (typeof (GroupCell));

			btn_Add.Clicked+= (object sender, EventArgs e) => 
			{
				popup.IsVisible=true;
			};
			btn_Ok.Clicked+= (object sender, EventArgs e) => 
			{
				popup.IsVisible=false;
				btn_Ok.Command=ViewModel.AddGroupCommand;
				//btn_Ok.CommandParameter=(Event)BindingContext
			
			};
			btn_Cancel.Clicked+= (object sender, EventArgs e) => 
			{
				popup.IsVisible=false;
			};

			Content = new StackLayout
			{ 
				BackgroundColor=Color.Aqua,
				Children = 
				{
					btn_Add,popup,list_Group
				}
			};
			
		}
		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			list_Group.ItemsSource = ViewModel.GroupList;
		}		
	}
}


