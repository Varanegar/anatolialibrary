using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;
using AnatoliIOS.ViewControllers;

namespace AnatoliIOS.Components
{
    public class MenuTableViewSource : UITableViewSource
    {
        public List<MenuItem> Items;
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return Items.Count;
        }
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(MenuItemTableViewCell.Identifier) as MenuItemTableViewCell;
            if (cell == null)
            {
                cell = new MenuItemTableViewCell();
            }
            cell.UpdateCell(Items[indexPath.Row].Title);
            return cell;
        }
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            AnatoliApp.GetInstance().PushViewController(new ProductsViewControler());
            tableView.DeselectRow(indexPath, true);
            (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.SidebarController.CloseMenu();
        }
    }
}
