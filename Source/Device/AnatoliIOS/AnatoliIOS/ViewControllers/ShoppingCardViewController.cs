using System;

using UIKit;
using AnatoliIOS.TableViewSources;
using Anatoli.App.Manager;
using Anatoli.Framework.AnatoliBase;
using Anatoli.App.Model.Product;

namespace AnatoliIOS.ViewControllers
{
	public partial class ShoppingCardViewController : BaseController
	{
		ShoppingCardTableViewSource _productTableViewSource; 
		public ShoppingCardViewController () : base ("ShoppingCardViewController", null)
		{
		}
		public async override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			_productTableViewSource = new ShoppingCardTableViewSource();
			_productTableViewSource.SetDataQuery(ShoppingCardManager.GetAll(AnatoliApp.GetInstance().DefaultStore.store_id));
			await _productTableViewSource.RefreshAsync ();
			if (_productTableViewSource.ItemsCount > 0)
				tableEmptyLabel.Hidden = true;
			else
				tableEmptyLabel.Hidden = false;
			productsTableView.Source = _productTableViewSource;
			productsTableView.ReloadData ();
			if (AnatoliApp.GetInstance().DefaultStore != null) {
				if (AnatoliApp.GetInstance().DefaultStore.store_name != null)
					storeNameLabel.Text = AnatoliApp.GetInstance ().DefaultStore.store_name;
				if (AnatoliApp.GetInstance().DefaultStore.store_tel != null)
					storeTelLabel.Text = AnatoliApp.GetInstance ().DefaultStore.store_tel;
				else
					storeTelLabel.Text = "نامشخص";
			}
			if (AnatoliApp.GetInstance().Customer != null) {
				if (AnatoliApp.GetInstance().Customer.MainStreet != null) {
					addressLabel.Text = AnatoliApp.GetInstance ().Customer.MainStreet;
				}
			}
			itemCountLabel.Text = await ShoppingCardManager.GetItemsCountAsync () + " عدد";
			totalPriceLabel.Text = (await ShoppingCardManager.GetTotalPriceAsync ()).ToCurrency() + " تومان";
			ShoppingCardManager.ItemChanged += UpdateLabels;
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			Title = "سبد خرید";

		}
		public async void UpdateLabels(ProductModel item){
			var count = await ShoppingCardManager.GetItemsCountAsync ();
			itemCountLabel.Text = count + " عدد";
			totalPriceLabel.Text = (await ShoppingCardManager.GetTotalPriceAsync ()).ToCurrency() + " تومان";
			if (count == 0) {
				productsTableView.ReloadData ();
				tableEmptyLabel.Hidden = false;
			}
		}
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			ShoppingCardManager.ItemChanged -= UpdateLabels;
		}
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.

		}
	}
}


