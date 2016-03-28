using System;
using Anatoli.App.Manager;
using Anatoli.App.Model.Store;
using AnatoliIOS.TableViewCells;

namespace AnatoliIOS.TableViewSources
{
	public class StoresTableViewSource : BaseTableViewSource<StoreManager,StoreDataModel>
	{
		public StoresTableViewSource ()
		{
		}
		public override UIKit.UITableViewCell GetCell (UIKit.UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (StoreSummaryTableViewCell.Key) as StoreSummaryTableViewCell;
			cell.UpdateCell (Items [indexPath.Row]);
			return cell;
		}
	}
}

