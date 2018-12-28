using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Core.ViewModels
{
    public class CollectionViewModel : MvxViewModel
    {
        private bool _isRefreshing;
        private MvxObservableCollection<Goal> _goals;
        private MvxCommand _reloadCommand;
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
            NextActivityCommand = new MvxAsyncCommand<Goal>(NewGoalMethod);
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            var list = _taskService.GetAllGoals();
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

        public IMvxCommand<Goal> NextActivityCommand { get; set; }

        public async Task NewGoalMethod(Goal goal)
        {
            var result = await _navigationService.Navigate<FillingDataViewModel, Goal>(goal);
        }

        public MvxCommand ReloadCommand
        {
            get
            {
                return _reloadCommand = _reloadCommand ?? new MvxCommand(ReloadCommandMethod);
            }
        }

        private void ReloadCommandMethod()
        {
            IsRefreshing = true;
            var list = _taskService.GetAllGoals();
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
