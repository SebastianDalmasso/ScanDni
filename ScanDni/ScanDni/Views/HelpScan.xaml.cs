using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScanDni.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HelpScan : Popup
	{
		public HelpScan ()
		{
			InitializeComponent ();
		}

        private void BtOk_Clicked(object sender, EventArgs e)
        {
			Dismiss(null);
		}
    }
}