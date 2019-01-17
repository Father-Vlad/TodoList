using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;
using TodoList.Core.Services;

namespace TodoList.Core.ViewModels
{
    public class CollectionViewModel : MvxViewModel
    {
        private bool _isRefreshLayoutRefreshing;
        private MvxObservableCollection<Goal> _goals;
        private MvxCommand _updateDataCommand;
        private readonly IMvxNavigationService _navigationService;
        private ITaskService _taskService;
        private ILoginService _loginService;

        public IMvxCommand LogoutCommand { get; set; }
        public IMvxCommand<Goal> FillingDataActivityCommand { get; set; }

        public CollectionViewModel(IMvxNavigationService navigationService, ITaskService taskService, ILoginService loginService)
        {
            _taskService = taskService;
            _navigationService = navigationService;
            _navigationService.BeforeClose += _navigationService_BeforeClose;
            _loginService = loginService;
            Goals = new MvxObservableCollection<Goal>();
            FillingDataActivityCommand = new MvxAsyncCommand<Goal>(CreateNewGoal);
            LogoutCommand = new MvxAsyncCommand(Logout);
        }

        private async Task Logout()
        {
            _loginService.LogoutFacebook();
            await _navigationService.Close(this);
        }

        private void _navigationService_BeforeClose(object sender, MvvmCross.Navigation.EventArguments.IMvxNavigateEventArgs e)
        {
            _navigationService.Navigate<LoginViewModel>();
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            User user = _loginService.CurrentUser;
            var list = _taskService.GetUserGoal(user.UserId);
            Goals = new MvxObservableCollection<Goal>(list);
            RaisePropertyChanged(() => Goals);
        }
        
        public MvxObservableCollection<Goal> Goals
        {
            get
            {
                return _goals;
            }
            set
            {
                _goals = value;
                RaisePropertyChanged(() => Goals);
            }
        }
        
        public async Task CreateNewGoal(Goal goal)
        {
            var result = await _navigationService.Navigate<FillingDataViewModel, Goal>(goal);
        }

        public MvxCommand UpdateDataCommand
        {
            get
            {
                return _updateDataCommand = _updateDataCommand ?? new MvxCommand(UpdateDataFromDB);
            }
        }

        private void UpdateDataFromDB()
        {
            IsRefreshLayoutRefreshing = true;
            User user = _loginService.CurrentUser;
            var list = _taskService.GetUserGoal(user.UserId);
            Goals = new MvxObservableCollection<Goal>(list);
            IsRefreshLayoutRefreshing = false;
        }

        public bool IsRefreshLayoutRefreshing
        {
            get
            {
                return _isRefreshLayoutRefreshing;
            }

            set
            {
                _isRefreshLayoutRefreshing = value;
                RaisePropertyChanged(() => IsRefreshLayoutRefreshing);
            }
        }
    }
}
