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
		UILabel productLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (productLabel != null) {
				productLabel.Dispose ();
				productLabel = null;
			}
		}
	}
}
