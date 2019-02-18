using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TodoList.Core.Interfaces;

namespace TodoList.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        #region Variables
        private readonly IMvxNavigationService _navigationService;
        private readonly ILoginService _loginService;
        #endregion Variables

        #region Constructors
        public MainViewModel(IMvxNavigationService navigationService, ILoginService loginService)
        {
            _navigationService = navigationService;
            _loginService = loginService;
            ShowCurrentViewModelCommand = new MvxCommand(ShowCurrentViewModel);
            ShowLoginViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LoginViewModel>());
            ShowViewPagerViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ViewPagerViewModel>());
        }
        #endregion Constructors

        #region Lifecycle
        #endregion Lifecycle

        #region Properties
        public IMvxCommand ShowCurrentViewModelCommand { get; set; }
        public IMvxAsyncCommand ShowLoginViewModelCommand { get; set; }
        public IMvxAsyncCommand ShowViewPagerViewModelCommand { get; set; }
        #endregion Properties

        #region Commands
        #endregion Commands

        #region Methods
        private void ShowCurrentViewModel()
        {
            if (_loginService.CurrentUserId == string.Empty)
            {
                ShowLoginViewModelCommand.Execute();
                return;
            }
            ShowViewPagerViewModelCommand.Execute();
        }
        #endregion Methods
    }
}