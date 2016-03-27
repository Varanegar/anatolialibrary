using AnatoliIOS.Components;
using System;
using UIKit;

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
            var source = new MenuTableViewSource();
            source.Items = new System.Collections.Generic.List<MenuItem>();
			source.Items.Add(new MenuItem() { Title = "Menu 1" , Icon = UIImage.FromBundle("igicon")});
            menuTableView.Source = source;
			menuTableView.RegisterNibForCellReuse (UINib.FromName ("MenuItemTableViewCell", null), MenuItemTableViewCell.Key);
        }
    }
}