using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
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

        public LoginView() : base(nameof(LoginView), null)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _buttonAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, null);
            NavigationItem.SetRightBarButtonItem(_buttonAdd, false);
            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(LoginToFacebookButton).To(vm => vm.NavigateToCollectionFragmentCommand);
            set.Bind(_buttonAdd).For("Clicked").To(vm => vm.NavigateToCollectionFragmentCommand);
            set.Bind(_buttonAdd).For(v => v.Enabled).To(vm => vm.ContinueButtonEnableStatus);
            set.Apply();
        }

        private void OnInteractionRequested(object sender, MvxValueEventArgs<CloseUIViewController> eventArgs)
        {
            _ui.DismissViewController(true, null);
            eventArgs.Value.OnClose();
        }

        partial void LoginButton_TouchUpInside(UIKit.UIButton sender)
        {
            ViewModel.LoginFacebookCommand.Execute(null);
            _ui = ViewModel.Authenticator.GetUI();
            PresentViewController(_ui, true, null);
        }
    }
}