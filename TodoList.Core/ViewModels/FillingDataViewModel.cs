using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Core.ViewModels
{
    public class FillingDataViewModel : MvxViewModel<Goal>
    {
        private string _goalName;
        private string _goalDescription;
        private bool _goalStatus = false;
        private bool _goalNameEnableStatus;
        private readonly IMvxNavigationService _navigationService;
        private ITaskService _taskService;
        private int _goalId;
        private bool _saveButtonEnableStatus = false;

        public FillingDataViewModel(IMvxNavigationService navigationService, ITaskService taskService)
        {
            _navigationService = navigationService;
            _taskService = taskService;
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
                RaisePropertyChanged(() => SaveButtonEnableStatus);
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

        public bool GoalNameEnableStatus
        {
            get
            {
                return _goalNameEnableStatus;
            }

            set
            {
                _goalNameEnableStatus = value;
                RaisePropertyChanged(() => GoalNameEnableStatus);
            }
        }

        public bool SaveButtonEnableStatus
        {
            get
            {
                if (GoalName == null | GoalName == string.Empty)
                {
                    return _saveButtonEnableStatus = false;
                }
                return _saveButtonEnableStatus = true;
            }

            set
            {
                _saveButtonEnableStatus = value;
                RaisePropertyChanged(() => SaveButtonEnableStatus);
            }
        }

        public MvxAsyncCommand SendBackCommand
        {
            get
            {
                return new MvxAsyncCommand(NavigateToPreviousActivity);
            }
        }

        private async Task NavigateToPreviousActivity()
        {
            await _navigationService.Close(this);
        }

        public MvxAsyncCommand SaveDataCommand
        {
            get
            {
                return new MvxAsyncCommand(SaveDataToDB);
            }
        }

        private async Task SaveDataToDB()
        {
            Goal goal = new Goal(GoalId, GoalName, GoalDescription, GoalStatus);
            _taskService.InsertGoal(goal);
            await _navigationService.Close(this);
        }

        public MvxAsyncCommand DeleteDataCommand
        {
            get
            {
                return new MvxAsyncCommand(DeleteDataFromDB);
            }
        }

        private async Task DeleteDataFromDB()
        {
            var position = GoalId;
            _taskService.DeleteGoal(position);
            await _navigationService.Close(this);
        }

        public override void Prepare(Goal parameter)
        {
            if (parameter != null)
            {
                GoalNameEnableStatus = false;
                GoalId = parameter.Id;
                GoalName = parameter.GoalName;
                GoalDescription = parameter.GoalDescription;
                GoalStatus = parameter.GoalStatus;
                return;
            }
            GoalNameEnableStatus = true;
        }
    }
}
