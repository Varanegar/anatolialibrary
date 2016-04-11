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
			EdgesForExtendedLayout = UIRectEdge.None;
			var headerView = ProformHeader.Create ();
			headerView.SizeToFit ();
			table.TableHeaderView = headerView;
			table.TableHeaderView.Bounds = new CoreGraphics.CGRect (0, -10, View.Frame.Width, table.TableHeaderView.Bounds.Height);
			headerView.BackgroundColor = UIColor.LightGray;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


