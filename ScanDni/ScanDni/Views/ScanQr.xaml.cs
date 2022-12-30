using ScanDni.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScanDni
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanQr : ContentPage
	{
        bool showPopUp;
        public ScanQr ()
		{
			InitializeComponent ();
            HelpPopUp();
        }

        private void ScanView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread( () =>
            {
                if (App.vibrar) {
                    try
                    {
                        Vibration.Vibrate();
                    }
                    catch (Exception) {}
                }

                ArmarDatos(result.Text);
            });
        }

        private async void ArmarDatos(string datos)
        {
            try
            {
                var text = datos.Split(char.Parse("@"));

                Item dni = new Item();
                dni.Tramite = text[0];
                dni.Apellido = text[1];
                dni.Nombre = text[2];
                dni.Genero = text[3];
                dni.Numero = text[4];
                dni.Ejemplar = text[5];
                dni.FechaNacimiento = text[6];
                dni.FechaEmision = text[7];

                if (dni.Genero == "F")
                {
                    dni.GeneroDesc = "Femenino";
                    dni.GeneroImg = "woman.png";
                }
                else if (dni.Genero == "M")
                {
                    dni.GeneroDesc = "Masculino";
                    dni.GeneroImg = "men.png";
                }
                else
                {
                    dni.GeneroDesc = "No Binario";
                    dni.GeneroImg = "nobinario.png";
                }

                Application.Current.MainPage = new Dni(dni, this);
            }
            catch (Exception)
            {
                await this.DisplaySnackBarAsync(Funciones.Toast("oops! Algo no salió bien", true));
            }
        }

        private void FlashlightBt_Clicked(object sender, EventArgs e)
        {
            try
            {
                scanView.ToggleTorch();
                FlashlightBt.TextColor = scanView.IsTorchOn ? Color.Yellow : Color.White;
            }
            catch (FeatureNotSupportedException)
            {
                _ = this.DisplaySnackBarAsync(Funciones.Toast("Linterna no soportada", true));
            }
            catch (PermissionException)
            {
                _ = this.DisplaySnackBarAsync(Funciones.Toast("No tenemos permisos", true));
            }
            catch (Exception)
            {
                _ = this.DisplaySnackBarAsync(Funciones.Toast("oops! Algo no salió bien", true));
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

        private async void HelpPopUp()
        {
            showPopUp = true;
            await Task.Delay(12000);
            if (showPopUp)
                _ = await Navigation.ShowPopupAsync(new Views.HelpScan());
        }

        protected override void OnDisappearing()
        {
            showPopUp = false;
        }

        private async void HelpBt_Clicked(object sender, EventArgs e)
        {
            await Navigation.ShowPopupAsync(new Views.HelpScan());
        }
    }
}