using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views.InputMethods;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using System;
using System.Runtime.Remoting.Contexts;
using TodoList.Core.ViewModels;

namespace TodoList.Droid.Views
{
    [Activity(Label = "FillingDataView")]
    public class FillingDataView : MvxAppCompatActivity<FillingDataViewModel>
    {
        private InputMethodManager _imm;
        private LinearLayout _linearLayoutMain;
        private LinearLayout _linearLayoutToggle;
        private LinearLayout _linearLayoutBottom;
        private Toolbar _toolBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FillingDataLayout);
            var editTextGoalName = FindViewById<EditText>(Resource.Id.edit_text_goal_name);
            var editTextGoalDescription = FindViewById<EditText>(Resource.Id.edit_text_goal_description);
            Typeface newTypeface = Typeface.CreateFromAsset(Assets, "Gothic.ttf");
            editTextGoalName.SetTypeface(newTypeface, TypefaceStyle.Normal);
            editTextGoalDescription.SetTypeface(newTypeface, TypefaceStyle.Normal);
            _linearLayoutMain = FindViewById<LinearLayout>(Resource.Id.filling_data_layout_main);
            _linearLayoutToggle = FindViewById<LinearLayout>(Resource.Id.filling_data_layout_toggle);
            _linearLayoutBottom = FindViewById<LinearLayout>(Resource.Id.filling_data_layout_bottom);
            _toolBar = FindViewById<Toolbar>(Resource.Id.toolbar_fillind_data);
            _linearLayoutMain.Click += HideKeyboard;
            _linearLayoutToggle.Click += HideKeyboard;
            _linearLayoutBottom.Click += HideKeyboard;
            _toolBar.Click += HideKeyboard;
            _imm = (InputMethodManager)GetSystemService(InputMethodService);
        }

        private void HideKeyboard(object sender, EventArgs e)
        {
            if (CurrentFocus == null)
            {
                return;
            }
            _imm.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);
            CurrentFocus.ClearFocus();
        }
    }
}