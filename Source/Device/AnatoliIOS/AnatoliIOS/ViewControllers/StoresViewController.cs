using System;

using UIKit;
using Anatoli.App.Manager;
using AnatoliIOS.TableViewSources;

namespace AnatoliIOS.ViewControllers
{
	public partial class StoresViewController : BaseController
	{
		public StoresViewController () : base ("StoresViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			StoresTableViewSource storesTableViewSource = new StoresTableViewSource();
			storesTableViewSource.SetDataQuery (StoreManager.GetAll ());
			storesTableViewSource.Refresh ();
			storesTableView.Source = storesTableViewSource;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


