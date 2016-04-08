using System;

using UIKit;

namespace AnatoliIOS.ViewControllers
{
	public partial class ProformaViewController : BaseController
	{
		public ProformaViewController () : base ("ProformaViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			Title = "پیش فاکتور";
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


