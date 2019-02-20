using MvvmCross.Commands;
using MvvmCross.Navigation;
using TodoList.Core.Helper;

namespace TodoList.Core.ViewModels
{
    public class MainViewModel : BaseViewModel<object>
    {
        #region Constructors
        public MainViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
            ShowCurrentViewModelCommand = new MvxCommand(ShowCurrentViewModel);
            ShowLoginViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LoginViewModel>());
            ShowViewPagerViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ViewPagerViewModel>());
        }
        #endregion Constructors

        #region Commands
        public IMvxCommand ShowCurrentViewModelCommand { get; set; }
        public IMvxAsyncCommand ShowLoginViewModelCommand { get; set; }
        public IMvxAsyncCommand ShowViewPagerViewModelCommand { get; set; }
        #endregion Commands

        #region Methods
        private void ShowCurrentViewModel()
        {
            if (CurrentUser.GetCurrentUserId() == string.Empty)
            {
                ShowLoginViewModelCommand.Execute();
                return;
            }
            ShowViewPagerViewModelCommand.Execute();
        }
        #endregion Methods
    }
}