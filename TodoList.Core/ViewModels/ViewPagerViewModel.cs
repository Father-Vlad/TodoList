using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Core.ViewModels
{
    public class ViewPagerViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private ILoginService _loginService;

        public ViewPagerViewModel(IMvxNavigationService navigationService, ILoginService loginService)
        {
            _navigationService = navigationService;
            _loginService = loginService;
            _navigationService.BeforeClose += _navigationService_BeforeClose;
            CollectionOfDoneTasksViewModelCommand = Mvx.IoCConstruct<CollectionOfDoneTasksViewModel>();
            CollectionOfNotDoneTasksViewModelCommand = Mvx.IoCConstruct<CollectionOfNotDoneTasksViewModel>();
            LogoutCommand = new MvxAsyncCommand(Logout);
            FillingDataActivityCommand = new MvxAsyncCommand<Goal>(CreateNewGoal);

            ShowCollectionOfDoneTasksViewModelCommand = new MvxAsyncCommand<Action>(async (logoutHandler) => await _navigationService.Navigate<CollectionOfDoneTasksViewModel, Action>(logoutHandler));
            ShowCollectionOfNotDoneTasksViewModelCommand = new MvxAsyncCommand<Action>(async (logoutHandler) => await _navigationService.Navigate<CollectionOfNotDoneTasksViewModel, Action>(logoutHandler));
        }

        public CollectionOfDoneTasksViewModel CollectionOfDoneTasksViewModelCommand { get; set; }
        public CollectionOfNotDoneTasksViewModel CollectionOfNotDoneTasksViewModelCommand { get; set; }
        public IMvxCommand LogoutCommand { get; set; }
        public IMvxCommand<Goal> FillingDataActivityCommand { get; set; }

        public IMvxAsyncCommand<Action> ShowCollectionOfDoneTasksViewModelCommand { get; private set; }
        public IMvxAsyncCommand<Action> ShowCollectionOfNotDoneTasksViewModelCommand { get; private set; }

        private async Task Logout()
        {
            _loginService.LogoutFacebook();
            await _navigationService.Close(this);
        }

        private void _navigationService_BeforeClose(object sender, MvvmCross.Navigation.EventArguments.IMvxNavigateEventArgs e)
        {
            _navigationService.Navigate<LoginViewModel>();
        }

        public async Task CreateNewGoal(Goal goal)
        {
            var result = await _navigationService.Navigate<FillingDataViewModel, Goal>(goal);
        }
    }
}