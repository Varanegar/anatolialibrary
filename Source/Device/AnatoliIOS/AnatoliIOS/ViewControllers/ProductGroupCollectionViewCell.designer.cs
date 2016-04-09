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

namespace AnatoliIOS
{
	[Register ("ProductGroupCollectionViewCell")]
	partial class ProductGroupCollectionViewCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView groupImageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel groupNameLable { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (groupImageView != null) {
				groupImageView.Dispose ();
				groupImageView = null;
			}
			if (groupNameLable != null) {
				groupNameLable.Dispose ();
				groupNameLable = null;
			}
		}
	}
}
