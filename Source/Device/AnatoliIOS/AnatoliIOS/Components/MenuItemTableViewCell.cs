using System;

using Foundation;
using UIKit;

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

		public void UpdateCell(string title){
			titleLabel.Text = title;

		}
	}
}
