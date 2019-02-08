using CoreGraphics;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using TodoList.Core.ViewModels;
using UIKit;

namespace TodoList.iOS
{
    [MvxModalPresentation(WrapInNavigationController = true, Animated = false)]
    public partial class LaunchSplachScreenAnimationView : MvxViewController<SplachScreenAnimationViewModel>
    {
        private Action OnFinishedAnimation { get; set; }
        private nfloat minAlpha = 0.0f;
        private nfloat maxAlpha = 1.0f;
        private CGAffineTransform minTransform = CGAffineTransform.MakeScale((nfloat)0.0, (nfloat)0.0);
        private CGAffineTransform maxTransform = CGAffineTransform.MakeScale((nfloat)9.0, (nfloat)9.0);

        public LaunchSplachScreenAnimationView() : base(nameof(LaunchSplachScreenAnimationView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.OnFinishedAnimation = () =>
            {
                ItemWitchAnimatedView.Alpha = minAlpha;
                ItemWitchAnimatedView.Transform = minTransform;
                ViewModel.FinishAnimationCommand.Execute(null);
            };
            Scale(ItemWitchAnimatedView, true, 2.0, OnFinishedAnimation);
        }
        
        public void Scale(UIView view, bool isIn, double duration, Action onFinished)
        {
            view.Alpha = minAlpha;
            view.Transform = minTransform;
            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
                {
                    view.Alpha = isIn ? maxAlpha : minAlpha;
                    view.Transform = isIn ? maxTransform : minTransform;
                }, onFinished);
        }
    }
}