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
        public ImageButton TelegramShare { get; set; }
        public ImageView GoalStatusHolder { get; set; }
        public Action<int> OnTelegramShareClickHolder { get; set; }

        public RecyclerHolder(View itemView, IMvxAndroidBindingContext context) : base(itemView, context)
        {
            
            GoalNameHolder = itemView.FindViewById<TextView>(Resource.Id.text_view);
            GoalStatusHolder = itemView.FindViewById<ImageView>(Resource.Id.image_view_check_status);
            TelegramShare = itemView.FindViewById<ImageButton>(Resource.Id.image_button_share);
            TelegramShare.Click += (s,e) =>
            {
                OnTelegramShareClickHolder(AdapterPosition);
            };
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<RecyclerHolder, Goal>();
                set.Bind(this.GoalNameHolder).To(x => x.GoalName);
                set.Apply();
            });
        }
    }
}