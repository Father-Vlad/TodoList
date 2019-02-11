using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

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
            ShowCurrentViewModelCommand = new MvxCommand(ShowCurrentViewModel);
            ShowLoginViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LoginViewModel>());
            ShowViewPagerViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ViewPagerViewModel>());
            //ShowAnimationViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<SplachScreenAnimationViewModel>());
        }

        public IMvxCommand ShowCurrentViewModelCommand { get; set; }
        public IMvxAsyncCommand ShowLoginViewModelCommand { get; set; }
        public IMvxAsyncCommand ShowViewPagerViewModelCommand { get; set; }
        //public IMvxAsyncCommand ShowAnimationViewModelCommand { get; set; }

        private void ShowCurrentViewModel()
        {
            /*if (TimePresented.IsFirstTimePresented)
            {
                TimePresented.IsFirstTimePresented = false;
                ShowAnimationViewModelCommand.Execute();
                return;
            }*/

            if (_loginService.CurrentUserId == string.Empty)
            {
                ShowLoginViewModelCommand.Execute();
                return;
            }
            ShowViewPagerViewModelCommand.Execute();
        }
    }
}