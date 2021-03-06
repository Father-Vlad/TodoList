﻿using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TodoList.Core.ViewModels;
using TodoList.iOS.Sources;

namespace TodoList.iOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "DONE", TabIconName = "TabBarChecked")]
    public partial class CollectionOfDoneTasksView : BaseViewController<CompletedGoalsViewModel>
    {
        #region Variables
        private MvxUIRefreshControl _refreshControl;
        #endregion Variables

        #region Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupNavigationBar();
            _refreshControl = new MvxUIRefreshControl();
            CollectionOfDoneTasksTableView.AddSubview(_refreshControl);
            var source = new TodoTasksTableViewSource(CollectionOfDoneTasksTableView);
            CollectionOfDoneTasksTableView.Source = source;
            source.OnShareHandlerSource = (currentTask) =>
            {
                this.ViewModel.PlatformName = false; //if false -> iOS, if true -> Android
                this.ViewModel.ShareMessageCommand.Execute(currentTask);
            };
            SetupBinding(source);
            CollectionOfDoneTasksTableView.ReloadData();
        }
        #endregion Lifecycle

        #region Methods
        private void SetupBinding(TodoTasksTableViewSource source)
        {
            var set = this.CreateBindingSet<CollectionOfDoneTasksView, CompletedGoalsViewModel>();
            set.Bind(source).To(vm => vm.Goals);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.FillingDataActivityCommand);
            set.Bind(_buttonFillingData).To(vm => vm.FillingDataActivityCommand);
            set.Bind(_buttonFillingData).For("Enabled").To(vm => vm.IsNetAvailable);
            set.Bind(_buttonLogOut).To(vm => vm.LogoutCommand);
            set.Bind(_refreshControl).For(v => v.IsRefreshing).To(vm => vm.IsRefreshLayoutRefreshing);
            set.Bind(_refreshControl).For(v => v.RefreshCommand).To(vm => vm.UpdateDataCommand);
            set.Bind(YourNetAvailableDoneLabel).For(v => v.Hidden).To(vm => vm.IsNetAvailable);
            set.Apply();
        }
        #endregion Methods
    }
}