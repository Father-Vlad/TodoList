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
    [Register ("FillingDataView")]
    partial class FillingDataView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DeleteButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView DescriptionOfTaskTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel IsYorNetAvailableLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField NameOfTaskTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SaveButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel StatusOfTaskLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch StatusOfTaskSwitch { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DeleteButton != null) {
                DeleteButton.Dispose ();
                DeleteButton = null;
            }

            if (DescriptionOfTaskTextView != null) {
                DescriptionOfTaskTextView.Dispose ();
                DescriptionOfTaskTextView = null;
            }

            if (IsYorNetAvailableLabel != null) {
                IsYorNetAvailableLabel.Dispose ();
                IsYorNetAvailableLabel = null;
            }

            if (NameOfTaskTextField != null) {
                NameOfTaskTextField.Dispose ();
                NameOfTaskTextField = null;
            }

            if (SaveButton != null) {
                SaveButton.Dispose ();
                SaveButton = null;
            }

            if (StatusOfTaskLabel != null) {
                StatusOfTaskLabel.Dispose ();
                StatusOfTaskLabel = null;
            }

            if (StatusOfTaskSwitch != null) {
                StatusOfTaskSwitch.Dispose ();
                StatusOfTaskSwitch = null;
            }
        }
    }
}