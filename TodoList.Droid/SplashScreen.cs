using Android.App;
using Android.Content.PM;
using MvvmCross.Platforms.Android.Views;

namespace TodoList.Droid
{
    [Activity(Label = "To-do List", MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}