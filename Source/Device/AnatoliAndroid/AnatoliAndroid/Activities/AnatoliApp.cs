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
using Toolbar = Android.Support.V7.Widget.Toolbar;
using AnatoliAndroid.Fragments;
using Anatoli.App.Manager;
using Android.Locations;
namespace AnatoliAndroid.Activities
{
    class AnatoliApp
    {
        private static AnatoliApp instance;
        private Activity _activity;
        public Android.Locations.LocationManager LocationManager;
        public AnatoliUserModel AnatoliUser;
        public List<DrawerItemType> AnatoliMenuItems;
        ListView _drawerListView;
        public ListView DrawerListView { get { return _drawerListView; } }
        private static LinkedList<StackItem> _list;

        Toolbar ToolBar;
        public Android.Support.V4.Widget.DrawerLayout DrawerLayout;
        RelativeLayout _searchBarLayout;
        RelativeLayout _toolBarLayout;
        ImageButton _shoppingCardImageButton;
        ImageButton _searchImageButton;
        ImageButton _searchBarImageButton;
        ImageButton _searchButtonImageButton;
        ImageButton _toolBarImageButton;
        ImageButton _menuIconImageButton;
        TextView _shoppingCardTextView;
        TextView _shoppingPriceTextView;
        double _price;
        public string DefaultStore;
        public TextView ShoppingCardItemCount { get { return _shoppingCardTextView; } }
        public void SetTotalPrice(double price)
        {
            _price = price;
            if (!_shoppingCardTextView.Text.Equals("0"))
                _shoppingPriceTextView.Visibility = ViewStates.Visible;
            else
                _shoppingPriceTextView.Visibility = ViewStates.Invisible;
            _shoppingPriceTextView.Text = _price.ToString() + " تومان";
        }
        public double GetTotalPrice()
        {
            return _price;
        }
        public void HideMenuIcon()
        {
            _menuIconImageButton.Visibility = ViewStates.Gone;
        }

        public void ShowMenuIcon()
        {
            _menuIconImageButton.Visibility = ViewStates.Visible;
        }

        public void HideSearchIcon()
        {
            _searchImageButton.Visibility = ViewStates.Gone;
        }

        public void ShowSearchIcon()
        {
            _searchImageButton.Visibility = ViewStates.Visible;
        }
        public delegate void ProcessMenu();
        public ProcessMenu MenuClicked;
        void OnMenuClick()
        {
            if (MenuClicked != null)
            {
                MenuClicked();
            }
        }
        TextView _toolBarTextView;
        AutoCompleteTextView _searchEditText;
        ArrayAdapter _autoCompleteAdapter;
        string[] _autoCompleteOptions;
        bool _searchBar = false;
        public bool SearchBarEnabled { get { return _searchBar; } }

        ProductsListFragment _productsListF;
        StoresListFragment _storesListF;
        FavoritsListFragment _favoritsFragment;
        ShoppingCardFragment _shoppingCardFragment;
        ProfileFragment _profileFragment;
        MessagesListFragment _messagesFragment;
        OrdersListFragment _ordersFragment;

