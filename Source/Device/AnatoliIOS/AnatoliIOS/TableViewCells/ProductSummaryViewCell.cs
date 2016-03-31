﻿using System;

using Foundation;
using UIKit;
using Anatoli.App.Model.Product;
using Anatoli.Framework.AnatoliBase;
using Anatoli.App.Manager;

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
			priceLabel.Text = item.price.ToCurrency () + " تومان";
			if (!item.IsAvailable) {
				addProductButton.Enabled = false;
				productLabel.TextColor = UIColor.Gray;
				priceLabel.TextColor = UIColor.Gray;
				priceLabel.Text = "موجود نیست";
			} else {
				addProductButton.Enabled = true;
			}
			addProductButton.TouchUpInside += async (object sender, EventArgs e) => {
				addProductButton.Enabled = false;
				if (item.count +1 > item.qty) {
					var alert = UIAlertController.Create("خطا","موجودی کافی نیست",UIAlertControllerStyle.Alert);
					alert.AddAction(UIAlertAction.Create("باشه",UIAlertActionStyle.Default,null));
					AnatoliApp.GetInstance().PresentViewController(alert);
					addProductButton.Enabled = true;
					return;
				}
				var result = await ShoppingCardManager.AddProductAsync(item);
				addProductButton.Enabled = true;
				if (result) {
					item.count ++;
					Console.WriteLine(ShoppingCardManager.GetTotalPriceAsync());
				}
			};
		}
	}
}
