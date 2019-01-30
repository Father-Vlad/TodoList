using Foundation;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using TodoList.Core.ViewModels;
using UIKit;

namespace TodoList.iOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = " Done")]
    public partial class CollectionOfDoneTasksView : MvxViewController<CollectionOfDoneTasksViewModel>
    {
        private UIBarButtonItem _buttonAdd;
        
        public CollectionOfDoneTasksView () : base (nameof(CollectionOfDoneTasksView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _buttonAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, null);
            NavigationItem.SetRightBarButtonItem(_buttonAdd, false);

            //var source = new TaskTableViewSource
        }
    }
}