using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using TodoList.Core.Helper;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;
using TodoList.Core.MvxInteraction;
using Xamarin.Auth;

namespace TodoList.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel<object>
    {
        #region Variables
        private readonly string _strLoginWelcomeTextLoggedIn = "Please login to continue";
        private readonly string _strLoginWelcomeTextLoggedOut = "Welcome";
        private readonly string _strLogInButtonText = "   Continue with Facebook   ";
        private readonly string _strLoggedOutButtonText = "   Logged out   ";
        private string _userId;
        private string _userName;
        private bool _continueButtonStatus;
        private string _welcomeText;
        private string _loginButtonText;
        private bool _ProfilePictureViewVisibleStatus = false;
        private readonly IUserService _userService;
        #endregion Variables

        #region Constructors
        public LoginViewModel(IMvxNavigationService navigationService, ILoginService loginService, IUserService userService) : base(navigationService, loginService)
        {
            _loginService.OnLoggedInHandler = new Action(() => FillingLoginUserDataCommand.Execute());
            _loginService.OnLoggedOutHandler = new Action(() => DeleteLoginUserDataCommand.Execute());
            _userService = userService;
            NavigateToCollectionFragmentCommand = new MvxAsyncCommand(LookAtCurrentGoals);
            FillingLoginUserDataCommand = new MvxCommand(FillingLoginUserData);
            DeleteLoginUserDataCommand = new MvxAsyncCommand(DeleteLoginUserData);
            LoginFacebookCommand = new MvxCommand(LoginFacebook);
            LogoutFacebookCommand = new MvxCommand(LogoutFacebook);
        }
        #endregion Constructors

        #region Properties
        public MvxInteraction<CloseUIViewController> Interaction { get; set; } = new MvxInteraction<CloseUIViewController>();

        public string WelcomeText
        {
            get
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    return _welcomeText = _strLoginWelcomeTextLoggedIn;
                }
                return _welcomeText = _strLoginWelcomeTextLoggedOut;
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
        #endregion Properties

        #region Commands
        public IMvxCommand FillingLoginUserDataCommand { get; set; }
        public IMvxCommand DeleteLoginUserDataCommand { get; set; }
        public IMvxCommand NavigateToCollectionFragmentCommand { get; set; }
        public IMvxCommand LoginFacebookCommand { get; set; }
        public IMvxCommand LogoutFacebookCommand { get; set; }
        #endregion Commands

        #region Methods
        public async Task LookAtCurrentGoals()
        {
            var result = await _navigationService.Navigate<ViewPagerViewModel>();
            await _navigationService.Close(this);
        }

        private void LoginFacebook()
        {
            _loginService.LoginFacebook();
        }

        private void LogoutFacebook()
        {
            _loginService.LogoutFacebook();
        }

        public void FillingLoginUserData()
        {
            User user = new User { UserId = CurrentUser.GetCurrentUserId(), UserName = CurrentUser.GetCurrentUserName() };
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
            var findUser = _userService.GetUser(user.UserId);
            if (findUser == null)
            {
                _userService.InsertUser(user);
            }
        }
        #endregion Methods
    }
}