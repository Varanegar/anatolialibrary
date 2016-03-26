using AnatoliIOS.ViewControllers;
using Foundation;
using SidebarNavigation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnatoliIOS
{
    public partial class RootViewController : UIViewController
    {
        // the sidebar controller for the app
        public SidebarController SidebarController { get; private set; }
        // the navigation controller
        public NavController NavController { get; private set; }
        public RootViewController(IntPtr handle)
            : base(handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavController = new NavController();
            NavController.PushViewController(new FirstPage(), true);
            SidebarController = new SidebarNavigation.SidebarController(this, NavController, new SideMenuController());
            SidebarController.MenuWidth = 180;
            SidebarController.ReopenOnRotate = false;
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
