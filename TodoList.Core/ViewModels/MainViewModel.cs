using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
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
            ShowViewPagerViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ViewPagerViewModel>());
        }

        public IMvxAsyncCommand ShowCurrentViewModelCommand { get; set; }
        public IMvxAsyncCommand ShowLoginViewModelCommand { get; set; }
        public IMvxAsyncCommand ShowViewPagerViewModelCommand { get; set; }

        private async Task ShowCurrentViewModel()
        {
            if (_loginService.CurrentUserId == string.Empty)
            {
                ShowLoginViewModelCommand.Execute();
                return;
            }
            ShowViewPagerViewModelCommand.Execute();
        }
    }
}