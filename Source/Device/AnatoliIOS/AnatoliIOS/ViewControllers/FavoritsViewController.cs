﻿using System;

using UIKit;
using AnatoliIOS.ViewControllers;

namespace AnatoliIOS
{
	public partial class FavoritsViewController : BaseController
	{
		public FavoritsViewController () : base ("FavoritsViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


