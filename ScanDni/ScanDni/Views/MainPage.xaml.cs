using ScanDni.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ScanDni
{
    public partial class MainPage : ContentPage
    {
        bool mostrarPrivacidad = true;
        public MainPage()
        {
            InitializeComponent();

            App.vibrar = Preferences.Get("vibrar", false);
            App.guardado = Preferences.Get("guardado", false);
            App.copia = Preferences.Get("copia", false);
            mostrarPrivacidad = Preferences.Get("privacidad", true);

            LbEscanear.Text = "Scan";

#if DEBUG
            AdMob.AdsId = "ca-app-pub-3940256099942544/6300978111";
#else
            AdMob.AdsId = "tu-key-de-admob";
#endif
        }

        protected override async void OnAppearing()
        {
            await Task.Delay(100);
            LbEscanear.Text = "Scan";
            animationView.WidthRequest = 400;
            animationView.HeightRequest = 400;
            animationView.IsVisible = true;

            if (mostrarPrivacidad)
                _ = await Navigation.ShowPopupAsync(new Privacidad());
        }

        private void QrBt_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ScanQr();
        }

        private void DniBt_Clicked(object sender, EventArgs e)
        {
#if DEBUG
            //Hack para insert sin scan
            Models.Item dni = new Models.Item
            {
                Numero = "12345676",
                Nombre = "ALGUIEN",
                Apellido = "PERSONA",
                FechaNacimiento = "01/01/1990",
                Genero = "M",
                GeneroDesc = "Masculino",
                GeneroImg = "men.png",
                Ejemplar = "B",
                FechaEmision = "01/01/2010",
                Tramite = "00123456"
            };
            Application.Current.MainPage = new Dni(dni, this);
#else
            Application.Current.MainPage = new ScanQr();
#endif
        }

        private void ArchivoBt_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Archivo();
        }

        private void AjustesBt_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Ajustes();
        }

        private void AdMob_AdsLoaded(object sender, EventArgs e)
        {
            LblAdMob.Text = "algo";
        }
    }
}
