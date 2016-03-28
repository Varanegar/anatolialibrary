using System;

using UIKit;

namespace AnatoliIOS.ViewControllers
{
    public partial class ProfileViewController : BaseController
    {
        public ProfileViewController()
            : base("ProfileViewController", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}