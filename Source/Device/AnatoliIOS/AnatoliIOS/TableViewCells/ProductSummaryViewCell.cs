using System;

using Foundation;
using UIKit;
using Anatoli.App.Model.Product;

namespace AnatoliIOS.TableViewCells
{
	public partial class ProductSummaryViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("ProductSummaryViewCell");
		public static readonly UINib Nib;

		static ProductSummaryViewCell ()
		{
			Nib = UINib.FromName ("ProductSummaryViewCell", NSBundle.MainBundle);
		}

		public ProductSummaryViewCell (IntPtr handle) : base (handle)
		{
		}

		public void UpdateCell(ProductModel item){
			productLabel.Text = item.product_name;
		}
	}
}
