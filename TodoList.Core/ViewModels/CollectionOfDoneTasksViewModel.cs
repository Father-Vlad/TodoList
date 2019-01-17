using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Core.ViewModels
{
    public class CollectionOfDoneTasksViewModel : MvxViewModel
    {
        private bool _isRefreshLayoutRefreshing;
        private MvxObservableCollection<Goal> _goals;
        private MvxCommand _updateDataCommand;
        private ITaskService _taskService;
        private ILoginService _loginService;

        public CollectionOfDoneTasksViewModel(ITaskService taskService, ILoginService loginService)
        {
            _taskService = taskService;
            _loginService = loginService;
            Goals = new MvxObservableCollection<Goal>();
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            User user = _loginService.CurrentUser;
            var list = _taskService.GetUserGoal(user.UserId);
            Goals = new MvxObservableCollection<Goal>(list);
            RaisePropertyChanged(() => Goals);
        }
        
        public MvxObservableCollection<Goal> Goals
        {
            get
            {
                return _goals;
            }
            set
            {
                _goals = value;
                RaisePropertyChanged(() => Goals);
            }
        }
        
        public MvxCommand UpdateDataCommand
        {
            get
            {
                return _updateDataCommand = _updateDataCommand ?? new MvxCommand(UpdateDataFromDB);
            }
        }

        private void UpdateDataFromDB()
        {
            IsRefreshLayoutRefreshing = true;
            User user = _loginService.CurrentUser;
            var list = _taskService.GetUserGoal(user.UserId);
            Goals = new MvxObservableCollection<Goal>(list);
            IsRefreshLayoutRefreshing = false;
        }

        public bool IsRefreshLayoutRefreshing
        {
            get
            {
                return _isRefreshLayoutRefreshing;
            }

            set
            {
                _isRefreshLayoutRefreshing = value;
                RaisePropertyChanged(() => IsRefreshLayoutRefreshing);
            }
        }
    }
}
