using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using TodoList.Core.ViewModels;
using Xamarin.Facebook;

namespace TodoList.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    [Register("TodoList.Droid.Views.LoginView")]
    public class LoginView : BaseFragment<LoginViewModel>
    {
        private TextView _textViewWelcome;
        private Button _facebookLoginButton;

        protected override int FragmentId
        {
            get
            {
                return Resource.Layout.LoginLayout;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            FacebookSdk.SdkInitialize(this.Context);
            _facebookLoginButton = view.FindViewById<Button>(Resource.Id.facebook_login_button);
            _facebookLoginButton.Click += delegate { LoginFacebook(); };
            _textViewWelcome = view.FindViewById<TextView>(Resource.Id.text_view_login);
            Typeface newTypeface = Typeface.CreateFromAsset(view.Context.Assets, "Gothic.ttf");
            _textViewWelcome.SetTypeface(newTypeface, TypefaceStyle.Normal);
            return view;
        }

        private void LoginFacebook()
        {
            this.ViewModel.LoginFacebookCommand.Execute();
            StartActivity(this.ViewModel.Authenticator.GetUI(View.Context));
        }
    }
}