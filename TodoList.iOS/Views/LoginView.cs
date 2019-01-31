using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using TodoList.Core.ViewModels;
using UIKit;

namespace TodoList.iOS.Views
{
    public partial class LoginView : MvxViewController<LoginViewModel>
    {
        private UIBarButtonItem _buttonAdd;
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

        partial void LoginButton_TouchUpInside(UIKit.UIButton sender)
        {
            ViewModel.LoginFacebookCommand.Execute(null);
            var ui = ViewModel.Authenticator.GetUI();
            PresentViewController(ui, true, null);
        }
    }
}