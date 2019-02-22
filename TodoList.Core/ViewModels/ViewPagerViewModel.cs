using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Core.ViewModels
{
    public class ViewPagerViewModel : BaseViewModel<object>
    {
        #region Variables
        ILoginService _loginService;
        #endregion Variables
        #region Constructors
        public ViewPagerViewModel(IMvxNavigationService navigationService, ILoginService loginService) : base(navigationService)
        {
            _loginService = loginService;
            CompletedGoalsViewModel = Mvx.IoCProvider.IoCConstruct<CompletedGoalsViewModel>();
            UncompletedGoalsViewModel = Mvx.IoCProvider.IoCConstruct<UncompletedGoalsViewModel>();
            LogoutCommand = new MvxAsyncCommand(Logout);
            FillingDataActivityCommand = new MvxAsyncCommand<Goal>(CreateNewGoal);
            ShowCompletedGoalsViewModelCommand = new MvxAsyncCommand<Action>(async (logoutHandler) => await _navigationService.Navigate<CompletedGoalsViewModel, Action>(logoutHandler));
            ShowUncompletedGoalsViewModelCommand = new MvxAsyncCommand<Action>(async (logoutHandler) => await _navigationService.Navigate<UncompletedGoalsViewModel, Action>(logoutHandler));
        }
        #endregion Constructors

        #region Properties
        public CompletedGoalsViewModel CompletedGoalsViewModel { get; set; }
        public UncompletedGoalsViewModel UncompletedGoalsViewModel { get; set; }
        #endregion Properties

        #region Commands
        public IMvxCommand LogoutCommand { get; set; }
        public IMvxCommand<Goal> FillingDataActivityCommand { get; set; }
        public IMvxAsyncCommand<Action> ShowCompletedGoalsViewModelCommand { get; private set; }
        public IMvxAsyncCommand<Action> ShowUncompletedGoalsViewModelCommand { get; private set; }
        #endregion Commands

        #region Methods
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
        #endregion Methods
    }
}