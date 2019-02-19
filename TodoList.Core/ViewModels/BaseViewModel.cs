using MvvmCross.ViewModels;
using Xamarin.Essentials;

namespace TodoList.Core.ViewModels
{
    public abstract class BaseViewModel<T> : MvxViewModel<T>
    {
        private bool _isNetAvailable;

        public void Connectivity_ConnectivityChanged()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                IsNetAvailable = true;
                return;
            }
            IsNetAvailable = false;
        }

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

        public override void Prepare(T parameter)
        {
        }
    }
}
