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
    [Register ("FillingDataView")]
    partial class FillingDataView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField NameOfTaskTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (NameOfTaskTextField != null) {
                NameOfTaskTextField.Dispose ();
                NameOfTaskTextField = null;
            }
        }
    }
}