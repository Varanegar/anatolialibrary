using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace AnatoliIOS.Components
{
    public class MenuItemTableViewCell : UITableViewCell
    {
        UILabel _title;
        UIImageView _icon;
        public static string Identifier = "MenuTableViewCell";
        public MenuItemTableViewCell()
            : base(UITableViewCellStyle.Default, Identifier)
        {
            _title = new UILabel();
            _icon = new UIImageView();
            ContentView.BackgroundColor = UIColor.White;
            ContentView.AddSubviews(new UIView[] { _title, _icon });
        }
        public void UpdateCell(string title)
        {
            _title.Text = title;
        }
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            _title.Frame = new CGRect(5, 4, ContentView.Bounds.Width, 25);
        }
    }
}
