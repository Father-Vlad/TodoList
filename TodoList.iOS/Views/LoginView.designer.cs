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
    [Register ("LoginView")]
    partial class LoginView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton LoginToFacebookButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LoginToFacebookButton != null) {
                LoginToFacebookButton.Dispose ();
                LoginToFacebookButton = null;
            }
        }
    }
}