using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace TodoList.Core.ViewModels
{
    public class SplachViewModel : MvxViewModel
    {
        #region Variables
        private readonly IMvxNavigationService _navigationService;
        #endregion Variables

        #region Constructors
        public SplachViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            FinishAnimationCommand = new MvxAsyncCommand(() => FinishAnimation());
        }
        #endregion Constructors

        #region Lifecycle
        #endregion Lifecycle

        #region Properties
        public IMvxAsyncCommand FinishAnimationCommand { get; set; }
        #endregion Properties

        #region Commands
        #endregion Commands

        #region Methods
        private async Task FinishAnimation()
        {
            await _navigationService.Close(this);
            var result = await _navigationService.Navigate<MainViewModel>();
        }
        #endregion Methods
    }
}