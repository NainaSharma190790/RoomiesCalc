using RoomiesCalc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace RoomiesCalc
{
	public partial class DashboardPage : DashboardPageXaml
	{
		public DashboardPage ()
		{
			InitializeComponent ();
		}
	}
    public partial class DashboardPageXaml : BaseContentPage<DashboardViewModel>
    {
    }
}
