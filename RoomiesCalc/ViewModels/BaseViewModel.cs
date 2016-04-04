using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace RoomiesCalc
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		private bool _isValid;

		public bool IsValid
		{
			get
			{
				return _isValid;
			}
			set {
				_isValid = value;
				OnPropertyChanged ();

			}
		}


		private string _title;

		public string Title
		{
			get { return _title; }
			set { _title = value; OnPropertyChanged(); }
		}


		#region Navigation

		public INavigation Navigation
		{
			get
			{
				var mainPage = Xamarin.Forms.Application.Current.MainPage;
				if (mainPage is NavigationPage)
				{
					return (INavigation)mainPage.Navigation;
				}
				return Xamarin.Forms.Application.Current.MainPage.Navigation;
			}
		}

		#endregion

		#region Property Changed Implementation

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName=null)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
	}
}