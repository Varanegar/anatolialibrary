using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Anatoli.App.Manager;
using Android.Net;
using Parse;
using AnatoliAndroid.Fragments;
using Android.Support.V4.Widget;
using Android.Support.V4.App;
using System.Collections.Generic;
using Anatoli.Framework.AnatoliBase;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using AnatoliAndroid.ListAdapters;

namespace AnatoliAndroid.Activities
{
    [Activity(Label = "آناتولی", Icon = "@drawable/icon")]
    public class MainActivity : ActionBarActivity
    {
        ImageView _shoppingCardImageView;
        ImageView _searchImageView;
        ImageView _searchBarImageView;
        ImageView _searchButtonImageView;
        ImageView _toolbarImageView;
        TextView _toolbarTextView;
        EditText _searchEditText;
        DrawerLayout _drawerLayout;
        ListView _drawerListView;
        List<DrawerItemType> _menuItems;
        List<DrawerItemType> _mainItems;
        ProductsListFragment _productsListF;
        StoresListFragment _storesListF;
        ProductManager _pm;
        Toolbar _toolbar;
        RelativeLayout _searchBarLayout;
        RelativeLayout _toolbarLayout;
        bool _searchBar = false;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            _searchBarLayout = _toolbar.FindViewById<RelativeLayout>(Resource.Id.searchRelativeLayout);
            _toolbarLayout = _toolbar.FindViewById<RelativeLayout>(Resource.Id.toolbarRelativeLayout);
            _searchBarLayout.Visibility = ViewStates.Gone;
            _toolbarLayout.Visibility = ViewStates.Visible;

            _toolbarImageView = _toolbar.FindViewById<ImageView>(Resource.Id.toolbarImageView);
            _toolbarImageView.Click += toolbarImageView_Click;

            _searchImageView = _toolbar.FindViewById<ImageView>(Resource.Id.searchImageView);
            _searchImageView.Click += searchImageView_Click;

            _searchBarImageView = _toolbar.FindViewById<ImageView>(Resource.Id.searchbarImageView);
            _searchBarImageView.Click += _searchBarImageView_Click;

            _searchButtonImageView = _toolbar.FindViewById<ImageView>(Resource.Id.searchButtonImageView);
            _searchButtonImageView.Click += _searchButtonImageView_Click;

            _searchEditText = _toolbar.FindViewById<EditText>(Resource.Id.searchEditText);

            _shoppingCardImageView = _toolbar.FindViewById<ImageView>(Resource.Id.shoppingCardImageView);
            _shoppingCardImageView.Click += shoppingCardImageView_Click;

            _toolbarTextView = _toolbar.FindViewById<TextView>(Resource.Id.toolbarTextView);
            _toolbarTextView.Text = "دسته بندی کالا";
            SetSupportActionBar(_toolbar);
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            _drawerListView = FindViewById<ListView>(Resource.Id.drawer_list);
            _pm = new ProductManager();
            _mainItems = new List<DrawerItemType>();
            var item1 = new DrawerMainItem();
            item1.ItemId = DrawerMainItem.DrawerMainItems.ProductCategries;
            item1.Name = "دسته بندی کالا";
            item1.ImageResId = Resource.Drawable.GroupIcon;
            var item2 = new DrawerMainItem();
            item2.ItemId = DrawerMainItem.DrawerMainItems.ShoppingCard;
            item2.Name = "سبد خرید";
            var item3 = new DrawerMainItem();
            item3.ItemId = DrawerMainItem.DrawerMainItems.Profile;
            item3.Name = "مشخصات من";
            var item4 = new DrawerMainItem();
            item4.ItemId = DrawerMainItem.DrawerMainItems.Help;
            item4.Name = "راهنما";
            var item5 = new DrawerMainItem();
            item5.ItemId = DrawerMainItem.DrawerMainItems.Login;
            item5.Name = "ورود";
            _mainItems.Add(item1);
            _mainItems.Add(item2);
            _mainItems.Add(item3);
            _mainItems.Add(item5);
            _mainItems.Add(item4);
            _menuItems = _mainItems;
            _drawerListView.Adapter = new DrawerMenuItems(_menuItems, this);
            _drawerListView.ItemClick += _drawerListView_ItemClick;
            ActivityContainer.Initialize(this);
        }

        void _searchButtonImageView_Click(object sender, EventArgs e)
        {
            _productsListF.Search(_searchEditText.Text);
        }

        void _searchBarImageView_Click(object sender, EventArgs e)
        {
            _searchBarLayout.Visibility = ViewStates.Gone;
            _toolbarLayout.Visibility = ViewStates.Visible;
        }

