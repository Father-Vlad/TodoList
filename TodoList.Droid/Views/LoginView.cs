using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using TodoList.Core.Services;
using TodoList.Core.ViewModels;
using Xamarin.Auth;
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
            var auth = new OAuth2Authenticator(
            clientId: "1838603119596376",
            scope:"",
            authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
            redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html")
            );

            auth.Completed += FacebookAuthCompleted;
            StartActivity(auth.GetUI(View.Context));
        }

        private async void FacebookAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            AccountStore.Create().Save(e.Account, "FacebookLastUser");
            var request = new OAuth2Request(
                "GET",
                new Uri("https://graph.facebook.com/me?fields=id,name,picture,email"),
                null,
                e.Account
                );
            var response = await request.GetResponseAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var userJson = response.GetResponseText();
                var jobject = JObject.Parse(userJson);
                var userId = jobject["id"]?.ToString();
                var userName = jobject["name"]?.ToString();
                this.ViewModel.UrlId = userId;
                this.ViewModel.UserName = userName;
                CurrentUser.CurrentUserId = userId;
            }

            if (e.IsAuthenticated)
            {
                Toast.MakeText(this.Context, "Authenticated!", ToastLength.Long).Show();
            }
        }
    }
}