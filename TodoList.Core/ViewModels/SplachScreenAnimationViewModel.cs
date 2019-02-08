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
            FinishAnimationCommand = new MvxCommand(() => FinishAnimation());
        }

        public IMvxCommand FinishAnimationCommand { get; set; }

        private void FinishAnimation()
        {
            //await _navigationService.Close(this);
             _navigationService.Navigate<MainViewModel>();
             _navigationService.Close(this);

            //???
        }
    }
}