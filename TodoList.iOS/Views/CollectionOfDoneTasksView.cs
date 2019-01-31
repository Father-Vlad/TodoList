using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TodoList.Core.ViewModels;
using TodoList.iOS.Sources;
using UIKit;

namespace TodoList.iOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Done", TabIconName = "Icon40")]
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
            var source = new TodoTasksTableViewSource(CollectionOfDoneTasksTableView);
            CollectionOfDoneTasksTableView.Source = source;
            var set = this.CreateBindingSet<CollectionOfDoneTasksView, CollectionOfDoneTasksViewModel>();
            set.Bind(source).To(vm => vm.Goals);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.FillingDataActivityCommand);
            set.Bind(_buttonAdd).For("Clicked").To(vm => vm.FillingDataActivityCommand);
            set.Apply();
            CollectionOfDoneTasksTableView.ReloadData();
        }
    }
}