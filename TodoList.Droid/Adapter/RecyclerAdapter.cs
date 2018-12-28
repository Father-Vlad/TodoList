using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using System;
using TodoList.Core.ViewModels;

namespace TodoList.Droid.Views
{
    public class RecyclerAdapter : MvxRecyclerAdapter
    {
        public RecyclerAdapter(IMvxAndroidBindingContext bindingContext)
            : base(bindingContext)
        {
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, this.BindingContext.LayoutInflaterHolder);
            var view = this.InflateViewForHolder(parent, viewType, itemBindingContext);
            return new RecyclerHolder(view, itemBindingContext)
            {
                Click = ItemClick,
            };
        }
    }
}