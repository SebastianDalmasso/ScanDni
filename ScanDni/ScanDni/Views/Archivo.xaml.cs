using Newtonsoft.Json;
using ScanDni.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScanDni.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Archivo : ContentPage
	{
        public ICommand DeleteCommand { get; set; }
        public Archivo ()
		{
			InitializeComponent ();
            DeleteCommand = new Command<Item>((param) => { DeleteDni(param); });
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                Lista.ItemsSource = await App.Database.GetAllDniAsync();
            }
            catch (Exception)
            {
                Lista.ItemsSource = null;
            }
            
        }

        private void BackBt_Clicked(object sender, EventArgs e)
        {
			Application.Current.MainPage = new MainPage();
		}

        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new MainPage();
            return true;
        }

        private void Lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			var dni = e.CurrentSelection[0] as Item;
			Application.Current.MainPage = new Dni(dni, this);
		}

        private async void DeleteDni(Item itemDelete)
        {
            _ = await App.Database.DeleteDniAsync(itemDelete);

            SnackBarOptions options = Funciones.Toast("Eliminado!", false, 5);
            SnackBarActionOptions action = new SnackBarActionOptions
            {
                ForegroundColor = Color.White,
                BackgroundColor = Color.FromHex("#28B463"),
                Text = "DESHACER",
                Action = () =>
                {
                    return Deshacer(itemDelete);
                }
            };
            options.Actions = new[] { action };

            _ = this.DisplaySnackBarAsync(options);
            Lista.ItemsSource = await App.Database.GetAllDniAsync();
        }

        private async Task Deshacer(Item itemRestaurar)
        {
            _ = await App.Database.SaveDniAsync(itemRestaurar);
            Lista.ItemsSource = await App.Database.GetAllDniAsync();
        }

        private async void BtJson_Clicked(object sender, EventArgs e)
        {
            object result = await Navigation.ShowPopupAsync(new Compartir());

            if ((bool)result)
                _ = this.DisplaySnackBarAsync(Funciones.Toast("oops! Algo no salió bien", true));
        }
    }
}