using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace TodoList.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowLoginViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LoginViewModel>());
            ShowCollectionViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<CollectionViewModel>());
            ShowFillingDataViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<FillingDataViewModel>());
        }

        public IMvxAsyncCommand ShowLoginViewModelCommand { get; set; }
        public IMvxAsyncCommand ShowCollectionViewModelCommand { get; set; }
        public IMvxAsyncCommand ShowFillingDataViewModelCommand { get; set; }
    }
}