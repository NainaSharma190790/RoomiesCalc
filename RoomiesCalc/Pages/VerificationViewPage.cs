using System;
using Xamarin.Forms;

namespace RoomiesCalc
{
	public class VerificationViewPage: BaseViewPage
	{
		#region All Fileds

		public RelativeLayout rltv_MainLayout;
		public Button btn_Verify;
		public Image img_Backgroud,img_logo;
		public Label lbl_Verify;
		public StackLayout stack_Main;
		private LoginViewModel ViewModel
		{
			get { return BindingContext as LoginViewModel; } //Type cast BindingContex as HomeViewModel to access binded properties
		}
		#endregion

		public VerificationViewPage ()
		{
			BindingContext = new LoginViewModel(this.Navigation);


			lbl_Verify = new Label
			{	
				TextColor=Colors.RC_Pink.ToFormsColor(),
				FontSize=22,
				Text="Enter your verfication code",
				FontAttributes=FontAttributes.Bold,
					
			};
			img_Backgroud = new Image
			{
				Source="Bg2.png",
				HeightRequest=h,
				Aspect=	Aspect.Fill
			};

			img_logo = new Image
			{
				Source="Logo.png",
				Aspect=	Aspect.AspectFit,
				TranslationY=-w/10
			};

			btn_Verify = new Button
			{
				Text="Verify",
				TextColor=Color.White,
				BackgroundColor=Colors.RC_Green.ToFormsColor(),
				FontSize=22,
				WidthRequest=w/2,
				HorizontalOptions=LayoutOptions.CenterAndExpand,
				Command=ViewModel.VerficationClick				
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
					lbl_Verify,
					new StackLayout
					{
						Orientation=StackOrientation.Horizontal,
						HorizontalOptions=LayoutOptions.CenterAndExpand,

						Children=
						{
							Verifytext("1"),
							Verifytext("2"),
							Verifytext("3"),
							Verifytext("4")}
						},	
					btn_Verify
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

		public StackLayout Verifytext(string number)
		{
			BoxView bx = new BoxView
			{
				WidthRequest=(w/4)*3,
				HeightRequest=1,
				BackgroundColor=Color.Gray,
			};


			RCEntry	txt = new RCEntry
			{
				WidthRequest=w/2,
				HorizontalOptions=LayoutOptions.CenterAndExpand,
				TextColor=Color.White,
				Text=number,
				BackgroundColor=Color.Transparent,

			};
			StackLayout stack =	new StackLayout {
				Spacing = 0,
				Orientation = StackOrientation.Vertical,
				WidthRequest=w/10,
				HeightRequest=h/10,
				Children =
				{ txt, bx }
			};
			return stack;
		}

	}
}

