using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System.Threading.Tasks;
using TodoList.Core.ViewModels;

namespace TodoList.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    [Register("TodoList.Droid.Views.SplachScreenAnimationView")]
    public class SplachScreenAnimationView : BaseFragment<SplachScreenAnimationViewModel>
    {
        ImageView myImage;
        Animation myAnimation;
        TransitionDrawable transition;
        RelativeLayout myLayout;

        protected override int FragmentId
        {
            get
            {
                return Resource.Layout.LaunchSplachScreenAnimationLayout;
            }
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Activity.OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            myImage = view.FindViewById<ImageView>(Resource.Id.launchSplachScreen_animation_imageView);
            myAnimation = AnimationUtils.LoadAnimation(Context, Resource.Animation.MyAnimation);
            transition = (TransitionDrawable)myImage.Background;
            transition.StartTransition(500);
            myLayout = view.FindViewById<RelativeLayout>(Resource.Id.launchSplachScreen_animation_relativeLayout);
            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
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
    }
}