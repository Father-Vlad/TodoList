using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using TodoList.Core.ViewModels;

namespace TodoList.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel),Resource.Id.content_frame, true)]
    [Register("TodoList.Droid.Views.FillingDataView")]
    public class FillingDataView : BaseFragment<FillingDataViewModel>
    {
        private LinearLayout _linearLayoutMain;
        private LinearLayout _linearLayoutToggle;
        private LinearLayout _linearLayoutBottom;
        private Toolbar _toolBar;

        protected override int FragmentId
        {
            get
            {
                return Resource.Layout.FillingDataLayout;
            }
        }

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
            _linearLayoutMain.Click += delegate { HideKeyboard(); }; 
            _linearLayoutToggle.Click += delegate { HideKeyboard(); };
            _linearLayoutBottom.Click += delegate { HideKeyboard(); };
            _toolBar.Click += delegate { HideKeyboard(); };
            return view;
        }

        private void HideKeyboard()
        {
            if (Activity.CurrentFocus != null)
            {
                InputMethodManager imm = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(Activity.CurrentFocus.WindowToken, 0);
            }
        }
        public override void OnDestroyView()
        {
            base.OnDestroyView();
            HideKeyboard();
        }
    }
}