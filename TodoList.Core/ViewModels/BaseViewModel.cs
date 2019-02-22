using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Essentials;

namespace TodoList.Core.ViewModels
{
    public abstract class BaseViewModel<T> : MvxViewModel<T>
    {
        #region Variables
        private bool _isNetAvailable;
        protected readonly IMvxNavigationService _navigationService;
        #endregion Variables

        #region Constructors
        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        #endregion Constructors

        #region Lifecycle
        public override void ViewAppearing()
        { 
            base.ViewAppearing();
            Connectivity_ConnectivityChanged();
            Connectivity.ConnectivityChanged += (sender, e) => { Connectivity_ConnectivityChanged(); };
        }

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
