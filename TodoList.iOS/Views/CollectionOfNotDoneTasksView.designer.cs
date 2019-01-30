// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Blank
{
    [Register ("CollectionOfNotDoneTasksView")]
    partial class CollectionOfNotDoneTasksView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView CollectionOfNotDoneTasksTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CollectionOfNotDoneTasksTableView != null) {
                CollectionOfNotDoneTasksTableView.Dispose ();
                CollectionOfNotDoneTasksTableView = null;
            }
        }
    }
}