using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using UIKit;

namespace TodoList.iOS.Views
{
    public abstract class BaseViewController<TViewModel> : MvxViewController where TViewModel : class, IMvxViewModel
    {
        #region Variables
        protected UIButton _buttonFillingData;
        protected UIButton _buttonLogOut;
        protected readonly string _textTitle = "To-do List";
        protected UIButton _buttonGoBack;
        protected readonly string _textTitleBack = "Write TODO sample...";
        #endregion Variables

        #region Properties
        public new TViewModel ViewModel
        {
            get
            {
                return (TViewModel)base.ViewModel;
            }

            set
            {
                base.ViewModel = value;
            }
        }
        #endregion Properties

        #region Methods
        protected void SetupNavigationBar()
        {
            NavigationItem.Title = _textTitle;
            var titleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.White };
            NavigationController.NavigationBar.TitleTextAttributes = titleTextAttributes;
            _buttonFillingData = new UIButton(UIButtonType.Custom);
            _buttonFillingData.Frame = new CGRect(0, 0, 40, 40);
            _buttonFillingData.SetImage(UIImage.FromBundle("TabBarFillingDataIcon"), UIControlState.Normal);
            this.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(_buttonFillingData), false);
            _buttonLogOut = new UIButton(UIButtonType.Custom);
            _buttonLogOut.Frame = new CGRect(0, 0, 40, 40);
            _buttonLogOut.SetImage(UIImage.FromBundle("TabBarLogOutIcon"), UIControlState.Normal);
            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_buttonLogOut), false);
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes() { TextColor = UIColor.White });
            NavigationController.NavigationBar.BarTintColor = new UIColor(0.17f, 0.24f, 0.31f, 1.0f);
        }

        protected void SetupBackNavigationBar()
        {
            Title = _textTitleBack;
            _buttonGoBack = new UIButton(UIButtonType.Custom);
            _buttonGoBack.Frame = new CGRect(0, 0, 40, 40);
            _buttonGoBack.SetImage(UIImage.FromBundle("FillingDataBackIcon"), UIControlState.Normal);
            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(_buttonGoBack), false);
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes() { TextColor = UIColor.White });
            NavigationController.NavigationBar.BarTintColor = new UIColor(0.17f, 0.24f, 0.31f, 1.0f);
        }
        #endregion Methods
    }
}