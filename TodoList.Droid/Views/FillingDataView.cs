using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
using TodoList.Core.ViewModels;

namespace TodoList.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel),Resource.Id.content_frame, true)]
    [Register("TodoList.Droid.Views.FillingDataView")]
    public class FillingDataView : BaseFragment<FillingDataViewModel>
    {
        private InputMethodManager _imm;
        private LinearLayout _linearLayoutMain;
        private LinearLayout _linearLayoutToggle;
        private LinearLayout _linearLayoutBottom;
        private Toolbar _toolBar;

        protected override int FragmentId => Resource.Layout.FillingDataLayout;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            var editTextGoalName = view.FindViewById<EditText>(Resource.Id.edit_text_goal_name);
            var editTextGoalDescription = view.FindViewById<EditText>(Resource.Id.edit_text_goal_description);
            Typeface newTypeface = Typeface.CreateFromAsset(view.Context.Assets, "Gothic.ttf");
            editTextGoalName.SetTypeface(newTypeface, TypefaceStyle.Normal);
            editTextGoalDescription.SetTypeface(newTypeface, TypefaceStyle.Normal);
            _linearLayoutMain = view.FindViewById<LinearLayout>(Resource.Id.filling_data_layout_main);
            _linearLayoutToggle = view.FindViewById<LinearLayout>(Resource.Id.filling_data_layout_toggle);
            _linearLayoutBottom = view.FindViewById<LinearLayout>(Resource.Id.filling_data_layout_bottom);
            _toolBar = view.FindViewById<Toolbar>(Resource.Id.toolbar_fillind_data);
            _linearLayoutMain.Click += HideKeyboard;
            _linearLayoutToggle.Click += HideKeyboard;
            _linearLayoutBottom.Click += HideKeyboard;
            _toolBar.Click += HideKeyboard;
            _imm = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
            return view;
        }

        private void HideKeyboard(object sender, EventArgs e)
        {
            if (Activity.CurrentFocus == null)
            {
                return;
            }
            _imm.HideSoftInputFromWindow(Activity.CurrentFocus.WindowToken, 0);
            Activity.CurrentFocus.ClearFocus();
        }
        /*protected override void OnCreate(Bundle savedInstanceState)
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
        }*/



        /*public void HideKeyboard(object sender, EventArgs e)
        {
            InputMethodManager close = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
            close.HideSoftInputFromWindow(_linearLayout.WindowToken, 0);
        }*/

    }
}