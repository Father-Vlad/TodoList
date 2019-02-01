using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;
using TodoList.Core.MvxInteraction;
using Xamarin.Auth;



namespace TodoList.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private readonly string _strLoginWelcomeText = "Please login to continue.";
        private readonly string _strLogInButtonText = "   Continue with Facebook";
        private readonly string _strLoggedOutButtonText = "   Logged out";
        private string _userId;
        private string _userName;
        private bool _continueButtonStatus;
        private string _welcomeText;
        private string _loginButtonText;
        private bool _ProfilePictureViewVisibleStatus = false;
        private readonly IMvxNavigationService _navigationService;
        private readonly ITaskService _taskService;
        private readonly ILoginService _loginService;

        public LoginViewModel(IMvxNavigationService navigationService, ITaskService taskService, ILoginService loginService)
        {
            _loginService = loginService;
            _loginService.OnLoggedInHandler = new Action(() => FillingLoginUserDataCommand.Execute());
            _loginService.OnLoggedOutHandler = new Action(() => DeleteLoginUserDataCommand.Execute());
            _navigationService = navigationService;
            _taskService = taskService;
            NavigateToCollectionFragmentCommand = new MvxAsyncCommand(LookAtCurrentGoals);
            NavigateToLoginFragmentCommand = new MvxAsyncCommand(LookAtLoginScreen);
            FillingLoginUserDataCommand = new MvxCommand(FillingLoginUserData);
            DeleteLoginUserDataCommand = new MvxAsyncCommand(DeleteLoginUserData);
        }

        public IMvxCommand FillingLoginUserDataCommand { get; set; }

        public IMvxCommand DeleteLoginUserDataCommand { get; set; }

        public IMvxCommand NavigateToCollectionFragmentCommand { get; set; }
        public IMvxCommand NavigateToLoginFragmentCommand { get; set; }

        public IMvxCommand LoginFacebookCommand => new MvxCommand(_loginService.LoginFacebook);

        public IMvxCommand LogoutFacebookCommand => new MvxCommand(_loginService.LogoutFacebook);

        public MvxInteraction<CloseUIViewController> Interaction { get; set; } = new MvxInteraction<CloseUIViewController>();

        public async Task LookAtCurrentGoals()
        {
            var result = await _navigationService.Navigate<ViewPagerViewModel>();
        }

        public async Task LookAtLoginScreen()
        {
            var result = await _navigationService.Navigate<LoginViewModel>();
        }

        public void FillingLoginUserData()
        {
            User user = _loginService.CurrentUser;
            var list = _taskService.GetAllUsers();
            CreateNewUser(user);
            UserId = user.UserId;
            UserName = user.UserName;
            var request = new CloseUIViewController();
            Interaction.Raise(request);
        }

        public async Task DeleteLoginUserData()
        {
            await Task.Run(() =>
            {
                UserId = string.Empty;
                UserName = string.Empty;
            });
        }

        public void CreateNewUser(User user)
        {
            var findUser = _taskService.GetUser(user.UserId);
            if (findUser == null)
            {
                _taskService.InsertUser(user);
            }
        }

        public string WelcomeText
        {
            get
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    return _welcomeText = _strLoginWelcomeText;
                }
                return _welcomeText = string.Empty;
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
                RaiseAllPropertiesChanged();
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

        public string LoginButtonText
        {
            get
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    return _loginButtonText = _strLogInButtonText;
                }
                return _loginButtonText = _strLoggedOutButtonText;
            }

            set
            {
                _loginButtonText = value;
                RaisePropertyChanged(() => LoginButtonText);
            }
        }

        public bool ProfilePictureViewVisibleStatus
        {
            get
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    return _ProfilePictureViewVisibleStatus = false;
                }
                return _ProfilePictureViewVisibleStatus = true;
            }

            set
            {
                _ProfilePictureViewVisibleStatus = value;
                RaisePropertyChanged(() => ProfilePictureViewVisibleStatus);
            }
        }

        public bool ContinueButtonEnableStatus
        {
            get
            {
                if (string.IsNullOrEmpty(UserId))
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

        public OAuth2Authenticator Authenticator
        {
            get
            {
                return _loginService.Authenticator();
            }
        }
    }
}