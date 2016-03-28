using System;

using UIKit;
using AnatoliIOS.TableViewSources;
using Anatoli.App.Manager;

namespace AnatoliIOS.ViewControllers
{
	public partial class ProductsViewController : UIViewController
	{
		public ProductsViewController () : base ("ProductsViewController", null)
		{
		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			var tableViewSource = new ProductsTableViewSource();
			if (AnatoliApp.GetInstance().DefaultStore == null) {
				var store = await StoreManager.GetDefaultAsync();
				if (store != null) {
					AnatoliApp.GetInstance().DefaultStore = store;
				}else{
					AnatoliApp.GetInstance().PushViewController(new StoresViewController());
					return;
				}
			}
			tableViewSource.SetDataQuery (ProductManager.GetAll (AnatoliApp.GetInstance().DefaultStore.store_id));
			tableViewSource.Refresh ();
			productsTableView.Source = tableViewSource;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


