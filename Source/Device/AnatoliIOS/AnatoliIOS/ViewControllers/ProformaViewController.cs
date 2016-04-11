using System;

using UIKit;
using CoreGraphics;

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
			var headerView = ProformHeader.Create ();
			header.AddSubview( headerView);

			var footerView = FooterView.Create ();
			footer.AddSubview(footerView);
			cancelButton.TouchUpInside += (object sender, EventArgs e) => {
				DismissViewController(true,null);
			};

			//table.TableFooterView.Bounds = new CoreGraphics.CGRect (0, -10, View.Frame.Width, table.TableFooterView.Bounds.Height);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


