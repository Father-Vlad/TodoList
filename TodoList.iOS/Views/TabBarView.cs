using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Color.Platforms.Ios;
using TodoList.Core;
using TodoList.Core.ViewModels;

namespace TodoList.iOS.Views
{
    [MvxRootPresentation(WrapInNavigationController = false)]
    public class TabBarView : MvxTabBarViewController<ViewPagerViewModel>
    {
        public TabBarView()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TabBar.BarTintColor = AppColors.PrimaryColor.ToNativeColor();
            TabBar.TintColor = AppColors.AccentColor.ToNativeColor();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ViewModel.ShowCollectionOfDoneTasksViewModelCommand.Execute(null);
            ViewModel.ShowCollectionOfNotDoneTasksViewModelCommand.Execute(null);
        }
    }
}