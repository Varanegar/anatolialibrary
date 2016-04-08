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

namespace AnatoliIOS.TableViewCells
{
	[Register ("ProductSummaryViewCell")]
	partial class ProductSummaryViewCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton addProductButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel countLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel priceLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView productImageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel productLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton removeProductButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView toolsView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (addProductButton != null) {
				addProductButton.Dispose ();
				addProductButton = null;
			}
			if (countLabel != null) {
				countLabel.Dispose ();
				countLabel = null;
			}
			if (priceLabel != null) {
				priceLabel.Dispose ();
				priceLabel = null;
			}
			if (productImageView != null) {
				productImageView.Dispose ();
				productImageView = null;
			}
			if (productLabel != null) {
				productLabel.Dispose ();
				productLabel = null;
			}
			if (removeProductButton != null) {
				removeProductButton.Dispose ();
				removeProductButton = null;
			}
			if (toolsView != null) {
				toolsView.Dispose ();
				toolsView = null;
			}
		}
	}
}
