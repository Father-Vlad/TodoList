using MvvmCross.ViewModels;
using Xamarin.Essentials;

namespace TodoList.Core.ViewModels
{
    public abstract class BaseViewModel<T> : MvxViewModel<T>
    {
        #region Variables
        private bool _isNetAvailable;
        #endregion Variables

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

            set
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
