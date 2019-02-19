using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TodoList.Core.ViewModels;

namespace TodoList.iOS.Views
{
    [MvxModalPresentation(WrapInNavigationController = false, Animated = false)]
    public class MainView : MvxViewController<MainViewModel>
    {
        #region Variables
        private bool _firstTimePresented = true;
        #endregion Variables

        #region Constructors
        public MainView()
        {
        }
        #endregion Constructors

        #region Lifecycle
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
        #endregion Lifecycle
    }
}