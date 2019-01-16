using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TodoList.Core.Interfaces;

namespace TodoList.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ILoginService _loginService;

        public MainViewModel(IMvxNavigationService navigationService, ILoginService loginService)
        {
            _navigationService = navigationService;
            _loginService = loginService;
            ShowCurrentViewModelCommand = new MvxAsyncCommand(ShowCurrentViewModel);
            ShowLoginViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LoginViewModel>());
            ShowCollectionViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<CollectionViewModel>());
        }

        public IMvxAsyncCommand ShowCurrentViewModelCommand { get; set; }
        public IMvxAsyncCommand ShowLoginViewModelCommand { get; set; }
        public IMvxAsyncCommand ShowCollectionViewModelCommand { get; set; }

        private async Task ShowCurrentViewModel()
        {
            if (_loginService.CurrentUserAccount == null)
            {
                ShowLoginViewModelCommand.Execute();
                return;
            }
            ShowCollectionViewModelCommand.Execute();
        }
    }
}