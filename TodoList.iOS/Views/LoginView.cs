using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using TodoList.Core.MvxInteraction;
using TodoList.Core.ViewModels;
using UIKit;

namespace TodoList.iOS.Views
{
    public partial class LoginView : MvxViewController<LoginViewModel>
    {
        private UIBarButtonItem _buttonAdd;
        private UIViewController _ui;
        private IMvxInteraction<CloseUIViewController> _interaction;

        public LoginView() : base(nameof(LoginView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.NavigationItem.HidesBackButton = true;
            _buttonAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, null);
            NavigationItem.SetRightBarButtonItem(_buttonAdd, false);
            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(_buttonAdd).For("Clicked").To(vm => vm.NavigateToCollectionFragmentCommand);
            set.Bind(_buttonAdd).For(v => v.Enabled).To(vm => vm.ContinueButtonEnableStatus);
            set.Bind(this).For(view => view.Interaction).To(viewModel => viewModel.Interaction).OneWay();
            set.Apply();
        }

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

        private void OnInteractionRequested(object sender, MvxValueEventArgs<CloseUIViewController> eventArgs)
        {
            _ui.DismissViewController(true, null);
        }

        partial void LoginButton_TouchUpInside(UIKit.UIButton sender)
        {
            ViewModel.LoginFacebookCommand.Execute(null);
            _ui = ViewModel.Authenticator.GetUI();
            PresentViewController(_ui, true, null);
        }
    }
}