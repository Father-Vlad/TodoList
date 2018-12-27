using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
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
            var editTextGoalName = FindViewById<EditText>(Resource.Id.edit_text_goal_name);
            var editTextGoalDescription = FindViewById<EditText>(Resource.Id.edit_text_goal_description);
            Typeface newTypeface = Typeface.CreateFromAsset(Assets, "Gothic.ttf");
            editTextGoalName.SetTypeface(newTypeface, TypefaceStyle.Normal);
            editTextGoalDescription.SetTypeface(newTypeface, TypefaceStyle.Normal);
        }
    }
}