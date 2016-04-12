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

namespace AnatoliIOS.ViewControllers
{
	[Register ("RegisterViewController")]
	partial class RegisterViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField emailTextField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField passwordTextField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField phoneTextField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton registerButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (emailTextField != null) {
				emailTextField.Dispose ();
				emailTextField = null;
			}
			if (passwordTextField != null) {
				passwordTextField.Dispose ();
				passwordTextField = null;
			}
			if (phoneTextField != null) {
				phoneTextField.Dispose ();
				phoneTextField = null;
			}
			if (registerButton != null) {
				registerButton.Dispose ();
				registerButton = null;
			}
		}
	}
}
