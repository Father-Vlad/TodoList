using Android.App;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using TodoList.Core.ViewModels;

namespace TodoList.Droid.Views
{
    [MvxActivityPresentation]
    [Activity]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        #region Lifecycle
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
            SetContentView(Resource.Layout.MainLayout);
            if (bundle == null)
            {
                ViewModel.ShowCurrentViewModelCommand.Execute(null);
            }
        }
        #endregion Lifecycle
    }
}