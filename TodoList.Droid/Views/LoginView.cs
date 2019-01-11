using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Net;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Net;
using TodoList.Core.Services;
using TodoList.Core.ViewModels;
using Xamarin.Auth;
using Xamarin.Facebook;
using Xamarin.Facebook.Login.Widget;
//using Xamarin.Facebook.Login;

namespace TodoList.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    [Register("TodoList.Droid.Views.LoginView")]
    public class LoginView : BaseFragment<LoginViewModel>
    {
        //private ICallbackManager _mFBCallManager;
        //private MyProfileTracker _mprofileTracker;
        //private LoginButton _facebookLoginButton2;
        private TextView _textViewWelcome;
        //private ProfilePictureView _profilePictureView;
        private Button _facebookLoginButton;

        protected override int FragmentId => Resource.Layout.LoginLayout;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            FacebookSdk.SdkInitialize(this.Context);
            //_mprofileTracker = new MyProfileTracker();
            //_mprofileTracker.MyOnProfileChanged += MyProfileTracker_mOnProfileChanged;
            //_mprofileTracker.StartTracking();
            ////SetContentView(Resource.Layout.LoginLayout);
            //_mFBCallManager = CallbackManagerFactory.Create();
            //_facebookLoginButton2 = view.FindViewById<LoginButton>(Resource.Id.facebook_login_button2);
            //_facebookLoginButton2.SetReadPermissions(new List<string> { "public_profile", "user_friends" });
            //_facebookLoginButton2.RegisterCallback(_mFBCallManager, this);
            _facebookLoginButton = view.FindViewById<Button>(Resource.Id.facebook_login_button);
            _facebookLoginButton.Click += delegate { LoginFacebook(); };
            _textViewWelcome = view.FindViewById<TextView>(Resource.Id.text_view_login);
            Typeface newTypeface = Typeface.CreateFromAsset(view.Context.Assets, "Gothic.ttf");
            _textViewWelcome.SetTypeface(newTypeface, TypefaceStyle.Normal);
            //_profilePictureView = view.FindViewById<ProfilePictureView>(Resource.Id.profile_picture_view);
            //_profilePictureView.Visibility = ViewStates.Gone;

            return view;
        }

        private void LoginFacebook()
        {
            var auth = new OAuth2Authenticator(/*(consumerKey:"",consumerSecret:"",requestTokenUrl:new Uri(""),authorizeUrl:new Uri("https://m.facebook.com/dialog/oauth/"),accessTokenUrl:new Uri(""),callbackUrl:new Uri("https://www.facebook.com/connect/login_success.html")*/
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
                //_profilePictureView.ProfileId = userId;
                this.ViewModel.UrlId = userId;
                this.ViewModel.UserName = userName;
                CurrentUser.CurrentUserId = userId;
                //jobject["picture"]?["data"]?["url"].ToString();
                //Email = jobject["emails"]?["preferred"].ToString();
                //_image.SetImageURI(Android.Net.Uri.Parse("https://platform-lookaside.fbsbx.com/platform/profilepic/?asid=362629600986228&height=100&width=100&ext=1549790007&hash=AeTSKcNpkxGkop8O"));
                //_image.SetImageURI(Android.Net.Uri.Parse(jobject["picture"]?["data"]?["url"].ToString()));
                //_image.SetImageResource(Convert.ToInt32(jobject["picture"]?["data"]?["url"].ToString()));
            }
            //AccountStore.Create(this.Context).Save(e.Account, "Facebook");
            //var current = AccountStore.Create(this.Context).FindAccountsForService("Facebook").FirstOrDefault();
            //CurrentUser.CurrentUserId = current.Properties["access_token"];
            if (e.IsAuthenticated)
            {
                Toast.MakeText(this.Context, "Authenticated!", ToastLength.Long).Show();
            }
        }

        /*protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            FacebookSdk.SdkInitialize(this.ApplicationContext);
            _mprofileTracker = new MyProfileTracker();
            _mprofileTracker.MyOnProfileChanged += MyProfileTracker_mOnProfileChanged;
            _mprofileTracker.StartTracking();
            SetContentView(Resource.Layout.LoginLayout);
            _mFBCallManager = CallbackManagerFactory.Create();
            _facebookLoginButton = FindViewById<LoginButton>(Resource.Id.facebook_login_button);
            _facebookLoginButton.SetReadPermissions(new List<string> { "public_profile", "user_friends" });
            _facebookLoginButton.RegisterCallback(_mFBCallManager, this);
            _textViewWelcome = FindViewById<TextView>(Resource.Id.text_view_login);
            Typeface newTypeface = Typeface.CreateFromAsset(Assets, "Gothic.ttf");
            _textViewWelcome.SetTypeface(newTypeface, TypefaceStyle.Normal);
            _profilePictureView = FindViewById<ProfilePictureView>(Resource.Id.profile_picture_view);
            _profilePictureView.ProfileId = this.ViewModel.GetCurrentUserId();
        }*/

        //void MyProfileTracker_mOnProfileChanged(object sender, OnProfileChangedEventArgs e)
        //{
        //    if (e.mProfile != null)
        //    {
        //        try
        //        {
        //            this.ViewModel.CreateNewUser(e.mProfile.Id, e.mProfile.Name.ToString());
        //            _profilePictureView.ProfileId = e.mProfile.Id;
        //        }
        //        catch (Java.Lang.Exception ex)
        //        {
        //            Toast.MakeText(this.Context, ex.Message, ToastLength.Short).Show();
        //        }
        //    }
        //    if (e.mProfile == null)
        //    {
        //        this.ViewModel.LogOut();
        //        _profilePictureView.ProfileId = null;
        //        Toast.MakeText(this.Context, "Logged Out", ToastLength.Short).Show();
        //    }
        //}

        //public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        //{
        //    base.OnActivityResult(requestCode, resultCode, data);
        //    _mFBCallManager.OnActivityResult(requestCode, (int)resultCode, data);
        //}

        //public override void OnDestroyView()
        //{
        //    _mprofileTracker.StopTracking();
        //    base.OnDestroyView();
        //}

        ///*protected override void OnDestroy()
        //{
        //    base.OnDestroy();
        //}*/

        //public void OnCancel()
        //{
        //}

        //public void OnError(FacebookException error)
        //{
        //    if (error.Message == "net::ERR_NAME_NOT_RESOLVED")
        //    {
        //        Toast.MakeText(this.Context, "No internet connection", ToastLength.Short).Show();
        //    }
        //}

        //public void OnSuccess(Java.Lang.Object result)
        //{
        //    Toast.MakeText(this.Context, "Success", ToastLength.Short).Show();
        //}
    }
}