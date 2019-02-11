using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace TodoList.Core.ViewModels
{
    public class SplachScreenAnimationViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public SplachScreenAnimationViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            FinishAnimationCommand = new MvxAsyncCommand(() => FinishAnimation());
        }

        public IMvxAsyncCommand FinishAnimationCommand { get; set; }

        private async Task FinishAnimation()
        {
            await _navigationService.Close(this);
            var result = await _navigationService.Navigate<MainViewModel>();
        }
    }
}