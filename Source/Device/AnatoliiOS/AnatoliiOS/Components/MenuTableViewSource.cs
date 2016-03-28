﻿using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;
using AnatoliIOS.ViewControllers;
using AnatoliIOS.TableViewCells;

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
			var cell = tableView.DequeueReusableCell(MenuItemTableViewCell.Key) as MenuItemTableViewCell;
           
			cell.UpdateCell(Items[indexPath.Row].Title,Items[indexPath.Row].Icon);
            return cell;
        }
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
			AnatoliApp.GetInstance ().SelectMenuItem (indexPath.Row);
			AnatoliApp.GetInstance ().RefreshMenu ();
            tableView.DeselectRow(indexPath, true);
            (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.SidebarController.CloseMenu();
        }
    }
}
