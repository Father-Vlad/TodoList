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

        public Action OnShareHandlerCell { get; set; }

        protected TaskViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set_1 = this.CreateBindingSet<TaskViewCell, Goal>();
                set_1.Bind(TaskNameLabel).To(vm => vm.GoalName);
                set_1.Bind(TaskStatusImageView).For(v=>v.Image).To(vm => vm.GoalStatus).WithConversion("StatusImage");
                set_1.Apply();
            });
        }
        partial void ShareToTelegram_TouchUpInside(UIKit.UIButton sender)
        {            
            OnShareHandlerCell?.Invoke();
        }
    }
}