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
	[Register ("ShoppingCardViewController")]
	partial class ShoppingCardViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel addressLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton checkoutButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView productsTableView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel storeNameLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (addressLabel != null) {
				addressLabel.Dispose ();
				addressLabel = null;
			}
			if (checkoutButton != null) {
				checkoutButton.Dispose ();
				checkoutButton = null;
			}
			if (productsTableView != null) {
				productsTableView.Dispose ();
				productsTableView = null;
			}
			if (storeNameLabel != null) {
				storeNameLabel.Dispose ();
				storeNameLabel = null;
			}
		}
	}
}
