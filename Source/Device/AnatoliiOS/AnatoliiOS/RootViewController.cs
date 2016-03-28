using AnatoliIOS.ViewControllers;
using Foundation;
using SidebarNavigation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Anatoli.App.Manager;

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
        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            await AnatoliApp.GetInstance().Initialize();

            NavController = new NavController();
            NavController.PushViewController(new FirstPageViewController(), true);
            SidebarController = new SidebarNavigation.SidebarController(this, NavController, new SideMenuController());
            SidebarController.MenuWidth = 180;
            SidebarController.ReopenOnRotate = false;
            await AnatoliApp.GetInstance().SyncDataBase();
            if (AnatoliApp.GetInstance().DefaultStore == null)
            {
                AnatoliApp.GetInstance().PushViewController(new StoresViewController());
            }
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
