// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace TodoList.iOS.Views.Cells
{
    [Register ("TaskViewCell")]
    partial class TaskViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ShareToTelgramButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TaskNameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView TaskStatusImageView { get; set; }

        [Action ("ShareToTelegram_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ShareToTelegram_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (ShareToTelgramButton != null) {
                ShareToTelgramButton.Dispose ();
                ShareToTelgramButton = null;
            }

            if (TaskNameLabel != null) {
                TaskNameLabel.Dispose ();
                TaskNameLabel = null;
            }

            if (TaskStatusImageView != null) {
                TaskStatusImageView.Dispose ();
                TaskStatusImageView = null;
            }
        }
    }
}