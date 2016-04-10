using System;
using AnatoliIOS.TableViewCells;
using Foundation;
using UIKit;

namespace AnatoliIOS.TableViewSources
{
	public class ShoppingCardTableViewSource : ProductsTableViewSource
	{
		public ShoppingCardTableViewSource ()
		{
			ItemRemoved += (tableView, indexPath) => { 
				Items.RemoveAt (indexPath.Row);
				tableView.DeleteRows (new NSIndexPath[]{ indexPath }, UITableViewRowAnimation.Bottom);
				tableView.ReloadData ();
			};
		}

		public override UIKit.UITableViewCell GetCellView (UIKit.UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = base.GetCellView (tableView, indexPath);
			(cell as BaseTableViewCell).ItemRemoved += (object sender, EventArgs e) => {
				Items.RemoveAt (indexPath.Row);
				tableView.DeleteRows (new NSIndexPath[]{ indexPath }, UITableViewRowAnimation.Left);
				tableView.ReloadData ();
			};
			return cell;
		}
	}
}

