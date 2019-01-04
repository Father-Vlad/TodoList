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
        private bool _isRefreshing;
        private MvxObservableCollection<Goal> _goals;
        private MvxCommand _updateDataCommand;
        private readonly IMvxNavigationService _navigationService;
        ITaskService _taskService;
               
        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public CollectionViewModel(IMvxNavigationService navigationService, ITaskService taskService)
        {
            _taskService = taskService;
            _navigationService = navigationService;
            Goals = new MvxObservableCollection<Goal>();
            FillingDataActivityCommand = new MvxAsyncCommand<Goal>(CreateNewGoal);
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            var list = _taskService.GetUserGoal(CurrentUser.UserId);
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

        public IMvxCommand<Goal> FillingDataActivityCommand { get; set; }

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
            IsRefreshing = true;
            var list = _taskService.GetUserGoal(CurrentUser.UserId);
            Goals = new MvxObservableCollection<Goal>(list);
            IsRefreshing = false;
        }

        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }

            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }
    }
}
