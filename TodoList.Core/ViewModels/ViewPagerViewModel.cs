using MvvmCross;
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
    public class ViewPagerViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private ILoginService _loginService;
        private bool _isNetAvailable;

        public ViewPagerViewModel(IMvxNavigationService navigationService, ILoginService loginService)
        {
            _navigationService = navigationService;
            _loginService = loginService;
            CompletedGoalsViewModel = Mvx.IoCProvider.IoCConstruct<CompletedGoalsViewModel>();
            UncompletedGoalsViewModel = Mvx.IoCProvider.IoCConstruct<UncompletedGoalsViewModel>();
            LogoutCommand = new MvxAsyncCommand(Logout);
            FillingDataActivityCommand = new MvxAsyncCommand<Goal>(CreateNewGoal);
            ShowCompletedGoalsViewModelCommand = new MvxAsyncCommand<Action>(async (logoutHandler) => await _navigationService.Navigate<CompletedGoalsViewModel, Action>(logoutHandler));
            ShowUncompletedGoalsViewModelCommand = new MvxAsyncCommand<Action>(async (logoutHandler) => await _navigationService.Navigate<UncompletedGoalsViewModel, Action>(logoutHandler));
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                IsNetAvailable = true;
            }
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        public CompletedGoalsViewModel CompletedGoalsViewModel { get; set; }
        public UncompletedGoalsViewModel UncompletedGoalsViewModel { get; set; }
        public IMvxCommand LogoutCommand { get; set; }
        public IMvxCommand<Goal> FillingDataActivityCommand { get; set; }
        public IMvxAsyncCommand<Action> ShowCompletedGoalsViewModelCommand { get; private set; }
        public IMvxAsyncCommand<Action> ShowUncompletedGoalsViewModelCommand { get; private set; }

        private async Task Logout()
        {
            _loginService.LogoutFacebook();
            await _navigationService.Navigate<LoginViewModel>();
            await _navigationService.Close(this);
        }

        public async Task CreateNewGoal(Goal goal)
        {
            var result = await _navigationService.Navigate<FillingGoalDataViewModel, Goal>(goal);
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                IsNetAvailable = true;
                return;
            }
            IsNetAvailable = false;
        }

        public bool IsNetAvailable
        {
            get
            {
                return _isNetAvailable;
            }

            set
            {
                _isNetAvailable = value;
                RaisePropertyChanged(() => IsNetAvailable);
            }
        }
    }
}