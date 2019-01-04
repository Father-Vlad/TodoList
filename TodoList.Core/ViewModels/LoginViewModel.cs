using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private string _userId = string.Empty;
        private string _userFirstName;
        private string _userLastName;
        private string _userFullName = "User Name";
        private bool _continueButtonStatus = false;
        private readonly IMvxNavigationService _navigationService;
        private readonly ITaskService _taskService;

        public LoginViewModel(IMvxNavigationService navigationService, ITaskService taskService)
        {
            _navigationService = navigationService;
            _taskService = taskService;
            CollectionActivityCommand = new MvxAsyncCommand(CreateNewGoal);
        }

        public IMvxCommand CollectionActivityCommand { get; set; }

        public async Task CreateNewGoal()
        {
            var result = await _navigationService.Navigate<CollectionViewModel>();
        }

        public void CreateNewUser(string userId, string userFirstName, string userLastName)
        {
            UserId = userId;
            UserFirstName = userFirstName;
            UserLastName = userLastName;
            UserFullName = string.Format($"{UserFirstName} {UserLastName}");
            User user = new User(0, UserId, UserFirstName, UserLastName);
            _taskService.InsertUser(user);
        }

        public bool ContinueButtonEnableStatus
        {
            get
            {
                if (UserId == null | UserId == string.Empty)
                {
                    return _continueButtonStatus = false;
                }
                return _continueButtonStatus = true;
            }

            set
            {
                _continueButtonStatus = value;
                RaisePropertyChanged(() => ContinueButtonEnableStatus);
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
                RaisePropertyChanged(() => ContinueButtonEnableStatus);
            }
        }

        public string UserFirstName
        {
            get
            {
                return _userFirstName;
            }

            set
            {
                _userFirstName = value;
                RaisePropertyChanged(() => UserFirstName);
            }
        }

        public string UserLastName
        {
            get
            {
                return _userLastName;
            }

            set
            {
                _userLastName = value;
                RaisePropertyChanged(() => UserLastName);
            }
        }

        public string UserFullName
        {
            get
            {
                return _userFullName;
            }
            set
            {
                _userFullName = value;
                RaisePropertyChanged(() => UserFullName);
            }
        }
    }
}