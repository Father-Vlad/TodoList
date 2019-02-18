using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using System;
using TodoList.Core.Models;
using UIKit;

namespace TodoList.iOS.Views.Cells
{
    public partial class TaskViewCell : MvxTableViewCell
    {
        #region Variables
        public static readonly NSString Key = new NSString("TaskViewCell");
        public static readonly UINib Nib = UINib.FromName("TaskViewCell", NSBundle.MainBundle);
        #endregion Variables

        #region Constructors
        protected TaskViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set_1 = this.CreateBindingSet<TaskViewCell, Goal>();
                set_1.Bind(TaskNameLabel).To(vm => vm.GoalName);
                set_1.Bind(TaskStatusImageView).For(v => v.Image).To(vm => vm.GoalStatus).WithConversion("StatusImage");
                set_1.Apply();
            });
        }
        #endregion Constructors

        #region Lifecycle
        #endregion Lifecycle

        #region Properties
        public Action OnShareHandlerCell { get; set; }
        #endregion Properties

        #region Commands
        #endregion Commands

        #region Methods
        public static TaskViewCell Create()
        {
            return Nib.Instantiate(null, null)[0] as TaskViewCell;
        }

        partial void ShareToTelegram_TouchUpInside(UIKit.UIButton sender)
        {
            OnShareHandlerCell?.Invoke();
        }
        #endregion Methods

        #region Overrides
        #endregion Overrides
    }
}