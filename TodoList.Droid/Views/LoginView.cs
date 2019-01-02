using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using System;
using System.Collections.Generic;
using TodoList.Core.Interfaces;
using TodoList.Core.ViewModels;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;

namespace TodoList.Droid.Views
{
    [Activity]
    public class LoginView : MvxAppCompatActivity<LoginViewModel>, IFacebookCallback
    {
        private ICallbackManager _mFBCallManager;
        private MyProfileTracker _mprofileTracker;
        private LoginButton _facebookLoginButton;
        private TextView _textViewWelcome;
        private TextView _textUserName;
        private ProfilePictureView _profilePictureView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            FacebookSdk.SdkInitialize(this.ApplicationContext);
            _mprofileTracker = new MyProfileTracker();
            _mprofileTracker.mOnProfileChanged += mProfileTracker_mOnProfileChanged;
            _mprofileTracker.StartTracking();
            SetContentView(Resource.Layout.LoginLayout);
            _mFBCallManager = CallbackManagerFactory.Create();
            _facebookLoginButton = FindViewById<LoginButton>(Resource.Id.facebook_login_button);
            _facebookLoginButton.SetReadPermissions(new List<string> { "public_profile", "user_friends" });
            _facebookLoginButton.RegisterCallback(_mFBCallManager, this);
            _textViewWelcome = FindViewById<TextView>(Resource.Id.text_view_login);
            Typeface newTypeface = Typeface.CreateFromAsset(Assets, "Gothic.ttf");
            _textViewWelcome.SetTypeface(newTypeface, TypefaceStyle.Normal);
            _textUserName = FindViewById<TextView>(Resource.Id.text_view_user_name);
            _profilePictureView = FindViewById<ProfilePictureView>(Resource.Id.profile_picture_view);
        }

        void mProfileTracker_mOnProfileChanged(object sender, OnProfileChangedEventArgs e)
        {
            if (e.mProfile != null)
            {
                try
                {
                    _textUserName.Text = e.mProfile.Name;
                    _profilePictureView.ProfileId = e.mProfile.Id;
                }
                catch (Java.Lang.Exception ex)
                {

                }
            }
            if (e.mProfile == null)
            {
                _textUserName.Text = "User Name";
                _profilePictureView.ProfileId = null;
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            _mFBCallManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        /*protected override void OnDestroy()
        {
            _mprofileTracker.StopTracking();
            base.OnDestroy();
        }*/

        public void OnCancel()
        {
        }

        public void OnError(FacebookException error)
        {
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            LoginResult loginResult = result as LoginResult;
            //this.ViewModel.UserId = loginResult.AccessToken.UserId;
            Console.WriteLine(loginResult.AccessToken.UserId);
        }
    }
}