using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using System.Collections.Generic;
using TodoList.Core.Services;
using TodoList.Core.ViewModels;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;

namespace TodoList.Droid.Views
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class LoginView : MvxAppCompatActivity<LoginViewModel>, IFacebookCallback
    {
        private ICallbackManager _mFBCallManager;
        private MyProfileTracker _mprofileTracker;
        private LoginButton _facebookLoginButton;
        private TextView _textViewWelcome;
        private ProfilePictureView _profilePictureView;

        protected override void OnCreate(Bundle bundle)
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
        }

        void MyProfileTracker_mOnProfileChanged(object sender, OnProfileChangedEventArgs e)
        {
            if (e.mProfile != null)
            {
                try
                {
                    this.ViewModel.CreateNewUser(e.mProfile.Id, e.mProfile.FirstName.ToString(), e.mProfile.LastName.ToString());
                    _profilePictureView.ProfileId = e.mProfile.Id;
                }
                catch (Java.Lang.Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }
            }
            if (e.mProfile == null)
            {
                this.ViewModel.UserFullName = "User Name";
                this.ViewModel.UserId = string.Empty;
                _profilePictureView.ProfileId = null;
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            _mFBCallManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        protected override void OnDestroy()
        {
            _mprofileTracker.StopTracking();
            base.OnDestroy();
        }

        public void OnCancel()
        {
        }

        public void OnError(FacebookException error)
        {
            if (error.Message == "net::ERR_NAME_NOT_RESOLVED")
            {
                Toast.MakeText(this, "No internet connection", ToastLength.Short).Show();
            }
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            Toast.MakeText(this, "Success", ToastLength.Short).Show();
            //LoginResult loginResult = result as LoginResult;
            //CurrentUser.CurrentUserId = loginResult.AccessToken.UserId.ToString();
        }
    }
}