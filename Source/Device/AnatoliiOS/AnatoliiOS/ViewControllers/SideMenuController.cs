using AnatoliIOS.Components;
using System;
using UIKit;
using AnatoliIOS.TableViewCells;

namespace AnatoliIOS.ViewControllers
{
    public partial class SideMenuController : BaseController
    {
        public SideMenuController()
            : base(null, null)
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
			menuTableView.RegisterNibForCellReuse (UINib.FromName ("MenuItemTableViewCell", null), MenuItemTableViewCell.Key);
			AnatoliApp.GetInstance ().MenuTableViewReference = menuTableView;
			AnatoliApp.GetInstance ().RefreshMenu ();

        }
    }
}