using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Color.Platforms.Ios;
using TodoList.Core;
using TodoList.Core.ViewModels;

namespace TodoList.iOS.Views
{
    [MvxModalPresentation(WrapInNavigationController = false, Animated = false)]
    public class MainView : MvxViewController<MainViewModel>
    {
        private bool _firstTimePresented = true;
        public MainView()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            if (_firstTimePresented)
            {
                _firstTimePresented = false;
                ViewModel.ShowCurrentViewModelCommand.Execute(null);
            }

        }
    }
}