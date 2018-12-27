using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.Services;

namespace TodoList.Core.ViewModels
{
    public class FillingDataViewModel : MvxViewModel<Goal>
    {
        private string _goalName;
        private string _goalDescription;
        private bool _goalStatus = false;
        private bool _titleEnableStatus;
        private readonly IMvxNavigationService _navigationService;
        private ITaskService _taskService;
        private int _goalId;

        public FillingDataViewModel(IMvxNavigationService navigationService, ITaskService taskService)
        {
            _navigationService = navigationService;
            _taskService = taskService;
        }

        public bool TitleEnableStatus
        {
            get
            {
                return _titleEnableStatus;
            }

            set
            {
                _titleEnableStatus = value;
                RaisePropertyChanged(() => TitleEnableStatus);
            }
        }

        public int GoalId
        {
            get
            {
                return _goalId;
            }

            set
            {
                _goalId = value;
                RaisePropertyChanged(() => GoalId);
            }
        }

        public string GoalName
        {
            get
            {
                return _goalName;
            }

            set
            {
                _goalName = value;
                RaisePropertyChanged(() => GoalName);
            }
        }

        public string GoalDescription
        {
            get
            {
                return _goalDescription;
            }

            set
            {
                _goalDescription = value;
                RaisePropertyChanged(() => GoalDescription);
            }
        }

        public bool GoalStatus
        {
            get
            {
                return _goalStatus;
            }

            set
            {
                _goalStatus = value;
                RaisePropertyChanged(() => GoalStatus);
            }
        }

        public MvxAsyncCommand BackCommand
        {
            get
            {
                return new MvxAsyncCommand(BackMethod);
            }
        }


        private async Task BackMethod()
        {

            await _navigationService.Close(this);
        }

        public MvxAsyncCommand SaveCommand
        {
            get
            {
                return new MvxAsyncCommand(SaveMethod);
            }
        }

        private async Task SaveMethod()
        {
            if (GoalName != null)
            {
                Goal goal = new Goal(GoalId, GoalName, GoalDescription, GoalStatus);

                _taskService.InsertGoal(goal);
            }
            await _navigationService.Close(this);
        }

        public MvxAsyncCommand DeleteCommand
        {
            get
            {
                return new MvxAsyncCommand(DeleteMethod);
            }
        }

        private async Task DeleteMethod()
        {
            var position = GoalId;

            _taskService.DeleteGoal(position);

            await _navigationService.Close(this);
        }

        public override void Prepare(Goal parameter)
        {
            if (parameter != null)
            {
                TitleEnableStatus = false;
                GoalId = parameter.Id;
                GoalName = parameter.GoalName;
                GoalDescription = parameter.GoalDescription;
                GoalStatus = parameter.GoalStatus;
                return;
            }
            TitleEnableStatus = true;
        }
    }
}
