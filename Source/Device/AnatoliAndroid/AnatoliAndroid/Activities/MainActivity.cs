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
using System.Threading.Tasks;

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
        AutoCompleteTextView _searchEditText;
        DrawerLayout _drawerLayout;
        ListView _drawerListView;
        List<DrawerItemType> _menuItems;
        List<DrawerItemType> _mainItems;
        ProductsListFragment _productsListF;
        StoresListFragment _storesListF;
        FavoritsListFragment _favoritsFragment;
        ShoppingCardFragment _shoppingCardFragment;
        ProductManager _pm;
        Toolbar _toolbar;
        RelativeLayout _searchBarLayout;
        RelativeLayout _toolbarLayout;
        ArrayAdapter _autoCompleteAdapter;
        string[] _autoCompleteOptions;
        bool _searchBar = false;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            _broadcastReceiver = new NetworkStatusBroadcastReceiver();
            _broadcastReceiver.ConnectionStatusChanged += OnNetworkStatusChanged;
            Application.Context.RegisterReceiver(_broadcastReceiver, new IntentFilter(ConnectivityManager.ConnectivityAction));

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
            _searchButtonImageView.Click += async (s, e) => { await Search(_searchEditText.Text); };

            _searchEditText = _toolbar.FindViewById<AutoCompleteTextView>(Resource.Id.searchEditText);
            _autoCompleteOptions = new String[] { "نوشیدنی", "لبنیات", "پروتئینی", "خواربار", "روغن", "پنیر", "شیر", "ماست", "کره", "دوغ", "گوشت" };
            _autoCompleteAdapter = new ArrayAdapter(this, Resource.Layout.AutoCompleteDropDownLayout, _autoCompleteOptions);            
            _searchEditText.Adapter = _autoCompleteAdapter;
            _searchEditText.TextChanged += async (s, e) =>
                {
                    if (e.AfterCount >= 3)
                    {
                        if (AnatoliApp.GetInstance().GetCurrentFragmentType() == typeof(ProductsListFragment))
                        {
                            var options = (await ProductManager.GetSuggests(_searchEditText.Text, 20));
                            if (options != null)
                            {
                                _autoCompleteOptions = options.ToArray();
                                _autoCompleteAdapter = new ArrayAdapter(this, Resource.Layout.AutoCompleteDropDownLayout, _autoCompleteOptions);
                                _searchEditText.Adapter = _autoCompleteAdapter;
                                _searchEditText.Invalidate();
                            }

                        }
                    }
                };
            _searchEditText.ItemClick += async (s, e) => { await Search(_searchEditText.Text); };
            _shoppingCardImageView = _toolbar.FindViewById<ImageView>(Resource.Id.shoppingCardImageView);
            _shoppingCardImageView.Click += shoppingCardImageView_Click;

            _toolbarTextView = _toolbar.FindViewById<TextView>(Resource.Id.toolbarTextView);
            _toolbarTextView.Text = "دسته بندی کالا";
            SetSupportActionBar(_toolbar);
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            _drawerListView = FindViewById<ListView>(Resource.Id.drawer_list);
            _pm = new ProductManager();
            _mainItems = new List<DrawerItemType>();
            var categoriesMenuEntry = new DrawerMainItem();
            categoriesMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.ProductCategries;
            categoriesMenuEntry.Name = "دسته بندی کالا";
            categoriesMenuEntry.ImageResId = Resource.Drawable.GroupIcon;
            var shoppingCardMenuEntry = new DrawerMainItem();
            shoppingCardMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.ShoppingCard;
            shoppingCardMenuEntry.Name = "سبد خرید";
            shoppingCardMenuEntry.ImageResId = Resource.Drawable.ShoppingCardRed;
            var profileMenuEntry = new DrawerMainItem();
            profileMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Profile;
            profileMenuEntry.Name = "مشخصات من";
            profileMenuEntry.ImageResId = Resource.Drawable.Profile;
            var helpMenuEntry = new DrawerMainItem();
            helpMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Help;
            helpMenuEntry.Name = "راهنما";
            helpMenuEntry.ImageResId = Resource.Drawable.Help;
            var loginMenuEntry = new DrawerMainItem();
            loginMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Login;
            loginMenuEntry.Name = "ورود";
            loginMenuEntry.ImageResId = Resource.Drawable.Profile;
            var storesMenuEntry = new DrawerMainItem();
            storesMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.StoresList;
            storesMenuEntry.Name = "انتخاب فروشگاه";
            storesMenuEntry.ImageResId = Resource.Drawable.Store;
            var favoritsMenuEntry = new DrawerMainItem();
            favoritsMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Favorits;
            favoritsMenuEntry.Name = "علاقه مندی ها";
            favoritsMenuEntry.ImageResId = Resource.Drawable.Favorits;
            _mainItems.Add(categoriesMenuEntry);
            _mainItems.Add(favoritsMenuEntry);
            _mainItems.Add(shoppingCardMenuEntry);
            _mainItems.Add(storesMenuEntry);
            _mainItems.Add(profileMenuEntry);
            _mainItems.Add(loginMenuEntry);
            _mainItems.Add(helpMenuEntry);
            _menuItems = _mainItems;
            _drawerListView.Adapter = new DrawerMenuItems(_menuItems, this);
            _drawerListView.ItemClick += _drawerListView_ItemClick;
            AnatoliApp.Initialize(this);
        }

        async Task Search(string value)
        {
            if (AnatoliApp.GetInstance().GetCurrentFragmentType() == typeof(ProductsListFragment))
            {
                await _productsListF.Search("product_name", value);
            }
            if (AnatoliApp.GetInstance().GetCurrentFragmentType() == typeof(StoresListFragment))
            {
                await _storesListF.Search("store_name", value);
            }
        }
        void _searchBarImageView_Click(object sender, EventArgs e)
        {
            _searchBarLayout.Visibility = ViewStates.Gone;
            _toolbarLayout.Visibility = ViewStates.Visible;
        }

        void shoppingCardImageView_Click(object sender, EventArgs e)
        {
            AnatoliApp.GetInstance().SetFragment<ShoppingCardFragment>(_shoppingCardFragment, "shoppingCard_fragment");
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
                        AnatoliApp.GetInstance().SetFragment<ProductsListFragment>(_productsListF, "products_fragment");
                        break;
                    case DrawerMainItem.DrawerMainItems.ShoppingCard:
                        AnatoliApp.GetInstance().SetFragment<ShoppingCardFragment>(_shoppingCardFragment, "shoppingCard_fragment");
                        _drawerLayout.CloseDrawer(_drawerListView);
                        break;
                    case DrawerMainItem.DrawerMainItems.StoresList:
                        _storesListF = AnatoliApp.GetInstance().SetFragment<StoresListFragment>(_storesListF, "stores_fragment");
                        _drawerLayout.CloseDrawer(_drawerListView);
                        break;
                    case DrawerMainItem.DrawerMainItems.Login:
                        var loginFragment = new LoginFragment();
                        AnatoliApp.GetInstance().SetFragment<LoginFragment>(loginFragment, "login_fragment");
                        _drawerLayout.CloseDrawer(_drawerListView);
                        break;
                    case DrawerMainItem.DrawerMainItems.MainMenu:
                        _menuItems = _mainItems;
                        _drawerListView.Adapter = new DrawerMenuItems(_menuItems, this);
                        _drawerListView.InvalidateViews();
                        break;
                    case DrawerMainItem.DrawerMainItems.Favorits:
                        _favoritsFragment = new FavoritsListFragment();
                        AnatoliApp.GetInstance().SetFragment<FavoritsListFragment>(_favoritsFragment, "favorits_fragment");
                        _drawerLayout.CloseDrawer(_drawerListView);
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
            //FragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, _productsListF).Commit();
            AnatoliApp.GetInstance().SetFragment<ProductsListFragment>(_productsListF, "products_fragment");
            var cn = (ConnectivityManager)GetSystemService(ConnectivityService);
            AnatoliClient.GetInstance(new AndroidWebClient(cn), new SQLiteAndroid(), new AndroidFileIO());

        }

        private void OnNetworkStatusChanged(object sender, EventArgs e)
        {


        }

        bool exit = false;
        public override void OnBackPressed()
        {
            //            base.OnBackPressed();
            if (!AnatoliApp.GetInstance().BackFragment())
            {
                // todo : prompt for exit;
                if (!exit)
                {
                    exit = true;
                    Toast.MakeText(this, "Please press back again to exit", ToastLength.Short).Show();
                }
                else
                    Finish();
            }
            else
                exit = false;
        }

        public event EventHandler NetworkStatusChanged;

        public NetworkStatusBroadcastReceiver _broadcastReceiver { get; set; }
    }

    [BroadcastReceiver()]
    public class NetworkStatusBroadcastReceiver : BroadcastReceiver
    {

        public event EventHandler ConnectionStatusChanged;

        public override void OnReceive(Context context, Intent intent)
        {
            if (ConnectionStatusChanged != null)
                ConnectionStatusChanged(this, EventArgs.Empty);
        }
    }

}

