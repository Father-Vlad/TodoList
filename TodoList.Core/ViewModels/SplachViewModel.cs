using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace TodoList.Core.ViewModels
{
    public class SplachViewModel : BaseViewModel<object>
    {
        #region Constructors
        public SplachViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
            FinishAnimationCommand = new MvxAsyncCommand(() => FinishAnimation());
        }
        #endregion Constructors

        #region Commands
        public IMvxAsyncCommand FinishAnimationCommand { get; set; }
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