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
        private readonly string _strLogin = "\nWelcome\n";
        private readonly string _strLoggedOut = "Welcome to To-do List app. Please login to continue.";
        private readonly string _strUserName = "What is your name";
        private string _userId = string.Empty;
        private string _userName;
        private bool _continueButtonStatus = true;
        private string _welcomeText;
        private string _url = "   Continue with Facebook";
        private string _urlId;
        private bool _vMProperty = false;
        private readonly IMvxNavigationService _navigationService;
        private readonly ITaskService _taskService;
        public IMvxCommand NavigateToCollectionFragmentCommand { get; set; }

        public LoginViewModel(IMvxNavigationService navigationService, ITaskService taskService)
        {
            UserName = _strUserName;
            WelcomeText = _strLoggedOut;
            _navigationService = navigationService;
            _taskService = taskService;
            NavigateToCollectionFragmentCommand = new MvxAsyncCommand(LookAtCurrentGoals);
        }

        public override void ViewAppearing()
        {
            CurrentUser.CurrentUserId = _taskService.GetLastUser() ?? string.Empty;
            if (CurrentUser.CurrentUserId == string.Empty)
            {
                return;
            }
            WelcomeText = _strLogin;
            User user = _taskService.GetUser(CurrentUser.CurrentUserId);
            if (user != null)
            {
                UserId = user.UserId;
                UserName = user.UserName;
            }
        }

        public bool VMProperty
        {
            get
            {
                if (string.IsNullOrEmpty(UrlId))
                {
                    return _vMProperty = false;
                }
                return _vMProperty = true;
            }

            set
            {
                _vMProperty = value;
                RaisePropertyChanged(() => VMProperty);
            }
        }

        public string Url
        {
            get
            {
                return _url;
            }

            set
            {
                _url = value;
                RaisePropertyChanged(() => Url);
            }
        }

        public string UrlId
        {
            get
            {
                return _urlId;
            }

            set
            {
                _urlId = value;
                RaisePropertyChanged(() => UrlId);
                RaisePropertyChanged(() => VMProperty);
            }
        }

        public async Task LookAtCurrentGoals()
        {
            var result = await _navigationService.Navigate<CollectionViewModel>();
        }

        public string GetCurrentUserId()
        {
            return _taskService.GetLastUser();
        }

        public void CreateNewUser(string userId, string userName)
        {
            WelcomeText = _strLogin;
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
            UserName = _strUserName;
            WelcomeText = _strLoggedOut;
        }

        public bool ContinueButtonEnableStatus
        {
            get
            {
                /*if (UserId == null || UserId == string.Empty)
                {
                    return _continueButtonStatus = false;
                }*/
                return _continueButtonStatus = true;
            }

            set
            {
                _continueButtonStatus = value;
                RaisePropertyChanged(() => ContinueButtonEnableStatus);
            }
        }

        public string WelcomeText
        {
            get
            {
                return _welcomeText;
            }

            set
            {
                _welcomeText = value;
                RaisePropertyChanged(() => WelcomeText);
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