using System;

using Xamarin.Forms;

namespace RoomiesCalc
{
	public class AddGroupView : BaseView
	{
		#region All Fileds
		public RelativeLayout rltv_MainLayout,rltv_PopUpLayout;
		public Button btn_Ok,btn_Cancel;
		public Image img_Backgroud,img_Group;
		public Label lbl_Grouptitle;
		public Entry txt_Grouptitle;
		public StackLayout stack_Popup,stack_Main,stack_PopupInside;
		public ListView list_Group;
		public Group GroupInfo=new Group(); 

		private AddGroupViewModel ViewModel
		{
			get { return new AddGroupViewModel(); } //Type cast BindingContex as AddGroupViewModel to access binded properties
		}
		#endregion

		public AddGroupView ()
		{
			BindingContext = new AddGroupViewModel();
		
			img_Backgroud = new Image
			{
				Source="Bg.png",
				HeightRequest=h,
				Aspect=	Aspect.Fill
			};
			img_Group = new Image
			{
				Source="Group.png",
			};
			btn_Ok =	new Button 
			{ 
				Text = "OK" ,
				Command=ViewModel.AddGroupCommand,
				HorizontalOptions=LayoutOptions.CenterAndExpand					
			};

			btn_Cancel =	new Button 
			{ 
				Image="Cross.png,",
				BackgroundColor=Color.Transparent
			};

			txt_Grouptitle = new Entry 
			{ 
				Placeholder = "Group Name" ,
				WidthRequest=(w / 4) * 2
			};

			lbl_Grouptitle = new Label 
			{ 
				Text = "Make your Group" ,
				FontSize=20
			};
			txt_Grouptitle.SetBinding(Entry.TextProperty,"G.GroupName");

			list_Group = new ListView
			{ 
				VerticalOptions=LayoutOptions.FillAndExpand,
				BackgroundColor=Color.Transparent
					//This one is list view color
				//BackgroundColor=Colors.RC_Green.ToFormsColor(),
			};

			rltv_PopUpLayout =	new RelativeLayout 
			{
				WidthRequest = (w / 4) * 3,//Get 75% of width
				HeightRequest = (w / 2),//Get 50% of width
				BackgroundColor = Colors.RC_Pink.ToFormsColor(),
				HorizontalOptions=LayoutOptions.CenterAndExpand,
				VerticalOptions=LayoutOptions.CenterAndExpand,
				TranslationY=-(h/5)

			
			};

			stack_PopupInside = new StackLayout
			{
				BackgroundColor=Color.Transparent,
				HorizontalOptions=LayoutOptions.CenterAndExpand,
				VerticalOptions=LayoutOptions.CenterAndExpand,
				Children = 
				{
					lbl_Grouptitle,
					new StackLayout
					{
						Orientation=StackOrientation.Horizontal,
						BackgroundColor=Color.Transparent,
						Children=
						{
							img_Group,
							txt_Grouptitle
						}
					},
					btn_Ok
				}
			};
			rltv_PopUpLayout.Children.Add (btn_Cancel, Constraint.Constant (w/2+(w/7)), Constraint.Constant (-w/10));
			rltv_PopUpLayout.Children.Add (stack_PopupInside, Constraint.Constant (w/20), Constraint.Constant (w/10));

			stack_Popup = new StackLayout
			{
				IsVisible=false,
				HeightRequest=h,
				WidthRequest=w,
				BackgroundColor=Color.Transparent,
				Children = 
				{
					rltv_PopUpLayout
								
				}				
			};
			list_Group.ItemTemplate = new DataTemplate (typeof (GroupCell));
			list_Group.ItemsSource = ViewModel.GroupList;


			btn_Ok.Clicked+= (object sender, EventArgs e) => 
			{
				stack_Popup.IsVisible=false;
			
			};
			btn_Cancel.Clicked+= (object sender, EventArgs e) => 
			{
				stack_Popup.IsVisible=false;
			};

			stack_Main = new StackLayout
			{ 
				BackgroundColor=Color.Transparent,
				Children = 
				{
					list_Group
				}
			};


			rltv_MainLayout = new RelativeLayout 
			{ 
				VerticalOptions= LayoutOptions.FillAndExpand,
				HorizontalOptions=LayoutOptions.FillAndExpand,
				WidthRequest=w,HeightRequest=h,
				BackgroundColor=Color.Pink
			};

			img_Share.IsVisible = false;
			lbl_Tittle.Text = "RoomiesCalc";

			var Sharetap = new TapGestureRecognizer(OnShareTapped);
			Sharetap.NumberOfTapsRequired = 1;
			img_Share.IsEnabled = true;
			img_Share.GestureRecognizers.Clear();
			img_Share.GestureRecognizers.Add(Sharetap);

			var Addtap = new TapGestureRecognizer(OnAddTapped);
			Addtap.NumberOfTapsRequired = 1;
			img_Add.IsEnabled = true;
			img_Add.GestureRecognizers.Clear();
			img_Add.GestureRecognizers.Add(Addtap);

			rltv_MainLayout.Children.Add (img_Backgroud, Constraint.Constant (0), Constraint.Constant (0));

			rltv_MainLayout.Children.Add (stack_NavBar, Constraint.Constant (0), Constraint.Constant (0));
			#if __IOS__

			rltv_MainLayout.Children.Add (stack_MainLayout, Constraint.Constant (0), Constraint.Constant ((h/9)));

			#endif

			#if __ANDROID__

			rltv_MainLayout.Children.Add (stack_Main, Constraint.Constant (0), Constraint.Constant ((h/11)));

			#endif
			rltv_MainLayout.Children.Add (stack_Popup, Constraint.Constant (0), Constraint.Constant (0));

			Content = rltv_MainLayout;
			
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			list_Group.ItemsSource = ViewModel.GroupList;
		}

		void OnAddTapped(View view, object sender)
		{
			stack_Popup.IsVisible=true;
		}

		void OnShareTapped(View view, object sender)
		{
			//Navigation.PushModalAsync(new NotificationView ());
		}


		#region Custom View cell
		/// <summary>
		/// This class is a ViewCell that will be displayed for each Group Cell.
		/// </summary>
		public class GroupCell : ViewCell
		{
			public GroupCell ()
			{
				var label = new Label
				{
					XAlign = TextAlignment.Center
				};
				label.SetBinding (Label.TextProperty, "Groups.GroupName");

				var layout = new StackLayout {
					Padding = new Thickness (20, 0, 0, 0),
					Orientation = StackOrientation.Horizontal,
					HorizontalOptions = LayoutOptions.StartAndExpand,
					Children = { label }
				};
				View = layout;
			}
		}
		#endregion
	}
}


