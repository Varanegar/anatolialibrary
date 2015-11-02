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
using AnatoliAndroid;
using Anatoli.App.Model.AnatoliUser;
using AnatoliAndroid.ListAdapters;

namespace AnatoliAndroid.Activities
{
    class AnatoliApp
    {
        private static AnatoliApp instance;
        private Activity _activity;
        public TextView ToolbarTextView { get; set; }
        public Android.Locations.LocationManager LocationManager;
        public AnatoliUserModel AnatoliUser;
        public List<DrawerItemType> AnatoliMenuItems;
        ListView _drawerListView;
        public ListView DrawerListView { get { return _drawerListView; } }
        private static LinkedList<StackItem> _list;
        public Type GetCurrentFragmentType()
        {
            try
            {
                return _list.Last<StackItem>().FragmentType;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Activity Activity { get { return _activity; } }
        public static AnatoliApp GetInstance()
        {
            if (instance == null)
                throw new NullReferenceException();
            return instance;
        }
        public static void Initialize(Activity activity, AnatoliUserModel user, ListView drawerListView)
        {
            instance = new AnatoliApp(activity, user, drawerListView);
            _list = new LinkedList<StackItem>();
        }
        private AnatoliApp()
        {

        }

        private AnatoliApp(Activity activity, AnatoliUserModel user, ListView drawerListView)
        {
            _activity = activity;
            AnatoliUser = user;
            _drawerListView = drawerListView;
        }
        public FragmentType SetFragment<FragmentType>(FragmentType fragment, string tag) where FragmentType : Android.App.Fragment, new()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            if (fragment == null)
            {
                fragment = new FragmentType();
            }
            transaction.Replace(Resource.Id.content_frame, fragment, tag);
            transaction.Commit();
            ToolbarTextView.Text = fragment.GetTitle();
            foreach (var item in _list)
            {
                if (item.FragmentType == fragment.GetType())
                {
                    if (item != _list.First())
                    {
                        _list.Remove(item);
                        break;
                    }
                }
            }
            _list.AddLast(new StackItem(tag, fragment.GetType()));
            return fragment;
        }
        public bool BackFragment()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            try
            {
                _list.RemoveLast();
                var stackItem = _list.Last<StackItem>();
                var fragment = Activator.CreateInstance(stackItem.FragmentType);
                transaction.Replace(Resource.Id.content_frame, fragment as Fragment, stackItem.FragmentName);
                transaction.Commit();
                ToolbarTextView.Text = (fragment as Fragment).GetTitle();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void RefreshMenuItems(List<DrawerItemType> items = null)
        {
            if (items != null)
            {
                AnatoliMenuItems = items;
                _drawerListView.Adapter = new DrawerMenuItems(AnatoliMenuItems, _activity);
                _drawerListView.InvalidateViews();
                return;
            }
            var mainItems = new List<DrawerItemType>();
            var categoriesMenuEntry = new DrawerMainItem();
            categoriesMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.ProductCategries;
            categoriesMenuEntry.Name = "دسته بندی کالا";
            categoriesMenuEntry.ImageResId = Resource.Drawable.GroupIcon;
            var shoppingCardMenuEntry = new DrawerMainItem();
            shoppingCardMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.ShoppingCard;
            shoppingCardMenuEntry.Name = "سبد خرید";
            shoppingCardMenuEntry.ImageResId = Resource.Drawable.ShoppingCardRed;
            var storesMenuEntry = new DrawerMainItem();
            storesMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.StoresList;
            storesMenuEntry.Name = "انتخاب فروشگاه";
            storesMenuEntry.ImageResId = Resource.Drawable.Store;
            var favoritsMenuEntry = new DrawerMainItem();
            favoritsMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Favorits;
            favoritsMenuEntry.Name = "علاقه مندی ها";
            favoritsMenuEntry.ImageResId = Resource.Drawable.Favorits;
            mainItems.Add(categoriesMenuEntry);
            mainItems.Add(favoritsMenuEntry);
            mainItems.Add(shoppingCardMenuEntry);
            mainItems.Add(storesMenuEntry);

            if (AnatoliUser != null)
            {
                var profileMenuEntry = new DrawerMainItem();
                profileMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Profile;
                profileMenuEntry.Name = "مشخصات من";
                profileMenuEntry.ImageResId = Resource.Drawable.Profile;
                mainItems.Add(profileMenuEntry);

                var logoutMenuEntry = new DrawerMainItem();
                logoutMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Logout;
                logoutMenuEntry.Name = "خروج";
                logoutMenuEntry.ImageResId = Resource.Drawable.Profile;
                mainItems.Add(logoutMenuEntry);
            }
            else
            {
                var loginMenuEntry = new DrawerMainItem();
                loginMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Login;
                loginMenuEntry.Name = "ورود";
                loginMenuEntry.ImageResId = Resource.Drawable.Profile;
                mainItems.Add(loginMenuEntry);
            }

            var helpMenuEntry = new DrawerMainItem();
            helpMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Help;
            helpMenuEntry.Name = "راهنما";
            helpMenuEntry.ImageResId = Resource.Drawable.Help;
            mainItems.Add(helpMenuEntry);
            AnatoliMenuItems = mainItems;
            _drawerListView.Adapter = new DrawerMenuItems(AnatoliMenuItems, _activity);
            _drawerListView.InvalidateViews();
        }

        public class StackItem
        {
            public string FragmentName;
            public Type FragmentType;
            public StackItem(string FragmentName, Type FragmentType)
            {
                this.FragmentName = FragmentName;
                this.FragmentType = FragmentType;
            }
        }

    }
}