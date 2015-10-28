using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AnatoliAndroid.ListAdapters
{
    public class DrawerMenuItems : BaseAdapter
    {
        List<DrawerItemType> _list;
        Activity _context;
        public DrawerMenuItems(List<DrawerItemType> list, Activity context)
        {
            _list = list;
            _context = context;
        }
        public override int Count
        {
            get { return _list.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            convertView = _context.LayoutInflater.Inflate(Resource.Layout.DrawerItemLayout, null);
            DrawerItemType item = null;
            if (_list != null)
                item = _list[position];
            else
                return convertView;
            TextView itemTextView = convertView.FindViewById<TextView>(Resource.Id.itemTextView);
            ImageView itemImageView = convertView.FindViewById<ImageView>(Resource.Id.itemIconImageView);
            RelativeLayout relativeLayout = convertView.FindViewById<RelativeLayout>(Resource.Id.relativeLayout1);
            itemTextView.Text = item.Name;
            itemImageView.SetImageResource(item.ImageResId);
            if (item.GetType() == typeof(DrawerMainItem))
            {
                if (item.ItemId == DrawerMainItem.DrawerMainItems.MainMenu)
                {
                    relativeLayout.SetBackgroundColor(Android.Graphics.Color.Gray);
                }
            }
            return convertView;
        }
    }
    public abstract class DrawerItemType
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int ImageResId { get; set; }
        public DrawerItemType(int itemId = 0, string name = "Menu Item", int imageResId = Resource.Drawable.MenuItem)
        {
            ItemId = itemId;
            Name = name;
            ImageResId = imageResId;
        }
    }
    class DrawerPCItem : DrawerItemType
    {

        public DrawerPCItem(int p1, string p2)
            : base(p1, p2)
        {
        }

    }
    class DrawerMainItem : DrawerItemType
    {
        public DrawerMainItem() : base() { }
        public DrawerMainItem(int itemId, string name)
            : base(itemId, name)
        {
        }
        public struct DrawerMainItems
        {
            public const int MainMenu = 0;
            public const int ProductCategries = 1;
            public const int ShoppingCard = 2;
            public const int Profile = 3;
            public const int Help = 4;
            public const int Login = 5;
            public const int StoresList = 6;
            public const int Favorits = 7;
        }
    }
}