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
            DrawerItemType item = null;
            if (_list != null)
                item = _list[position];
            else
                return _context.LayoutInflater.Inflate(Resource.Layout.DrawerItemLayout, null);
            if (item.GetType() == typeof(DrawerMainItem))
            {
                if (item.ItemId == DrawerMainItem.DrawerMainItems.Login || item.ItemId == DrawerMainItem.DrawerMainItems.Favorits)
                    convertView = _context.LayoutInflater.Inflate(Resource.Layout.DrawerItemLayout, null);
                else
                    convertView = _context.LayoutInflater.Inflate(Resource.Layout.DrawerSubItemLayout, null);
            }
            else
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.DrawerItemLayout, null);


            TextView itemTextView = convertView.FindViewById<TextView>(Resource.Id.itemTextView);
            if (item.GetType() == typeof(DrawerMainItem) && item.ItemId == DrawerMainItem.DrawerMainItems.Login)
                itemTextView.SetTextColor(Android.Graphics.Color.Green);

            ImageView itemImageView = convertView.FindViewById<ImageView>(Resource.Id.itemIconImageView);
            RelativeLayout relativeLayout = convertView.FindViewById<RelativeLayout>(Resource.Id.relativeLayout1);
            itemTextView.Text = item.Name;
            if (item.ImageResId != -1)
                itemImageView.SetImageResource(item.ImageResId);
            else if (item.GetType() == typeof(DrawerPCItem))
            {
                var pcItem = item as DrawerPCItem;
                if (pcItem.ItemType == DrawerPCItem.ItemTypes.Parent)
                {
                    itemImageView.SetImageResource(Resource.Drawable.SearchBack);
                }
                else
                {
                    itemImageView.Visibility = ViewStates.Invisible;
                    if (pcItem.ItemType == DrawerPCItem.ItemTypes.Leaf)
                    {
                        convertView.SetBackgroundResource(Resource.Color.lllgray);
                    }
                    else
                    {
                        itemImageView.LayoutParameters.Width = 200;
                        itemImageView.RequestLayout();
                    }
                }
            }
            else
            {
                itemImageView.Visibility = ViewStates.Invisible;
                itemImageView.LayoutParameters.Width = 5;
                itemImageView.RequestLayout();
            }

            if (item.GetType() == typeof(DrawerMainItem))
            {
                if (item.ItemId == DrawerMainItem.DrawerMainItems.MainMenu)
                {
                    relativeLayout.SetBackgroundResource(Resource.Color.llgray);
                }
                if (item.ItemId == DrawerMainItem.DrawerMainItems.Avatar)
                {
                    relativeLayout.SetBackgroundResource(Resource.Color.llgray);
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
        public DrawerItemType(int itemId = 0, string name = "Menu Item", int imageResId = -1)
        {
            ItemId = itemId;
            Name = name;
            ImageResId = imageResId;
        }
    }
    class DrawerPCItem : DrawerItemType
    {
        public ItemTypes ItemType;

        public DrawerPCItem(int itemId, string itemName, ItemTypes type = ItemTypes.Normal)
            : base(itemId, itemName, -1)
        {
            ItemType = type;
        }
        public enum ItemTypes
        {
            Parent = 0,
            Normal,
            Leaf
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
            public const int Logout = 8;
            public const int Messages = 9;
            public const int Orders = 10;
            public const int Avatar = 11;
        }
    }
}