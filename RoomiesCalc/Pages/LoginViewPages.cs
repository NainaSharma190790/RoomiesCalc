using System;
using Xamarin.Forms;

namespace RoomiesCalc
{
	public class LoginViewPage: BaseViewPage
	{
		#region All Fileds

		public RelativeLayout rltv_MainLayout;
		public Button btn_Login;
		public Image img_Backgroud,img_logo;
		public RCEntry txt_Name,txt_Nmuber;
		public Label lbl_Country;
		public StackLayout stack_Main;
		public BoxView bx_Name,bx_Number;
		private LoginViewModel ViewModel
		{
			get { return BindingContext as LoginViewModel; } //Type cast BindingContex as HomeViewModel to access binded properties
		}

		#endregion

		public LoginViewPage ()
		{
			BindingContext = new LoginViewModel(this.Navigation);

			bx_Name = new BoxView
			{
				WidthRequest=(w/4)*3,
				HeightRequest=1,
				BackgroundColor=Color.Gray,
			};

			bx_Number = new BoxView
			{
				WidthRequest=(w/4)*3,
				HeightRequest=1,
				BackgroundColor=Color.Gray,
			};

			img_Backgroud = new Image
			{
				Source="Bg1.png",
				HeightRequest=h,
				Aspect=	Aspect.Fill
			};

			img_logo = new Image
			{
				Source="Logo.png",
				Aspect=	Aspect.AspectFit,
				TranslationY=-w/10
			};
			txt_Name = new RCEntry
			{
				WidthRequest=w/2,
				HorizontalOptions=LayoutOptions.CenterAndExpand,
				TextColor=Color.White,
				Placeholder="FullName",
				BackgroundColor=Color.Transparent,
				TranslationX=10,
					
			};

			txt_Nmuber = new RCEntry
			{
				WidthRequest=w/2,
				HorizontalOptions=LayoutOptions.CenterAndExpand,
				TextColor=Color.White,
				Placeholder="MobileNumber",
				BackgroundColor=Color.Transparent
					
					
			};
			lbl_Country = new Label
			{
				Text="+91",
				TextColor=Color.White,
				//YAlign=TextAlignment.End,
				FontSize=22,
				TranslationY=5
			};
			btn_Login = new Button
			{
				Text="Login",
				TextColor=Color.White,
				BackgroundColor=Colors.RC_Green.ToFormsColor(),
				FontSize=22,
				WidthRequest=w/2,
				HorizontalOptions=LayoutOptions.CenterAndExpand,
				Command=ViewModel.LoginClick				
			};
			stack_Main = new StackLayout
			{ 
				HeightRequest=h/2,
				WidthRequest=(w/4)*3,
				HorizontalOptions=LayoutOptions.CenterAndExpand,
				VerticalOptions=LayoutOptions.CenterAndExpand,
				BackgroundColor=Color.Transparent,
				Spacing=w/20,
				Children=
				{
					img_logo,
					new StackLayout
					{
						Spacing=0,
						Orientation=StackOrientation.Vertical,
						HorizontalOptions=LayoutOptions.CenterAndExpand,
						Children=
						{txt_Name,bx_Name}
					},	
					new StackLayout
					{
						Spacing=0,
						Orientation=StackOrientation.Vertical,
						HorizontalOptions=LayoutOptions.CenterAndExpand,
						Children=
						{
							new StackLayout
							{
								Spacing=-3,
								Orientation=StackOrientation.Horizontal,
								HorizontalOptions=LayoutOptions.CenterAndExpand,
								Children=
								{lbl_Country,txt_Nmuber}
							},
							bx_Number					
						}
					},
					btn_Login
				}
			};

			rltv_MainLayout = new RelativeLayout
			{ 
				HeightRequest=h,WidthRequest=w,               
			};

			rltv_MainLayout.Children.Add(img_Backgroud, Constraint.Constant(0), Constraint.Constant(0));
			rltv_MainLayout.Children.Add(stack_Main, Constraint.Constant((w/4)/2), Constraint.Constant(h/6));           
			this. Content= rltv_MainLayout;

		}

	}
}

