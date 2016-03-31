using System;
using Anatoli.App.Manager;
using Anatoli.App.Model.Product;
using AnatoliIOS.TableViewCells;
using UIKit;

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
			if (cell == null) {
				cell = new ProductSummaryViewCell ();
			}
			cell.UpdateCell (Items [indexPath.Row]);
			return cell;
		}
		public override UITableViewRowAction[] EditActionsForRow (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			UITableViewRowAction favoritAction;
			if (Items [indexPath.Row].IsFavorit) {
				favoritAction = UITableViewRowAction.Create (UITableViewRowActionStyle.Default, "حذف از فهرست من", async delegate {
					tableView.Editing = false;
					var result = await ProductManager.RemoveFavoritAsync(Items[indexPath.Row].product_id);
					if (result) {
						Items[indexPath.Row].favorit = 0;
					}
				});
			} else {
				favoritAction = UITableViewRowAction.Create (UITableViewRowActionStyle.Default, "افزودن به فهرست من", async delegate {
					tableView.Editing = false;
					var result = await ProductManager.AddToFavoritsAsync (Items [indexPath.Row].product_id);
					if (result) {
						Items [indexPath.Row].favorit = 1;
					}
				});
			}
			return new UITableViewRowAction[]{ favoritAction };
		}
	}
}

