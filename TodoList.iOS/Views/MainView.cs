using MvvmCross.Platforms.Ios.Views;
using TodoList.Core.ViewModels;

namespace TodoList.iOS.Views
{
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
                _firstTimePresented = !_firstTimePresented;
                ViewModel.ShowLoginViewModelCommand.Execute(null);
            }

        }
    }
}