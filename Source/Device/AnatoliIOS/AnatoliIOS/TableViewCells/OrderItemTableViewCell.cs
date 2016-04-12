using System;

using Foundation;
using UIKit;

namespace AnatoliIOS.TableViewCells
{
	public partial class OrderItemTableViewCell : BaseTableViewCell
	{
		public static readonly NSString Key = new NSString ("OrderItemTableViewCell");
		public static readonly UINib Nib;

		static OrderItemTableViewCell ()
		{
			Nib = UINib.FromName ("OrderItemTableViewCell", NSBundle.MainBundle);
		}

		public OrderItemTableViewCell (IntPtr handle) : base (handle)
		{
		}
	}
}
