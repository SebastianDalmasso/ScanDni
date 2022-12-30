using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Airbnb.Lottie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanDni.Droid
{
    [Activity(Label = "DniScan",
        MainLauncher = true,
        Theme = "@style/MainTheme.Splash",
        NoHistory = true)]
    public class SplashActivity : Activity, Animator.IAnimatorListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Drawable.Splash);

            var animationView = FindViewById<LottieAnimationView>(Resource.Id.animation_view);
            animationView.AddAnimatorListener(this);

            // Create your application here
        }
        public void OnAnimationCancel(Animator animation)
        {
        }

        public void OnAnimationEnd(Animator animation)
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        public void OnAnimationRepeat(Animator animation)
        {
        }

        public void OnAnimationStart(Animator animation)
        {
        }

        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    SetContentView(Resource.Drawable.Splash);
        //    Task startupWork = new Task(() => { SimulateStartupAsync(); });
        //    startupWork.Start();
        //}

        //private async Task SimulateStartupAsync()
        //{
        //    await Task.Delay(1000);

        //    StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        //}
    }
}