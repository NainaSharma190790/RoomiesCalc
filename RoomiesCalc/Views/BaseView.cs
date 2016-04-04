using System;
using Xamarin.Forms;

namespace RoomiesCalc
{
	public class BaseView:ContentPage
	{
		#region All Fields
		public StackLayout stack_NavBar;
		public Image img_Back,img_Add,img_Share,img_Notification;
		public Label lbl_Tittle;
		public int h= App.ScreenHeight;
		public int w=App.ScreenWidth;

		#endregion
		public BaseView ()
		{
			img_Back = new Image
			{ 				
				Source="icon.png",	
				BackgroundColor=Color.Transparent,
				HorizontalOptions=LayoutOptions.Start,
				VerticalOptions=LayoutOptions.CenterAndExpand,
				Aspect=Aspect.AspectFit,
				TranslationX=10,
				#if __IOS__
				TranslationY=10		
				#endif

					
			};

			img_Add = new Image
			{ 
				Source="Add.png",
				BackgroundColor=Color.Transparent,
				HorizontalOptions=LayoutOptions.End,
				VerticalOptions=LayoutOptions.CenterAndExpand,
				Aspect=Aspect.AspectFit,
				TranslationX=-10,
				#if __IOS__
				TranslationY=10		
				#endif					
					
			};
			img_Share = new Image
			{ 
				Source="Share.png",
				BackgroundColor=Color.Transparent,
				HorizontalOptions=LayoutOptions.End,
				VerticalOptions=LayoutOptions.CenterAndExpand,
				Aspect=Aspect.AspectFit,
				TranslationX=-10,
				#if __IOS__
				TranslationY=10		
				#endif					

			};
			img_Notification = new Image
			{ 
				Source="Notification.png",
				BackgroundColor=Color.Transparent,
				HorizontalOptions=LayoutOptions.End,
				VerticalOptions=LayoutOptions.CenterAndExpand,
				Aspect=Aspect.AspectFit,
				TranslationX=-10,
				#if __IOS__
				TranslationY=10		
				#endif					

			};

			lbl_Tittle = new Label
			{ 
				HorizontalOptions=LayoutOptions.CenterAndExpand,
				VerticalOptions=LayoutOptions.Center,
				TextColor=Color.White,
				FontSize=22,
				XAlign = TextAlignment.Center,
				YAlign = TextAlignment.Center,
				#if __IOS__
				TranslationY=10		
				#endif

			};
			stack_NavBar = new StackLayout
			{ 
				BackgroundColor=Color.Gray,
				Orientation=StackOrientation.Horizontal,
				WidthRequest=w,
				Opacity=0.6,

				#if __IOS__
				HeightRequest=h/9,
				#endif
				#if __ANDROID__
				HeightRequest=h/11,
				#endif
				Children=
				{
					img_Back,lbl_Tittle,img_Notification,img_Add,img_Share
				}			
			};
		}
	}
}

