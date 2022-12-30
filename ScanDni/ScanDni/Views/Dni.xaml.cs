using MarcTron.Plugin;
using ScanDni.Models;
using ScanDni.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScanDni
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Dni : ContentPage
	{
		Item dni;
        readonly Page pageBack;
		public Dni (Item datos, Page page)
		{
			InitializeComponent ();
			pageBack = page;
			ArmarDatos(datos);
			BindingContext = this;

#if DEBUG
			AdMob.AdsId = "ca-app-pub-3940256099942544/6300978111";
#else
             AdMob.AdsId = "tu-key-de-admob";
#endif

			CrossMTAdmob.Current.OnInterstitialLoaded += (s, args) =>
            {
				CrossMTAdmob.Current.ShowInterstitial();
            };
		}

		private async void ArmarDatos(Item datos) {
            try
            {
				dni = datos;
				SetDatos();

				if (dni.Id == 0) //vino por el QR
				{
					var itemBD = App.Database.ExistsDniAsync(dni);
					if (itemBD == null && App.guardado)
					{
						Guardar();
						BtGuadar.IsVisible = false;
						BtEliminar.IsVisible = true;
					}
					else if (itemBD != null)
					{
						dni.Id = itemBD.Id;
						Actualizar();
						BtGuadar.IsVisible = false;
						BtEliminar.IsVisible = true;
					}
					else 
					{
						BtGuadar.IsVisible = true;
						BtEliminar.IsVisible = false;
					}
				}
				else //vino por el archivo
				{
					BtGuadar.IsVisible = false;
					BtEliminar.IsVisible = true;
				}
			}
            catch (Exception)
            {
				await DisplayAlert("oops!", "Ocurrio un error inesperado", "OK");
            }
		}

		private void SetDatos() {
			LbDocumento.Text = dni.Numero;
			LbDocumentoBack.Text = dni.Numero;
			TxValor1.Text = dni.Apellido;
			TxValor2.Text = dni.Nombre;
            TxValor3.Text = dni.FechaNacimiento;
			Img.Source = dni.GeneroImg;
			TxValor1Back.Text = dni.Tramite;
			TxValor2Back.Text = dni.Ejemplar;
			TxValor3Back.Text = dni.FechaEmision;
		}

        private void BtVolver_Clicked(object sender, EventArgs e)
        {
			Application.Current.MainPage = pageBack;
		}

		protected override bool OnBackButtonPressed()
		{
			Application.Current.MainPage = pageBack;
			return true;
		}

		private void BtGuadar_Clicked(object sender, EventArgs e)
        {
			Guardar();
			BtGuadar.IsVisible = false;
			BtEliminar.IsVisible = true;
		}

		private async void Guardar() 
		{
			var itemBD = App.Database.ExistsDniAsync(dni);
			if (itemBD == null)
				dni.Id = await App.Database.SaveDniAsync(dni);

            _ = this.DisplaySnackBarAsync(Funciones.Toast("Guardado!", false));
		}

		private async void Actualizar()
		{
			await App.Database.UpdateDniAsync(dni);
            _ = this.DisplaySnackBarAsync(Funciones.Toast("Actualizado!", false));
		}

		private async void BtCompartir_Clicked(object sender, EventArgs e)
        {
			await Share.RequestAsync(new ShareTextRequest
			{
				Text = FormatearDatosDni(),
				Title = "Dni"
			});
		}

        private async void BtCopiar_Clicked(object sender, EventArgs e)
        {
			await Clipboard.SetTextAsync(FormatearDatosDni());
            _ = this.DisplaySnackBarAsync(Funciones.Toast("Copiado!", false));
		}

		private string FormatearDatosDni() {

			string formato = "Dni: {1}{0}Apellido: {2}{0}Nombre: {3}{0}Género: {4}{0}Fecha Nacimiento: {5}";
			string mensaje = string.Format(formato, Environment.NewLine, dni.Numero, dni.Apellido, dni.Nombre, dni.GeneroDesc, dni.FechaNacimiento);

			if (App.copia) {
				formato = mensaje + "{0}Trámite N°: {1}{0}Ejemplar: {2}{0}Fecha Emisión: {3}";
				mensaje = string.Format(formato, Environment.NewLine, dni.Tramite, dni.Ejemplar, dni.FechaEmision);
			}
			return mensaje;
		}

		private async void BtEliminar_Clicked(object sender, EventArgs e)
        {
            await App.Database.DeleteDniAsync(dni);
            _ = this.DisplaySnackBarAsync(Funciones.Toast("Eliminado!", false));
            dni.Id = 0;
			BtGuadar.IsVisible = true;
            BtEliminar.IsVisible = false;
        }

        private void BtEscanear_Clicked(object sender, EventArgs e)
        {
			Application.Current.MainPage = new ScanQr();
		}

        private void swipeDni_Clicked(object sender, EventArgs e)
        {
			SwipeFront();
		}

        private void swipeDniBack_Clicked(object sender, EventArgs e)
        {
			SwipeBack();
		}

        private void BtSwipe_Clicked(object sender, EventArgs e)
        {
			if (!swipeGralBack.IsVisible)
				SwipeFront();
			else
				SwipeBack();
		}

		private async void SwipeFront() 
		{
			FrameDniBack.RotationY = -270;
			await FrameDni.RotateYTo(-90, 500, Easing.SpringIn);
			swipeGralFront.IsVisible = false;
			swipeGralBack.IsVisible = true;
			await FrameDniBack.RotateYTo(-360, 500, Easing.SpringOut);
			FrameDniBack.RotationY = 0;
		}

		private async void SwipeBack()
		{
			FrameDni.RotationY = -270;
			await FrameDniBack.RotateYTo(-90, 500, Easing.SpringIn);
			swipeGralBack.IsVisible = false;
			swipeGralFront.IsVisible = true;
			await FrameDni.RotateYTo(-360, 500, Easing.SpringOut);
			FrameDni.RotationY = 0;
		}

        private void BtAd_Clicked(object sender, EventArgs e)
        {
#if DEBUG
			CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/1033173712");
#else
            CrossMTAdmob.Current.LoadInterstitial("tu-key-de-admob");
#endif
		}

		private void AdMob_AdsLoaded(object sender, EventArgs e)
        {
			LblAdMob.Text = "algo";
		}
    }
}