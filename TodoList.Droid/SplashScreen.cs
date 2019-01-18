using Android.App;
using Android.Content;
using Android.Content.PM;
using MvvmCross.Platforms.Android.Views;

namespace TodoList.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    [IntentFilter(new[] { Intent.ActionView }, Categories = new[] {Intent.ActionView,Intent.CategoryDefault,Intent.CategoryBrowsable}, DataScheme = "testappforlinks", DataHost = "my_code_is_here")]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}