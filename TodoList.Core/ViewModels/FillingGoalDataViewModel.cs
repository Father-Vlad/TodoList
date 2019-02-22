using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using TodoList.Core.Helper;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Core.ViewModels
{
    public class FillingGoalDataViewModel : BaseViewModel<Goal>
    {
        #region Variables
        ILoginService _loginService;
        IWebApiService _webApiService;
        private string _goalName;
        private string _goalDescription;
        private bool _goalStatus = false;
        private bool _goalNameEnableStatus;
        private int _goalId;
        private bool _saveButtonEnableStatus = false;
        private string _userId;
        private string _deleteCanselButtonText;
        #endregion Variables

        #region Constructors
        public FillingGoalDataViewModel(IMvxNavigationService navigationService, ILoginService loginService, IWebApiService webApiService) : base(navigationService)
        {
            _loginService = loginService;
            _webApiService = webApiService;
        }
        #endregion Constructors

        #region Lifecycle
        public override void Prepare(Goal parameter)
        {
            if (parameter != null)
            {
                GoalNameEnableStatus = false;
                GoalId = parameter.Id;
                GoalName = parameter.GoalName;
                GoalDescription = parameter.GoalDescription;
                GoalStatus = parameter.GoalStatus;
                UserId = parameter.UserId;
                return;
            }
            GoalNameEnableStatus = true;
        }
        #endregion Lifecycle

        #region Properties
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
                if (GoalName == null | GoalName.Trim() == string.Empty)
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

        public string DeleteCanselButtonText
        {
            get
            {
                if (GoalName == null)
                {
                    return _deleteCanselButtonText = "Cancel";
                }
                return _deleteCanselButtonText = "Delete";
            }

            set
            {
                _deleteCanselButtonText = value;
                RaisePropertyChanged(() => DeleteCanselButtonText);
            }
        }

        public string UserId
        {
            get
            {
                return _userId = CurrentUser.GetCurrentUserId();
            }

            set
            {
                _userId = value;
                RaisePropertyChanged(() => UserId);
            }
        }
        #endregion Properties

        #region Commands
        public MvxAsyncCommand SendBackCommand
        {
            get
            {
                return new MvxAsyncCommand(NavigateToPreviousActivity);
            }
        }

        public MvxAsyncCommand SaveDataCommand
        {
            get
            {
                return new MvxAsyncCommand(SaveDataToDB);
            }
        }

        public MvxAsyncCommand DeleteDataCommand
        {
            get
            {
                return new MvxAsyncCommand(DeleteDataFromDB);
            }
        }
        #endregion Commands

        #region Methods
        private async Task NavigateToPreviousActivity()
        {
            await _navigationService.Close(this);
        }

        private async Task SaveDataToDB()
        {
            if (IsNetAvailable)
            {
                Goal goal = new Goal(GoalId, GoalName.Trim(), GoalDescription, GoalStatus, UserId);
                await _webApiService.InsertOrUpdateDataAsync(goal);
                await _navigationService.Close(this);
            }
        }

        private async Task DeleteDataFromDB()
        {
            if (IsNetAvailable)
            {
                var position = GoalId;
                await _webApiService.DeleteDataAsync(position);
                await _navigationService.Close(this);
            }
        }
        #endregion Methods
    }
}