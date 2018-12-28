using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using System;
using TodoList.Core.Models;

namespace TodoList.Droid.Views
{
    public class RecyclerHolder : MvxRecyclerViewHolder
    {
        public TextView GoalNameHolder { get; set; }
        public CheckBox GoalStatusHolder { get; set; }
        public RecyclerHolder(View itemView, IMvxAndroidBindingContext context, Action<Int32> listener) : base(itemView, context)
        {
            GoalNameHolder = itemView.FindViewById<TextView>(Resource.Id.text_view);
            GoalStatusHolder = itemView.FindViewById<CheckBox>(Resource.Id.check_box);
            itemView.Click += (sender, e) => listener(obj: base.AdapterPosition);
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<RecyclerHolder, Goal>();
                set.Bind(this.GoalNameHolder).To(x => x.GoalName);
                set.Bind(this.GoalStatusHolder).To(y => y.GoalStatus);
                set.Apply();
            });
        }

    }
}