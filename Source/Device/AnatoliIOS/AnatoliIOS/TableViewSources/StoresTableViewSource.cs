using System;
using Anatoli.App.Manager;
using Anatoli.App.Model.Store;
using AnatoliIOS.TableViewCells;
using AnatoliIOS.ViewControllers;

namespace AnatoliIOS.TableViewSources
{
    public class StoresTableViewSource : BaseTableViewSource<StoreManager, StoreDataModel>
    {
        public StoresTableViewSource()
        {
        }
		public override UIKit.UITableViewCell GetCellView (UIKit.UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(StoreSummaryTableViewCell.Key) as StoreSummaryTableViewCell;
			cell.UpdateCell(Items[indexPath.Row]);
			return cell;
		}
        
        public async override void RowSelected(UIKit.UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var item = Items[indexPath.Row];
            if (item != null)
            {
                var result = await StoreManager.SelectAsync(item);
                if (result)
                {
                    AnatoliApp.GetInstance().DefaultStore = item;
					AnatoliApp.GetInstance().PushViewController(new FirstPageViewController());
                }
            }

        }
		public override nfloat GetHeightForRow (UIKit.UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			return 70f;
		}
    }
}

