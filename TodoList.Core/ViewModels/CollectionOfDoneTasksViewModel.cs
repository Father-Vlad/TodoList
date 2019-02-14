using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;
using Xamarin.Essentials;

namespace TodoList.Core.ViewModels
{
    public class CollectionOfDoneTasksViewModel : MvxViewModel<Action>
    {
        private bool _isRefreshLayoutRefreshing;
        private MvxObservableCollection<Goal> _goals;
        private MvxCommand _updateDataCommand;
        private ITaskService _taskService;
        private ILoginService _loginService;
        private IMvxNavigationService _navigationService;
        private IShareTextToTelegramService _shareTextToTelegramService;
        private IWebApiService _webApiService;
        private readonly string _toastMessage = "Telegram is not installed";
        private readonly string _checkAppNameiOS = "tg://msg?text=";
        private readonly string _checkAppNameDroid = "org.telegram.messenger";
        private readonly string _newLineDroid = "\n";
        private readonly string _newLineiOS = "%0A";
        private readonly string _shareWelcomeText = "Hi, I created a new task for myself with To-do List app.";
        private readonly string _shareForewordText = "The name of the task is: ";
        private readonly string _goalDone = "It's done :)";
        private readonly string _goalNotDone = "It's not done yet :(";
        private bool _platformName;
        private int _currentTaskId;

        public CollectionOfDoneTasksViewModel(IMvxNavigationService navigationService, ITaskService taskService, ILoginService loginService, IShareTextToTelegramService shareTextToTelegramService, IWebApiService webApiService)
        {
            _navigationService = navigationService;
            _taskService = taskService;
            _loginService = loginService;
            _shareTextToTelegramService = shareTextToTelegramService;
            _webApiService = webApiService;
            Goals = new MvxObservableCollection<Goal>();
            FillingDataActivityCommand = new MvxAsyncCommand<Goal>(CreateNewGoal);
            ShareMessageCommand = new MvxCommand<int>(ShareMessege);
            _webApiService.OnRefreshDoneGoalsHandler = new Action(() => 
            {
                User user = _loginService.CurrentUser;
                var list = _taskService.GetDoneUserGoal(user.UserId);
                Goals = new MvxObservableCollection<Goal>(list);
            });
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
            var net = Connectivity.NetworkAccess;
            if (net == NetworkAccess.Internet)
            {
                _webApiService.RefreshDataAsync();
                return;
            }
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

        private int CurrentTaskId
        {
            get
            {
                return _currentTaskId;
            }

            set
            {
                _currentTaskId = value;
                RaisePropertyChanged(() => CurrentTaskId);
            }
        }

        private string CurrentTaskName { get; set; }

        private bool CurrentTaskStatus { get; set; }

        private string ShareTaskStatus
        {
            get
            {
                return CurrentTaskStatus ? _goalDone : _goalNotDone;
            }
        }

        public bool PlatformName // if true -> Android platform, if false -> iOS
        {
            get
            {
                return _platformName;
            }

            set
            {
                _platformName = value;
                RaisePropertyChanged<bool>(() => PlatformName);
            }
        }

        private string CurrentPlatformName
        {
            get
            {
                if (PlatformName)
                {
                    ShareNewLine = _newLineDroid;
                    return _checkAppNameDroid;
                }
                ShareNewLine = _newLineiOS;
                return _checkAppNameiOS;
            }
        }

        private string ShareNewLine { get; set; }

        private string GetIOSFormattingString(string stringToFormat)
        {
            string shareCurrentTaskName = string.Empty;
            for (int i = 0; i < stringToFormat.Length; i++)
            {
                if (stringToFormat[i] == ' ')
                {
                    shareCurrentTaskName += "%20";
                    continue;
                }
                shareCurrentTaskName += stringToFormat[i];
            }
            return shareCurrentTaskName;
        }

        private string ShareString
        {
            get
            {
                if (PlatformName)
                {
                    return string.Format(_shareWelcomeText + ShareNewLine + _shareForewordText + CurrentTaskName + ShareNewLine + ShareTaskStatus);
                }
                return GetIOSFormattingString(string.Format(_checkAppNameiOS + _shareWelcomeText + ShareNewLine + _shareForewordText + CurrentTaskName + ShareNewLine + ShareTaskStatus));
            }
        }

        private void ShareMessege(int currentTaskId)
        {
            Goal currentGoal = _taskService.GetCurrentGoal(currentTaskId);
            CurrentTaskId = currentGoal.Id;
            CurrentTaskName = currentGoal.GoalName;
            CurrentTaskStatus = currentGoal.GoalStatus;
            if (_shareTextToTelegramService.IsTheAppInstalled(CurrentPlatformName) == false)
            {
                _shareTextToTelegramService.ShowToastMessage(_toastMessage);
                return;
            }
            _shareTextToTelegramService.ShareText(ShareString);
        }
    }
}