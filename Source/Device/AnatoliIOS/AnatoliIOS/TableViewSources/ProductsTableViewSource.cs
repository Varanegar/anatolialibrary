using System;
using Anatoli.App.Manager;
using Anatoli.App.Model.Product;
using AnatoliIOS.TableViewCells;

namespace AnatoliIOS.TableViewSources
{
	public class ProductsTableViewSource : BaseTableViewSource<ProductManager,ProductModel>
	{
		public ProductsTableViewSource ()
		{
		}
		public override UIKit.UITableViewCell GetCell (UIKit.UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (ProductSummaryViewCell.Key) as ProductSummaryViewCell;
			cell.UpdateCell (Items [indexPath.Row]);
			return cell;
		}
	}
}

