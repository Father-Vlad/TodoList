// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace TodoList.iOS
{
    [Register ("SplachScreenAnimationView")]
    partial class SplachScreenAnimationView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ItemWitchAnimatedView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView LaunchImageView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ItemWitchAnimatedView != null) {
                ItemWitchAnimatedView.Dispose ();
                ItemWitchAnimatedView = null;
            }

            if (LaunchImageView != null) {
                LaunchImageView.Dispose ();
                LaunchImageView = null;
            }
        }
    }
}