using System;

using UIKit;
using AnatoliIOS.TableViewSources;
using Anatoli.App.Manager;

namespace AnatoliIOS.ViewControllers
{
	public partial class ShoppingCardViewController : BaseController
	{
		ProductsTableViewSource _productTableViewSource = new ProductsTableViewSource();
		public ShoppingCardViewController () : base ("ShoppingCardViewController", null)
		{
		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			Title = "سبد خرید";
			_productTableViewSource.SetDataQuery(ShoppingCardManager.GetAll(AnatoliApp.GetInstance().DefaultStore.store_id));
			await _productTableViewSource.RefreshAsync ();
			productsTableView.Source = _productTableViewSource;
			productsTableView.ReloadData ();
			if (AnatoliApp.GetInstance().DefaultStore != null) {
				if (AnatoliApp.GetInstance().DefaultStore.store_name != null) {
					storeNameLabel.Text = AnatoliApp.GetInstance ().DefaultStore.store_name;
				}
			}
			if (AnatoliApp.GetInstance().Customer != null) {
				if (AnatoliApp.GetInstance().Customer.MainStreet != null) {
					addressLabel.Text = AnatoliApp.GetInstance ().Customer.MainStreet;
				}
			}

		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.

		}
	}
}


