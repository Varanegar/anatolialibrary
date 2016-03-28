using System;

using Foundation;
using UIKit;
using CoreGraphics;
using System.Drawing;

namespace AnatoliIOS
{
	public partial class MenuItemTableViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("MenuItemTableViewCell");
		public static readonly UINib Nib;

		static MenuItemTableViewCell ()
		{
			Nib = UINib.FromName ("MenuItemTableViewCell", NSBundle.MainBundle);
		}

		public MenuItemTableViewCell (IntPtr handle) : base (handle)
		{
			
		}

		public void UpdateCell(string title, UIImage uiImage){
			titleLabel.Text = title;
			iconImageView.Image = uiImage;
		}
	}
}