        void shoppingCardImageView_Click(object sender, EventArgs e)
        {
            if (ShoppingCard.GetInstance().Items.Count > 0)
            {
                var transaction = FragmentManager.BeginTransaction();
                var shoppingCardFragment = new ShoppingCardFragment();
                transaction.Replace(Resource.Id.content_frame, shoppingCardFragment, "shoppingCard_fragment");
                transaction.Commit();
            }
            _drawerLayout.CloseDrawer(_drawerListView);
        }

        void searchImageView_Click(object sender, EventArgs e)
        {
            if (_searchBar)
            {

            }
            else
            {
                _toolbarLayout.Visibility = ViewStates.Gone;
                _searchBarLayout.Visibility = ViewStates.Visible;
            }
        }

        void toolbarImageView_Click(object sender, EventArgs e)
        {
            if (_drawerLayout.IsDrawerOpen(_drawerListView))
            {
                _drawerLayout.CloseDrawer(_drawerListView);
            }
            else
            {
                _drawerLayout.OpenDrawer(_drawerListView);
            }
        }


        void _drawerListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var selectedItem = _menuItems[e.Position];
            _drawerListView.SetItemChecked(e.Position, true);
            _toolbarTextView.Text = selectedItem.Name;
            if (selectedItem.GetType() == typeof(DrawerMainItem))
            {
                switch (selectedItem.ItemId)
                {
                    case DrawerMainItem.DrawerMainItems.ProductCategries:
                        _productsListF.ExitSearchMode();
                        _productsListF.SetCatId(0);
                        var temp = _pm.GetCategories(0);
                        var categories = new List<DrawerItemType>();
                        categories.Add(new DrawerMainItem(DrawerMainItem.DrawerMainItems.MainMenu, "منوی اصلی"));
                        foreach (var item in temp)
                        {
                            var it = new DrawerPCItem(item.Item1, item.Item2);
                            categories.Add(it);
                        }
                        _menuItems = categories;
                        _drawerListView.Adapter = new DrawerMenuItems(_menuItems, this);
                        _drawerListView.InvalidateViews();
                        var transactionF = FragmentManager.BeginTransaction();
                        transactionF.Replace(Resource.Id.content_frame, _productsListF, "products_fragment");
                        transactionF.Commit();
                        break;
                    case DrawerMainItem.DrawerMainItems.ShoppingCard:
                        if (ShoppingCard.GetInstance().Items.Count > 0)
                        {
                            var transaction = FragmentManager.BeginTransaction();
                            var shoppingCardFragment = new ShoppingCardFragment();
                            transaction.Replace(Resource.Id.content_frame, shoppingCardFragment, "shoppingCard_fragment");
                            transaction.Commit();
                        }
                        _drawerLayout.CloseDrawer(_drawerListView);
                        break;
                    case DrawerMainItem.DrawerMainItems.Login:
                        var transaction2 = FragmentManager.BeginTransaction();
                        var loginFragment = new LoginFragment();
                        transaction2.Replace(Resource.Id.content_frame, loginFragment, "login_fragment");
                        transaction2.Commit();
                        _drawerLayout.CloseDrawer(_drawerListView);
                        break;
                    case DrawerMainItem.DrawerMainItems.MainMenu:
                        _menuItems = _mainItems;
                        _drawerListView.Adapter = new DrawerMenuItems(_menuItems, this);
                        _drawerListView.InvalidateViews();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                var temp = _pm.GetCategories(selectedItem.ItemId);
                if (temp != null)
                {
                    _productsListF.SetCatId(selectedItem.ItemId);
                    var categories = new List<DrawerItemType>();
                    categories.Add(new DrawerMainItem(DrawerMainItem.DrawerMainItems.MainMenu, "منوی اصلی"));
                    foreach (var item in temp)
                    {
                        var it = new DrawerPCItem(item.Item1, item.Item2);
                        categories.Add(it);
                    }
                    _menuItems = categories;
                    _drawerListView.Adapter = new DrawerMenuItems(_menuItems, this);
                    _drawerListView.InvalidateViews();
                }
                else
                {
                    _drawerLayout.CloseDrawer(_drawerListView);
                }

            }
        }
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            _toolbarTextView.Text = "دسته بندی کالا";
            _productsListF = new ProductsListFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, _productsListF).Commit();
            var cn = (ConnectivityManager)GetSystemService(ConnectivityService);
            AnatoliClient.GetInstance(new AndroidWebClient(cn), new SQLiteAndroid(), new AndroidFileIO());

        }
    }

}

