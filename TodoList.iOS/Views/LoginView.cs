using CoreGraphics;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.ViewModels;
using TodoList.Core.MvxInteraction;
using TodoList.Core.ViewModels;
using UIKit;
using Xamarin.Essentials;

namespace TodoList.iOS.Views
{
    [MvxModalPresentation(WrapInNavigationController = true, Animated = false)]
    public partial class LoginView : BaseViewController<LoginViewModel>
    {
        #region Variables
        private UIButton _buttonContinue;
        private UIViewController _ui;
        private IMvxInteraction<CloseUIViewController> _interaction;
        #endregion Variables

        #region Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupLoginNavigationBar();
            SetupBinding();
        }
        #endregion Lifecycle

        #region Properties
        public IMvxInteraction<CloseUIViewController> Interaction
        {
            get => _interaction;
            set
            {
                if (_interaction != null)
                    _interaction.Requested -= OnInteractionRequested;

                _interaction = value;
                _interaction.Requested += OnInteractionRequested;
            }
        }
        #endregion Properties

        #region Methods
        private void SetupLoginNavigationBar()
        {
            this.NavigationItem.HidesBackButton = true;
            _buttonContinue = new UIButton(UIButtonType.Custom);
            _buttonContinue.Frame = new CGRect(0, 0, 40, 40);
            _buttonContinue.SetImage(UIImage.FromBundle("LoginViewNavigationBar"), UIControlState.Normal);
            this.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(_buttonContinue), false);
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes() { TextColor = UIColor.White });
            NavigationController.NavigationBar.BarTintColor = new UIColor(0.17f, 0.24f, 0.31f, 1.0f);
        }

        private void SetupBinding()
        {
            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(_buttonContinue).To(vm => vm.NavigateToCollectionFragmentCommand);
            set.Bind(_buttonContinue).For(v => v.Enabled).To(vm => vm.ContinueButtonEnableStatus);
            set.Bind(this).For(view => view.Interaction).To(viewModel => viewModel.Interaction).OneWay();
            set.Bind(UserImageView).For(v => v.Image).To(vm => vm.UserId).WithConversion("UrlPictureString");
            set.Bind(UserImageView).For(v => v.Hidden).To(vm => vm.ProfilePictureViewVisibleStatus).WithConversion("ProfilePictureVisibleStatus");
            set.Bind(LoginToFacebookButton).For("Title").To(vm => vm.LoginButtonText);
            set.Bind(LoginToFacebookButton).For("Enabled").To(vm => vm.IsNetAvailable);
            set.Bind(NavigationItem).For(v => v.Title).To(vm => vm.WelcomeText);
            set.Bind(UserNameLabel).To(vm => vm.UserName);
            set.Bind(NetAvailableLabel).For(v => v.Hidden).To(vm => vm.IsNetAvailable);
            set.Apply();
        }

        partial void LoginButton_TouchUpInside(UIKit.UIButton sender)
        {
            if (string.IsNullOrEmpty(this.ViewModel.UserId))
            {
                ViewModel.LoginFacebookCommand.Execute(null);
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    _ui = ViewModel.Authenticator.GetUI();
                    PresentViewController(_ui, true, null);
                    return;
                }
            }
            this.ViewModel.LogoutFacebookCommand.Execute();
        }

        private void OnInteractionRequested(object sender, MvxValueEventArgs<CloseUIViewController> eventArgs)
        {
            _ui.DismissViewController(true, null);
        }
        #endregion Methods

        #region Overrides
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return UIInterfaceOrientationMask.Portrait;
        }
        #endregion Overrides
    }
}