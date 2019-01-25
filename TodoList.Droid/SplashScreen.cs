using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Java.Lang;
using MvvmCross.Platforms.Android.Views;

namespace TodoList.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    [IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.ActionView, Intent.CategoryDefault, Intent.CategoryBrowsable }, DataScheme = "@string/app_name", DataHost = "my_code_is_here")]
    public class SplashScreen : MvxSplashScreenActivity
    {
        ImageView myImage;
        Animation myAnimation;
        LinearLayout myLayout;
        TransitionDrawable transition;

        public SplashScreen() : base(Resource.Layout.SplashScreen)
        {
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            myAnimation = AnimationUtils.LoadAnimation(this, Resource.Animation.MyAnimation);
            myLayout = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            myImage = FindViewById<ImageView>(Resource.Id.imageView1);
            transition = (TransitionDrawable)myImage.Background;
            transition.StartTransition(500);
            
        }  

        protected override void OnResume()
        {
            base.OnResume();
            Task.Run(() =>
            {
                myImage.StartAnimation(myAnimation);
                myImage.Animation.AnimationEnd += delegate
                {
                    myLayout.SetBackgroundColor(Android.Graphics.Color.ParseColor("#E5E5E5"));
                };
            });
            //System.Threading.Thread.Sleep(40000);
        }
    }
}