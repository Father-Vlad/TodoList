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
        public static readonly NSString Key = new NSString("TaskViewCell");
        public static readonly UINib Nib = UINib.FromName("TaskViewCell", NSBundle.MainBundle);

        public static TaskViewCell Create()
        {
            return Nib.Instantiate(null, null)[0] as TaskViewCell;
        }

        protected TaskViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<TaskViewCell, Goal>();
                set.Bind(TaskNameLabel).To(vm => vm.GoalName);
                set.Bind(TaskStatusImageView).For(v=>v.Image).To(vm => vm.GoalStatus).WithConversion("StatusImage");
                set.Apply();
            });
        }
    }
}