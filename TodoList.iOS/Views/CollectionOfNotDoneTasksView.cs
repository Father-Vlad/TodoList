using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TodoList.Core.ViewModels;
using TodoList.iOS.Sources;
using UIKit;
namespace TodoList.iOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Not Done")]
    public partial class CollectionOfNotDoneTasksView : MvxViewController<CollectionOfNotDoneTasksViewModel>
    {
        private UIBarButtonItem _buttonAdd;

        public CollectionOfNotDoneTasksView() : base(nameof(CollectionOfNotDoneTasksView), null)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _buttonAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, null);
            NavigationItem.SetRightBarButtonItem(_buttonAdd, false);
            var source = new TodoTasksTableViewSource(CollectionOfNotDoneTasksTableView);
            CollectionOfNotDoneTasksTableView.Source = source;
            var set = this.CreateBindingSet<CollectionOfNotDoneTasksView, CollectionOfDoneTasksViewModel>();
            set.Bind(source).To(vm => vm.Goals);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.FillingDataActivityCommand);
            set.Bind(_buttonAdd).For("Clicked").To(vm => vm.FillingDataActivityCommand);
            set.Apply();
            CollectionOfNotDoneTasksTableView.ReloadData();
        }
    }
}