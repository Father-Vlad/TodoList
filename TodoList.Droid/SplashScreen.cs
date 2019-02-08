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
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait,FinishOnTaskLaunch = false)]
    [IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.ActionView, Intent.CategoryDefault, Intent.CategoryBrowsable }, DataScheme = "@string/text_title", DataHost = "my_code_is_here")]
    public class SplashScreen : MvxSplashScreenActivity
    {
        ImageView myImage;
        Animation myAnimation;
        LinearLayout myLayout;
        TransitionDrawable transition;

        public SplashScreen() : base(Resource.Layout.SplashScreen)
        {
        }
    }
}