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
        #region Variables
        private bool _firstTimePresented = true;
        #endregion Variables

        #region Constructors
        public TabBarView()
        {
        }
        #endregion Constructors

        #region Lifecycle
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
                ViewModel.ShowCompletedGoalsViewModelCommand.Execute(logOutHandler);
                ViewModel.ShowUncompletedGoalsViewModelCommand.Execute(logOutHandler);
                _firstTimePresented = false;
            }
        }
        #endregion Lifecycle

        #region Properties
        #endregion Properties

        #region Commands
        #endregion Commands

        #region Methods
        #endregion Methods

        #region Overrides
        #endregion Overrides
    }
}