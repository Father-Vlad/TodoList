﻿using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using TodoList.Core.Helper;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Core.ViewModels
{
    public class CompletedGoalsViewModel : BaseViewModel<Action>
    {
        #region Variables
        ILoginService _loginService;
        IWebApiService _webApiService;
        ITelegramService _telegramService;
        IGoalService _goalService;
        IAlertService _alertService;
        private bool _isRefreshLayoutRefreshing;
        private MvxObservableCollection<Goal> _goals;
        private MvxCommand _updateDataCommand;
        private readonly string _alertErorMessage = "Something went wrong";
        private readonly string _alertMessage = "Telegram is not installed";
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
        #endregion Variables
        
        #region Constructors
        public CompletedGoalsViewModel(IMvxNavigationService navigationService, ILoginService loginService, IWebApiService webApiService, ITelegramService telegramService, IGoalService goalService, IAlertService alertService)
            : base(navigationService)
        {
            _loginService = loginService;
            _webApiService = webApiService;
            _telegramService = telegramService;
            _goalService = goalService;
            _alertService = alertService;
            Goals = new MvxObservableCollection<Goal>();
            FillingDataActivityCommand = new MvxAsyncCommand<Goal>(CreateNewGoal);
            ShareMessageCommand = new MvxCommand<int>(ShareMessege);
        }
        #endregion Constructors

        #region Lifecycle
        public override void Prepare(Action action)
        {
            base.Prepare();
            OnLoggedOutHandler = action;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            MakeListOfGoals();
        }
        #endregion Lifecycle

        #region Properties
        public Action OnLoggedOutHandler { get; set; }
        
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
        #endregion Properties

        #region Commands
        public IMvxCommand<Goal> FillingDataActivityCommand { get; set; }
        public IMvxCommand LogoutCommand => new MvxCommand(Logout);
        public IMvxCommand<int> ShareMessageCommand { get; set; }

        public MvxCommand UpdateDataCommand
        {
            get
            {
                return _updateDataCommand = _updateDataCommand ?? new MvxCommand(UpdateDataFromDB);
            }
        }
        #endregion Commands

        #region Methods
        private void Logout()
        {
            OnLoggedOutHandler();
        }

        public async Task CreateNewGoal(Goal goal)
        {
            var result = await _navigationService.Navigate<FillingGoalDataViewModel, Goal>(goal);
        }

        private void UpdateDataFromDB()
        {
            MakeListOfGoals();
        }

        private async void MakeListOfGoals()
        {
            IsRefreshLayoutRefreshing = true;
            LoadCacheOrUploadNewData();
            if (IsNetAvailable)
            {
                await _webApiService.RefreshDataAsync();
                LoadCacheOrUploadNewData();
            }
            IsRefreshLayoutRefreshing = false;
        }

        private void LoadCacheOrUploadNewData()
        {
            var list = _goalService.GetDoneUserGoal(CurrentUser.GetCurrentUserId());
            Goals = new MvxObservableCollection<Goal>(list);
        }

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

        private void ShareMessege(int currentTaskId)
        {
            Goal currentGoal = _goalService.GetCurrentGoal(currentTaskId);
            CurrentTaskId = currentGoal.Id;
            CurrentTaskName = currentGoal.GoalName;
            CurrentTaskStatus = currentGoal.GoalStatus;
            if (_telegramService.IsTheAppInstalled(CurrentPlatformName) == false)
            {
                _alertService.ShowToast(_alertMessage);
                return;
            }
            try
            {
                _telegramService.ShareText(ShareString);
            }
            catch
            {
                _alertService.ShowToast(_alertErorMessage);
            }
        }
        #endregion Methods
    }
}