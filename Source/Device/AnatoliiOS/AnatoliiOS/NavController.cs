using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace AnatoliIOS
{
    public class NavController : UINavigationController
    {
        public NavController()
            : base((string)null, null)
        {
			
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }
		public override UIViewController PopViewController (bool animated)
		{
			if (AnatoliApp.GetInstance().PopViewController()) {
				return base.PopViewController (animated);	
			}
			return null;
		}
    }
}
