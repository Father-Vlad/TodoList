using Android.App;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views.Animations;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using System.Threading.Tasks;
using TodoList.Core.ViewModels;

namespace TodoList.Droid.Views
{
    [Activity(Label = "SplashScreenAnimationActivityView")]
    public class SplashScreenAnimationActivityView : MvxAppCompatActivity<SplachViewModel>
    {
        #region Variables
        ImageView myImage;
        Animation myAnimation;
        TransitionDrawable transition;
        RelativeLayout myLayout;
        #endregion Variables

        #region Constructors
        #endregion Constructors

        #region Lifecycle
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LaunchSplachScreenAnimationLayout);
            OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
            myImage = FindViewById<ImageView>(Resource.Id.launchSplachScreen_animation_imageView);
            myAnimation = AnimationUtils.LoadAnimation(this, Resource.Animation.MyAnimation);
            transition = (TransitionDrawable)myImage.Background;
            transition.StartTransition(500);
            myLayout = FindViewById<RelativeLayout>(Resource.Id.launchSplachScreen_animation_relativeLayout);
            Task.Run(() =>
            {
                myImage.StartAnimation(myAnimation);
                myImage.Animation.AnimationEnd += delegate
                {
                    myLayout.SetBackgroundColor(Android.Graphics.Color.ParseColor("#E5E5E5"));
                    this.ViewModel.FinishAnimationCommand.Execute(null);
                };
            });
        }
        #endregion Lifecycle

        #region Properties
        #endregion Properties

        #region Commands
        #endregion Commands

        #region Methods
        #endregion Methods

        #region Overrides
        #endregion Overrides
    }
}