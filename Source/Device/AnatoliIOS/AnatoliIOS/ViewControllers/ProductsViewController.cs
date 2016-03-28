using System;

using UIKit;
using AnatoliIOS.TableViewSources;

namespace AnatoliIOS.ViewControllers
{
	public partial class ProductsViewController : UIViewController
	{
		public ProductsViewController () : base ("ProductsViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			productsTableView.Source = new ProductsTableViewSource();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


