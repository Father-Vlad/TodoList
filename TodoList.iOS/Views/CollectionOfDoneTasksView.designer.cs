// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace TodoList.iOS.Views
{
    [Register ("CollectionOfDoneTasksView")]
    partial class CollectionOfDoneTasksView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView CollectionOfDoneTasksTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint CollectionOfDoneTasksTopConstraint { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CollectionOfDoneTasksTableView != null) {
                CollectionOfDoneTasksTableView.Dispose ();
                CollectionOfDoneTasksTableView = null;
            }

            if (CollectionOfDoneTasksTopConstraint != null) {
                CollectionOfDoneTasksTopConstraint.Dispose ();
                CollectionOfDoneTasksTopConstraint = null;
            }
        }
    }
}