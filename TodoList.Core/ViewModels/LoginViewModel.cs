using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;

namespace TodoList.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private string _userId = string.Empty;
        private readonly IMvxNavigationService _navigationService;

        public LoginViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            CollectionActivityCommand = new MvxAsyncCommand(CreateNewGoal);
        }

        public IMvxCommand CollectionActivityCommand { get; set; }

        public async Task CreateNewGoal()
        {
            if (UserId != string.Empty)
            {
                var result = await _navigationService.Navigate<CollectionViewModel>();
            }
        }

        public string UserId
        {
            get
            {
                return _userId;
            }

            set
            {
                _userId = value;
                RaisePropertyChanged(() => UserId);
            }
        }

    }
}
