// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MailBox
{
    [Register ("DetailsViewController")]
    partial class DetailsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BackButon { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel EmailText { get; set; }

        [Action ("OnDismiss:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnDismiss (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (BackButon != null) {
                BackButon.Dispose ();
                BackButon = null;
            }

            if (EmailText != null) {
                EmailText.Dispose ();
                EmailText = null;
            }
        }
    }
}