        ProductManager _pm;
        /// <summary>
        /// Shortcut for AnatoliApp.GetInstance().Activity.Resources
        /// </summary>
        /// <returns></returns>
        public static Android.Content.Res.Resources GetResources()
        {
            return AnatoliApp.GetInstance().Activity.Resources;
        }
        public void CloseSearchBar()
        {
            _searchBarLayout.Visibility = ViewStates.Gone;
            _toolBarLayout.Visibility = ViewStates.Visible;
            _searchEditText.Text = "";
            _searchBar = false;
        }
        public void OpenSearchBar()
        {
            _toolBarLayout.Visibility = ViewStates.Gone;
            _searchBarLayout.Visibility = ViewStates.Visible;
            _searchBar = true;
        }
        public void CreateToolbar()
        {
            _searchEditText = ToolBar.FindViewById<AutoCompleteTextView>(Resource.Id.searchEditText);

            _toolBarTextView = ToolBar.FindViewById<TextView>(Resource.Id.toolbarTextView);
            _shoppingCardTextView = ToolBar.FindViewById<TextView>(Resource.Id.shoppingCardTextView);
            _shoppingPriceTextView = ToolBar.FindViewById<TextView>(Resource.Id.shoppingPriceTextView);
            _searchBarLayout = ToolBar.FindViewById<RelativeLayout>(Resource.Id.searchRelativeLayout);
            _toolBarLayout = ToolBar.FindViewById<RelativeLayout>(Resource.Id.toolbarRelativeLayout);
            CloseSearchBar();

            _toolBarImageButton = ToolBar.FindViewById<ImageButton>(Resource.Id.toolbarImageButton);
            _toolBarImageButton.Click += toolbarImageButton_Click;
            _toolBarTextView.Click += toolbarImageButton_Click;

            _searchImageButton = ToolBar.FindViewById<ImageButton>(Resource.Id.searchImageButton);
            _searchImageButton.Click += searchImageButton_Click;

            _searchBarImageButton = ToolBar.FindViewById<ImageButton>(Resource.Id.searchbarImageButton);
            _searchBarImageButton.Click += _searchBarImageButton_Click;

            _searchButtonImageButton = ToolBar.FindViewById<ImageButton>(Resource.Id.searchButtonImageButton);
            _searchButtonImageButton.Click += async (s, e) =>
            {
                DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
                await Search(_searchEditText.Text);
            };


            _autoCompleteOptions = new String[] { "نوشیدنی", "لبنیات", "پروتئینی", "خواربار", "روغن", "پنیر", "شیر", "ماست", "کره", "دوغ", "گوشت" };
            _autoCompleteAdapter = new ArrayAdapter(_activity, Resource.Layout.AutoCompleteDropDownLayout, _autoCompleteOptions);

            _searchEditText.Adapter = _autoCompleteAdapter;
            _searchEditText.TextChanged += async (s, e) =>
            {
                if (e.AfterCount >= 3)
                {
                    if (AnatoliApp.GetInstance().GetCurrentFragmentType() == typeof(AnatoliAndroid.Fragments.ProductsListFragment))
                    {
                        var options = (await Anatoli.App.Manager.ProductManager.GetSuggests(_searchEditText.Text, 20));
                        if (options != null)
                        {
                            _autoCompleteOptions = options.ToArray();
                            _autoCompleteAdapter = new ArrayAdapter(_activity, Resource.Layout.AutoCompleteDropDownLayout, _autoCompleteOptions);
                            _searchEditText.Adapter = _autoCompleteAdapter;
                            _searchEditText.Invalidate();
                        }

                    }
                }
            };
            _searchEditText.ItemClick += async (s, e) => { await Search(_searchEditText.Text); };
            _shoppingCardImageButton = ToolBar.FindViewById<ImageButton>(Resource.Id.shoppingCardImageButton);
            var shoppingbarRelativeLayout = ToolBar.FindViewById<RelativeLayout>(Resource.Id.shoppingbarRelativeLayout);
            shoppingbarRelativeLayout.Click += shoppingbarRelativeLayout_Click;
            _shoppingCardImageButton.Click += shoppingbarRelativeLayout_Click;
            _menuIconImageButton = ToolBar.FindViewById<ImageButton>(Resource.Id.menuImageButton);
            _menuIconImageButton.Click += (s, e) => { OnMenuClick(); };
        }

        void shoppingbarRelativeLayout_Click(object sender, EventArgs e)
        {
            SetFragment<AnatoliAndroid.Fragments.ShoppingCardFragment>(_shoppingCardFragment, "shoppingCard_fragment");
            DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
        }

        async System.Threading.Tasks.Task Search(string value)
        {
            if (String.IsNullOrEmpty(value))
                return;
            if (AnatoliApp.GetInstance().GetCurrentFragmentType() == typeof(AnatoliAndroid.Fragments.ProductsListFragment))
            {
                if (_productsListF == null)
                {
                    _productsListF = _currentFragment as ProductsListFragment;
                }
                await _productsListF.Search("product_name", value);
            }
            if (AnatoliApp.GetInstance().GetCurrentFragmentType() == typeof(AnatoliAndroid.Fragments.StoresListFragment))
            {
                if (_storesListF == null)
                {
                    _storesListF = _currentFragment as StoresListFragment;
                }
                await _storesListF.Search("store_name", value);
            }
        }
        void _searchBarImageButton_Click(object sender, EventArgs e)
        {
            CloseSearchBar();
            DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
        }

        void searchImageButton_Click(object sender, EventArgs e)
        {
            DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
            if (_searchBar)
            {

            }
            else
            {
                OpenSearchBar();
            }
        }

