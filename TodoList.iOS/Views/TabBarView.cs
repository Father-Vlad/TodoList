using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Color.Platforms.Ios;
using System;
using TodoList.Core;
using TodoList.Core.ViewModels;

namespace TodoList.iOS.Views
{
    [MvxRootPresentation(WrapInNavigationController = false)]
    public class TabBarView : MvxTabBarViewController<ViewPagerViewModel>
    {
        private bool _firstTimePresented = true;
        public TabBarView()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TabBar.BarTintColor = AppColors.PrimaryColor.ToNativeColor();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            var logOutHandler = new Action(() => ViewModel.LogoutCommand.Execute());
            if (_firstTimePresented)
            {
                ViewModel.ShowCollectionOfDoneTasksViewModelCommand.Execute(logOutHandler);
                ViewModel.ShowCollectionOfNotDoneTasksViewModelCommand.Execute(logOutHandler);
                _firstTimePresented = false;
            }
        }
    }
}