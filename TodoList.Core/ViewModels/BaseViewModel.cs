using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TodoList.Core.Interfaces;
using Xamarin.Essentials;

namespace TodoList.Core.ViewModels
{
    public abstract class BaseViewModel<T> : MvxViewModel<T>
    {
        #region Variables
        private bool _isNetAvailable;
        protected readonly IMvxNavigationService _navigationService;
        protected readonly ILoginService _loginService;
        protected readonly IWebApiService _webApiService;
        protected readonly ITelegramService _telegramService;
        protected readonly IGoalService _goalService;
        protected readonly IAlertService _alertService;

        #endregion Variables

        #region Constructors
        protected BaseViewModel()
        {
        }

        protected BaseViewModel(IMvxNavigationService navigationService) : this()
        {
            _navigationService = navigationService;
        }

        protected BaseViewModel(IMvxNavigationService navigationService, ILoginService loginService) : this(navigationService)
        {
            _loginService = loginService;
            Connectivity_ConnectivityChanged();
            Connectivity.ConnectivityChanged += (sender,e) => { Connectivity_ConnectivityChanged(); };
        }

        protected BaseViewModel(IMvxNavigationService navigationService, ILoginService loginService, IWebApiService webApiService) : this(navigationService, loginService)
        {
            _webApiService = webApiService;
        }

        protected BaseViewModel(IMvxNavigationService navigationService, ILoginService loginService, IWebApiService webApiService, ITelegramService telegramService, IGoalService goalService, IAlertService alertService) : this(navigationService, loginService, webApiService)
        {
            _telegramService = telegramService;
            _goalService = goalService;
            _alertService = alertService;
        }
        #endregion Constructors

        #region Lifecycle
        public override void Prepare(T parameter)
        {
        }
        #endregion Lifecycle

        #region Properties
        public bool IsNetAvailable
        {
            get
            {
                return _isNetAvailable;
            }

            protected set
            {
                _isNetAvailable = value;
                RaisePropertyChanged(() => IsNetAvailable);
            }
        }
        #endregion Properties

        #region Methods
        protected void Connectivity_ConnectivityChanged()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                IsNetAvailable = true;
                return;
            }
            IsNetAvailable = false;
        }
        #endregion Methods
    }
}
