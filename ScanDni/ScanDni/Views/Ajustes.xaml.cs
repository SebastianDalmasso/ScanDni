using MarcTron.Plugin;
using MarcTron.Plugin.CustomEventArgs;
using Plugin.StoreReview;
using ScanDni.Views;
using System;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScanDni
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Ajustes : ContentPage
	{
		public Ajustes ()
		{
			InitializeComponent ();

			SwVibrar.IsToggled = App.vibrar;
			SwGuardado.IsToggled = App.guardado;
            SwCopia.IsToggled = App.copia;

            CrossMTAdmob.Current.OnRewardedVideoAdLoaded += (s, args) =>
            {
                CrossMTAdmob.Current.ShowRewardedVideo();
            };
            //CrossMTAdmob.Current.OnRewardedVideoAdCompleted += Current_OnRewardedVideoAdCompleted;
            //CrossMTAdmob.Current.OnRewarded += Current_OnRewarded;
        }

        private void SwGuardado_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("guardado", SwGuardado.IsToggled);
        }

        private void SwVibrar_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("vibrar", SwVibrar.IsToggled);
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

        private void SwCopia_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("copia", SwCopia.IsToggled);
        }

        private async void BtCompartir_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = "Escanea los Dni con esta App! \n https://play.google.com/store/apps/details?id=com.kajustudio.scandni",
                Title = "DniScan"
            });
        }

        private async void BtCalificar_Clicked(object sender, EventArgs e)
        {
            //Launcher.OpenAsync(new Uri("market://details?id=com.KajuStudio.scandni"));
            await CrossStoreReview.Current.RequestReview(false);
        }

        private async void BtPrivacidad_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.ShowPopupAsync(new Privacidad());
        }

        private void BtAd_Clicked(object sender, EventArgs e)
        {
#if DEBUG
            CrossMTAdmob.Current.LoadRewardedVideo("ca-app-pub-3940256099942544/5224354917");
#else
            CrossMTAdmob.Current.LoadRewardedVideo("tu-key-de-admob");
#endif

        }

        //private void Current_OnRewardedVideoAdCompleted(object sender, EventArgs e)
        //{
        //    Console.WriteLine("OnRewardedVideoAdCompleted Seba");
        //}

        //private void Current_OnRewarded(object sender, MTEventArgs e)
        //{
        //    Console.WriteLine($"OnRewarded: {e.RewardType} - {e.RewardAmount}");
        //}
    }
}