        void toolbarImageButton_Click(object sender, EventArgs e)
        {
            if (DrawerLayout.IsDrawerOpen(AnatoliApp.GetInstance().DrawerListView))
            {
                DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
            }
            else
            {
                DrawerLayout.OpenDrawer(AnatoliApp.GetInstance().DrawerListView);
            }
        }

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
        Fragment _currentFragment;
        public Activity Activity { get { return _activity; } }
        public static AnatoliApp GetInstance()
        {
            if (instance == null)
                throw new NullReferenceException();
            return instance;
        }
        public static void Initialize(Activity activity, AnatoliUserModel user, ListView drawerListView, Toolbar toolbar)
        {
            instance = new AnatoliApp(activity, user, drawerListView, toolbar);
            _list = new LinkedList<StackItem>();
        }
        private AnatoliApp()
        {

        }

        private AnatoliApp(Activity activity, AnatoliUserModel user, ListView drawerListView, Toolbar toolbar)
        {
            _activity = activity;
            AnatoliUser = user;
            _drawerListView = drawerListView;
            _drawerListView.ItemClick += _drawerListView_ItemClick;
            ToolBar = toolbar;
            CreateToolbar();
            _pm = new ProductManager();
        }
        async void _drawerListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var selectedItem = AnatoliApp.GetInstance().AnatoliMenuItems[e.Position];
            AnatoliApp.GetInstance().DrawerListView.SetItemChecked(e.Position, true);
            if (selectedItem.GetType() == typeof(DrawerMainItem))
            {
                switch (selectedItem.ItemId)
                {
                    case DrawerMainItem.DrawerMainItems.ProductCategries:
                        if (_productsListF != null)
                        {
                            _productsListF.ExitSearchMode();
                            _productsListF.SetCatId(0);
                        }
                        var temp = CategoryManager.GetCategories(0);
                        var categories = new List<DrawerItemType>();
                        categories.Add(new DrawerMainItem(DrawerMainItem.DrawerMainItems.MainMenu, AnatoliApp.GetResources().GetText(Resource.String.MainMenu)));
                        var current = CategoryManager.GetParentCategory(0);
                        categories.Add(new DrawerPCItem(current.catId, current.name, DrawerPCItem.ItemTypes.Leaf));
                        foreach (var item in temp)
                        {
                            var it = new DrawerPCItem(item.catId, item.name);
                            categories.Add(it);
                        }
                        AnatoliApp.GetInstance().RefreshMenuItems(categories);
                        _productsListF = AnatoliApp.GetInstance().SetFragment<ProductsListFragment>(_productsListF, "products_fragment");
                        break;
                    case DrawerMainItem.DrawerMainItems.ShoppingCard:
                        _shoppingCardFragment = AnatoliApp.GetInstance().SetFragment<ShoppingCardFragment>(_shoppingCardFragment, "shoppingCard_fragment");
                        DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
                        break;
                    case DrawerMainItem.DrawerMainItems.StoresList:
                        _storesListF = AnatoliApp.GetInstance().SetFragment<StoresListFragment>(_storesListF, "stores_fragment");
                        DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
                        break;
                    case DrawerMainItem.DrawerMainItems.Login:
                        var transaction = Activity.FragmentManager.BeginTransaction();
                        var loginFragment = new LoginFragment();
                        loginFragment.Show(transaction, "shipping_dialog");
                        DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
                        break;
                    case DrawerMainItem.DrawerMainItems.MainMenu:
                        AnatoliApp.GetInstance().RefreshMenuItems();
                        break;
                    case DrawerMainItem.DrawerMainItems.Favorits:
                        _favoritsFragment = new FavoritsListFragment();
                        AnatoliApp.GetInstance().SetFragment<FavoritsListFragment>(_favoritsFragment, "favorits_fragment");
                        DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
                        break;
                    case DrawerMainItem.DrawerMainItems.Avatar:
                        _profileFragment = new ProfileFragment();
                        AnatoliApp.GetInstance().SetFragment<ProfileFragment>(_profileFragment, "profile_fragment");
                        DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
                        break;
                    case DrawerMainItem.DrawerMainItems.Messages:
                        _messagesFragment = new MessagesListFragment();
                        AnatoliApp.GetInstance().SetFragment<MessagesListFragment>(_messagesFragment, "messages_fragment");
                        DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
                        break;
                    case DrawerMainItem.DrawerMainItems.Orders:
                        _ordersFragment = new OrdersListFragment();
                        AnatoliApp.GetInstance().SetFragment<OrdersListFragment>(_ordersFragment, "orders_fragment");
                        DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
                        break;
                    case DrawerMainItem.DrawerMainItems.Logout:
                        bool result = await AnatoliUserManager.LogoutAsync();
                        if (result)
                        {
                            AnatoliApp.GetInstance().AnatoliUser = null;
                            AnatoliApp.GetInstance().RefreshMenuItems();
                            AnatoliApp.GetInstance().SetFragment<ProductsListFragment>(_productsListF, "products_fragment");
                        }
                        DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (_productsListF != null)
                {
                    _productsListF.ExitSearchMode();
                    _productsListF.SetCatId(0);
                }
                if ((selectedItem as DrawerPCItem).ItemType == DrawerPCItem.ItemTypes.Leaf)
                {
                    _productsListF.SetCatId(selectedItem.ItemId);
                    AnatoliApp.GetInstance()._toolBarTextView.Text = selectedItem.Name;
                    DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
                    return;
                }
                var temp = CategoryManager.GetCategories(selectedItem.ItemId);
                if (temp != null)
                {
                    _productsListF.SetCatId(selectedItem.ItemId);
                    var categories = new List<DrawerItemType>();
                    categories.Add(new DrawerMainItem(DrawerMainItem.DrawerMainItems.MainMenu, AnatoliApp.GetResources().GetText(Resource.String.MainMenu)));
                    var parent = CategoryManager.GetParentCategory(selectedItem.ItemId);

                    var current = CategoryManager.GetCategoryInfo(selectedItem.ItemId);
                    if (current.catId != parent.catId)
                    {
                        categories.Add(new DrawerPCItem(parent.catId, parent.name, DrawerPCItem.ItemTypes.Parent));
                        categories.Add(new DrawerPCItem(current.catId, current.name, DrawerPCItem.ItemTypes.Leaf));
                    }
                    else
                        categories.Add(new DrawerPCItem(parent.catId, parent.name, DrawerPCItem.ItemTypes.Leaf));

                    foreach (var item in temp)
                    {
                        var it = new DrawerPCItem(item.catId, item.name);
                        categories.Add(it);
                    }
                    AnatoliApp.GetInstance().RefreshMenuItems(categories);
                    AnatoliApp.GetInstance()._toolBarTextView.Text = selectedItem.Name;
                }
                else
                {
                    AnatoliApp.GetInstance()._toolBarTextView.Text = selectedItem.Name;
                    DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
                }

            }
        }
        internal FragmentType SetFragment<FragmentType>(FragmentType fragment, string tag, Tuple<string, string> parameter) where FragmentType : Android.App.Fragment, new()
        {
            if (fragment == null)
                fragment = new FragmentType();
            Bundle bundle = new Bundle();
            bundle.PutString(parameter.Item1, parameter.Item2);
            fragment.Arguments = bundle;
            return SetFragment(fragment, tag);
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
            _toolBarTextView.Text = fragment.GetTitle();
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
            _currentFragment = fragment;
            return fragment;
        }
        public bool BackFragment()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            try
            {
                _list.RemoveLast();
                var stackItem = _list.Last<StackItem>();
                if (stackItem.FragmentType == typeof(AnatoliAndroid.Fragments.LoginFragment) && AnatoliUser != null)
                {
                    _list.RemoveLast();
                    stackItem = _list.Last<StackItem>();
                }
                if (stackItem.FragmentType == typeof(AnatoliAndroid.Fragments.ProfileFragment) && AnatoliUser == null)
                {
                    _list.RemoveLast();
                    stackItem = _list.Last<StackItem>();
                }
                var fragment = Activator.CreateInstance(stackItem.FragmentType);
                transaction.Replace(Resource.Id.content_frame, fragment as Fragment, stackItem.FragmentName);
                transaction.Commit();
                _toolBarTextView.Text = (fragment as Fragment).GetTitle();
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
            if (AnatoliUser != null)
            {

                var avatarMenuEntry = new DrawerMainItem();
                avatarMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Avatar;
                avatarMenuEntry.Name = AnatoliUser.FullName;
                avatarMenuEntry.ImageResId = Resource.Drawable.ic_person_gray_24dp;
                mainItems.Add(avatarMenuEntry);

            }
            else
            {
                var loginMenuEntry = new DrawerMainItem();
                loginMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Login;
                loginMenuEntry.Name = "ورود";
                loginMenuEntry.ImageResId = Resource.Drawable.ic_log_in_green_24dp;
                mainItems.Add(loginMenuEntry);
            }



            var categoriesMenuEntry = new DrawerMainItem();
            categoriesMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.ProductCategries;
            categoriesMenuEntry.Name = AnatoliApp.GetResources().GetText(Resource.String.Products);
            categoriesMenuEntry.ImageResId = Resource.Drawable.ic_list_orange_24dp;
            mainItems.Add(categoriesMenuEntry);


            var favoritsMenuEntry = new DrawerMainItem();
            favoritsMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Favorits;
            favoritsMenuEntry.Name = AnatoliApp.GetResources().GetText(Resource.String.MyList);
            favoritsMenuEntry.ImageResId = Resource.Drawable.ic_mylist_orange_24dp;
            mainItems.Add(favoritsMenuEntry);

            var storesMenuEntry = new DrawerMainItem();
            storesMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.StoresList;
            if (DefaultStore != null)
                storesMenuEntry.Name = AnatoliApp.GetResources().GetText(Resource.String.MyStore) + " ( " + DefaultStore + " ) ";
            else
                storesMenuEntry.Name = AnatoliApp.GetResources().GetText(Resource.String.MyStore);
            mainItems.Add(storesMenuEntry);

            if (AnatoliUser != null)
            {
                var msgMenuEntry = new DrawerMainItem();
                msgMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Messages;
                msgMenuEntry.Name = AnatoliApp.GetResources().GetText(Resource.String.Messages);
                mainItems.Add(msgMenuEntry);

                var ordersMenuEntry = new DrawerMainItem();
                ordersMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Orders;
                ordersMenuEntry.Name = AnatoliApp.GetResources().GetText(Resource.String.LastOrders);
                mainItems.Add(ordersMenuEntry);
            }

            //var shoppingCardMenuEntry = new DrawerMainItem();
            //shoppingCardMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.ShoppingCard;
            //shoppingCardMenuEntry.Name = "سبد خرید";
            //shoppingCardMenuEntry.ImageResId = Resource.Drawable.ShoppingCardRed;
            //mainItems.Add(shoppingCardMenuEntry);



            //if (AnatoliUser != null)
            //{
            //    var profileMenuEntry = new DrawerMainItem();
            //    profileMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Profile;
            //    profileMenuEntry.Name = "مشخصات من";
            //    profileMenuEntry.ImageResId = Resource.Drawable.Profile;
            //    mainItems.Add(profileMenuEntry);

            //}

            //var helpMenuEntry = new DrawerMainItem();
            //helpMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Help;
            //helpMenuEntry.Name = "راهنما";
            //mainItems.Add(helpMenuEntry);

            //if (AnatoliUser != null)
            //{
            //    var logoutMenuEntry = new DrawerMainItem();
            //    logoutMenuEntry.ItemId = DrawerMainItem.DrawerMainItems.Logout;
            //    logoutMenuEntry.Name = "خروج";
            //    logoutMenuEntry.ImageResId = Resource.Drawable.Exit;
            //    mainItems.Add(logoutMenuEntry);
            //}

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

        string _locationProvider = "";
        
        public void StartLocationUpdates()
        {
            try
            {
                Criteria criteriaForLocationService = new Criteria
                {
                    Accuracy = Accuracy.Fine,
                    PowerRequirement = Power.Medium
                };
                _locationProvider = LocationManager.GetBestProvider(criteriaForLocationService, true);
                if (!String.IsNullOrEmpty(_locationProvider))
                {
                    LocationManager.RequestLocationUpdates(_locationProvider, 0, 0, (ILocationListener)_activity);
                    if (_locationProvider != LocationManager.GpsProvider)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(_activity);
                        alert.SetMessage("برای فاصله یابی دقیق از فروشگاه ها gps دستگاه خود را روشن نمایید");
                        alert.SetPositiveButton("روشن کردن gps", (s, e) =>
                        {
                            Intent callGPSSettingIntent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                            _activity.StartActivity(callGPSSettingIntent);
                        });
                        alert.SetNegativeButton("بی خیال", (s, e) => { });
                        alert.Show();
                    }
                }
                else
                {

                }

            }
            catch (Exception)
            {

            }
        }

        public void StopLocationUpdates()
        {
            LocationManager.RemoveUpdates((ILocationListener)_activity);
        }

        public void SetLocation(Location location)
        {
            OnLocationChanged(location);
        }

        void OnLocationChanged(Location location)
        {
            if (LocationChanged != null)
            {
                LocationChanged.Invoke(location);
            }
        }
        public event LocationChangedEventHandler LocationChanged;
        public delegate void LocationChangedEventHandler(Location location);
    }
}