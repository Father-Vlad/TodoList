using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TodoList.Core.ViewModels;
using TodoList.iOS.Helper;
using TodoList.iOS.Sources;
using UIKit;

namespace TodoList.iOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "DONE", TabIconName = "TabBarChecked")]
    public partial class CollectionOfDoneTasksView : MvxViewController<CollectionOfDoneTasksViewModel>
    {
        private UIBarButtonItem _buttonAdd;
        private UIBarButtonItem _buttonLogOut;
        private readonly string _textTitle = "To-do List";
        //private readonly string _toastMessage = "Telegram is not installed";
        //private string _shareText = "Hi, I created a new task for myself with To-do List app.\nThe name of task is: ";
        //private string _tabCellMessage = "tg://msg?text=";
        private MvxUIRefreshControl _refreshControl;
        NSUrl _urlShareToTelegram;

        public CollectionOfDoneTasksView () : base (nameof(CollectionOfDoneTasksView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = _textTitle;
            _refreshControl = new MvxUIRefreshControl();
            CollectionOfDoneTasksTableView.AddSubview(_refreshControl);
            _buttonAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, null);
            NavigationItem.SetRightBarButtonItem(_buttonAdd, false);
            _buttonLogOut = new UIBarButtonItem(UIBarButtonSystemItem.Stop, null);
            NavigationItem.SetLeftBarButtonItem(_buttonLogOut, false);
            var source = new TodoTasksTableViewSource(CollectionOfDoneTasksTableView);
            CollectionOfDoneTasksTableView.Source = source;
            source.OnShareHandlerSource = (currentTask) => 
            {
                this.ViewModel.ShareMessageCommand.Execute(currentTask);
            };
            var set = this.CreateBindingSet<CollectionOfDoneTasksView, CollectionOfDoneTasksViewModel>();
            set.Bind(source).To(vm => vm.Goals);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.FillingDataActivityCommand);
            set.Bind(_buttonAdd).For("Clicked").To(vm => vm.FillingDataActivityCommand);
            set.Bind(_buttonLogOut).For("Clicked").To(vm => vm.LogoutCommand);
            set.Bind(_refreshControl).For(v => v.IsRefreshing).To(vm => vm.IsRefreshLayoutRefreshing);
            set.Bind(_refreshControl).For(v => v.RefreshCommand).To(vm => vm.UpdateDataCommand);
            set.Apply();
            CollectionOfDoneTasksTableView.ReloadData();
        }
    }
}