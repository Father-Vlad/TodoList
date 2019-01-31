using Foundation;
using MvvmCross.Base;
using MvvmCross.Binding.Extensions;
using MvvmCross.Platforms.Ios.Binding.Views;
using System;
using System.Linq;
using TodoList.Core.Models;
using TodoList.iOS.Views.Cells;
using UIKit;

namespace TodoList.iOS.Sources
{
    public class TodoTasksTableViewSource : MvxTableViewSource
    {
        public TodoTasksTableViewSource(UITableView tableView) : base(tableView)
        {
            DeselectAutomatically = true;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var group = ItemsSource.ElementAt(indexPath.Row) as Goal;
            var cell = GetOrCreateCellFor(tableView, indexPath, group);
            return cell;
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = tableView.DequeueReusableCell(TaskViewCell.Key) as TaskViewCell;
            if (cell == null)
            {
                cell = TaskViewCell.Create();
            }
            var bindable = cell as IMvxDataConsumer;
            if (bindable != null)
            {
                bindable.DataContext = item;
            }
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ItemsSource.Count();
        }
    }
}