using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Core.ViewModels
{
    public class CollectionOfDoneTasksViewModel : MvxViewModel<Action>
    {
        private bool _isRefreshLayoutRefreshing;
        private MvxObservableCollection<Goal> _goals;
        private MvxCommand _updateDataCommand;
        private ITaskService _taskService;
        private ILoginService _loginService;
        private readonly IMvxNavigationService _navigationService;
        private readonly IShareTextToTelegramService _shareTextToTelegramService;
        private readonly string _toastMessage = "Telegram is not installed";
        private readonly string _tabCellMessage = "tg://msg?text=";
        private readonly string _shareText = "Hi, I created a new task for myself with To-do List app.\nThe name of task is: ";
        private readonly string _goalDone = "\nIt’s done :)";
        private readonly string _goalNotDone = "\nIt’s not done yet :(";
        private string _shareString = string.Empty;

        public CollectionOfDoneTasksViewModel(IMvxNavigationService navigationService, ITaskService taskService, ILoginService loginService, IShareTextToTelegramService shareTextToTelegramService)
        {
            _navigationService = navigationService;
            _taskService = taskService;
            _loginService = loginService;
            _shareTextToTelegramService = shareTextToTelegramService;
            Goals = new MvxObservableCollection<Goal>();
            FillingDataActivityCommand = new MvxAsyncCommand<Goal>(CreateNewGoal);
            ShareMessageCommand = new MvxCommand<int>(ShareMessege);
        }

        public IMvxCommand<Goal> FillingDataActivityCommand { get; set; }
        public IMvxCommand LogoutCommand => new MvxCommand(Logout);
        public Action OnLoggedOutHandler { get; set; }
        public IMvxCommand<int> ShareMessageCommand { get; set; }

        private void Logout()
        {
            OnLoggedOutHandler();
        }
        public override void Prepare(Action action)
        {
            base.Prepare();
            OnLoggedOutHandler = action;
        }
        public override void ViewAppearing()
        {
            base.ViewAppearing();
            MakeListOfGoals();
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

        public async Task CreateNewGoal(Goal goal)
        {
            var result = await _navigationService.Navigate<FillingDataViewModel, Goal>(goal);
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
            MakeListOfGoals();
            IsRefreshLayoutRefreshing = false;
        }

        private void MakeListOfGoals()
        {
            User user = _loginService.CurrentUser;
            var list = _taskService.GetDoneUserGoal(user.UserId);
            Goals = new MvxObservableCollection<Goal>(list);
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

        private void ShareMessege(int currentTaskId)
        {
            _shareString += _tabCellMessage;
            _shareString += _shareText;
            Goal currentGoal = _taskService.CurrentGoal(currentTaskId);
            if (currentGoal.GoalStatus)
            {
                _shareString += _goalDone;
            }
            if (currentGoal.GoalStatus == false)
            {
                _shareString += _goalNotDone;
            }
            _shareTextToTelegramService.ShareText(_shareString, _toastMessage);
        }
    }
}
