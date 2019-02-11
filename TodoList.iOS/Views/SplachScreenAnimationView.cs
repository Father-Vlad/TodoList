using CoreGraphics;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Color.Platforms.Ios;
using System;
using TodoList.Core;
using TodoList.Core.ViewModels;
using UIKit;

namespace TodoList.iOS
{
    [MvxRootPresentation(WrapInNavigationController = false)]
    public partial class SplachScreenAnimationView : MvxViewController<SplachScreenAnimationViewModel>
    {
        private Action OnFinishedAnimation { get; set; }
        private nfloat minAlpha = 0.0f;
        private nfloat maxAlpha = 1.0f;
        private CGAffineTransform minTransform = CGAffineTransform.MakeScale((nfloat)0.0, (nfloat)0.0);
        private CGAffineTransform maxTransform = CGAffineTransform.MakeScale((nfloat)10.0, (nfloat)10.0);

        public SplachScreenAnimationView() : base(nameof(SplachScreenAnimationView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.BringSubviewToFront(LaunchImageView);
            this.OnFinishedAnimation = () =>
            {
                ItemWitchAnimatedView.Alpha = minAlpha;
                ItemWitchAnimatedView.Transform = minTransform;
                this.View.BackgroundColor = new UIColor(0.9f, 0.9f, 0.9f, 1.0f);
                ViewModel.FinishAnimationCommand.Execute(null);
            };
            Scale(ItemWitchAnimatedView, true, 2.0, OnFinishedAnimation);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            LaunchImageView.Image = null;
            this.View.BackgroundColor = new UIColor(1.0f, 1.0f, 1.0f, 1.0f);
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