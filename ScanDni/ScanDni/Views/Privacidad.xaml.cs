using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScanDni.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Privacidad : Popup
    {
        public Privacidad()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("privacidad", false);
            Dismiss(null);
        }
    }
}