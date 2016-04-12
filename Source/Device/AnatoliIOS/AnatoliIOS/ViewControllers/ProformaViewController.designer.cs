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
	[Register ("ProformaViewController")]
	partial class ProformaViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton cancelButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView footer { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView header { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView table { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (cancelButton != null) {
				cancelButton.Dispose ();
				cancelButton = null;
			}
			if (footer != null) {
				footer.Dispose ();
				footer = null;
			}
			if (header != null) {
				header.Dispose ();
				header = null;
			}
			if (table != null) {
				table.Dispose ();
				table = null;
			}
		}
	}
}
