using Android.App;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using TodoList.Core.ViewModels;

namespace TodoList.Droid.Views
{
    [Activity(Label = "FillingDataView")]
    public class FillingDataView : MvxAppCompatActivity<FillingDataViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FillingDataLayout);
        }
    }
}