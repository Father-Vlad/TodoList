using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;
using TodoList.Core.Services;

namespace TodoList.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private string _userId = string.Empty;
        private string _userName = "User Name";
        private bool _continueButtonStatus = false;
        private readonly IMvxNavigationService _navigationService;
        private readonly ITaskService _taskService;
        public IMvxCommand CollectionActivityCommand { get; set; }

        public LoginViewModel(IMvxNavigationService navigationService, ITaskService taskService)
        {
            _navigationService = navigationService;
            _taskService = taskService;
            CollectionActivityCommand = new MvxAsyncCommand(LookAtCurrentGoals);
        }

        public override void ViewAppearing()
        {
            CurrentUser.CurrentUserId = _taskService.GetLastUser() ?? string.Empty;
            if (CurrentUser.CurrentUserId == string.Empty)
            {
                return;
            }
            User user = _taskService.GetUser(CurrentUser.CurrentUserId);
            if (user != null)
            {
                UserId = user.UserId;
                UserName = user.UserName;
            }
        }

        public async Task LookAtCurrentGoals()
        {
            var result = await _navigationService.Navigate<CollectionViewModel>();
        }

        public void CreateNewUser(string userId, string userName)
        {
            UserId = userId;
            UserName = userName;
            User user = new User(UserId, UserName);
            _taskService.InsertUser(user);
            _taskService.InsertOrReplaceLastUser(new LastUser(user.UserId));
            CurrentUser.CurrentUserId = _taskService.GetLastUser();
        }

        public void LogOut()
        {
            UserId = string.Empty;
            UserName = "User Name";
        }

        public bool ContinueButtonEnableStatus
        {
            get
            {
                if (UserId == null || UserId == string.Empty)
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

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }
    }
}