using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Java.Lang;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using System.Collections.Generic;
using TodoList.Core.ViewModels;
using Xamarin.Facebook;
using Xamarin.Facebook.Login.Widget;

namespace TodoList.Droid.Views
{
    [Activity]
    public class CollectionView : MvxAppCompatActivity<CollectionViewModel>, IFacebookCallback
    {
        private RecyclerAdapter _recyclerAdapter;
        private RecyclerView.LayoutManager _layoutManager;
        private MvxRecyclerView _recyclerView;

        private ICallbackManager mFBCallManager;
        private MyProfileTracker mprofileTracker;
        LoginButton BtnFBLogin;

        public void OnCancel()
        {
        }

        public void OnError(FacebookException error)
        {
        }

        public void OnSuccess(Object result)
        {
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            FacebookSdk.SdkInitialize(this.ApplicationContext);
            mprofileTracker = new MyProfileTracker();
            mprofileTracker.mOnProfileChanged += mProfileTracker_mOnProfileChanged;
            mprofileTracker.StartTracking();

            SetContentView(Resource.Layout.CollectionLayout);

            BtnFBLogin = FindViewById<LoginButton>(Resource.Id.fblogin);
            BtnFBLogin.SetReadPermissions(new List<string> {
                "user_friends",
                "public_profile"
                });
            mFBCallManager = CallbackManagerFactory.Create();
            BtnFBLogin.RegisterCallback(mFBCallManager, this);

            _recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.recycler_view_main);
            _layoutManager = new LinearLayoutManager(this);
            _recyclerView.SetLayoutManager(_layoutManager);
            _recyclerAdapter = new RecyclerAdapter((IMvxAndroidBindingContext)this.BindingContext);
            _recyclerView.Adapter = _recyclerAdapter;
        }
        void mProfileTracker_mOnProfileChanged(object sender, OnProfileChangedEventArgs e)
        {
            if (e.mProfile != null)
            {
                try
                {
                    //TxtFirstName.Text = e.mProfile.FirstName;
                    //TxtLastName.Text = e.mProfile.LastName;
                    //TxtName.Text = e.mProfile.Name;
                    //mprofile.ProfileId = e.mProfile.Id;
                }
                catch (Java.Lang.Exception ex) { }
            }
            else
            {
                //TxtFirstName.Text = "First Name";
                //TxtLastName.Text = "Last Name";
                //TxtName.Text = "Name";
                //mprofile.ProfileId = null;
            }
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            mFBCallManager.OnActivityResult(requestCode, (int)resultCode, data);
        }
    }
}