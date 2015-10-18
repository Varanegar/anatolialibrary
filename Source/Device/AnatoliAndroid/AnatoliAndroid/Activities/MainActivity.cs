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

namespace AnatoliAndroid.Activities
{
    [Activity(Label = "آناتولی", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        DrawerLayout _drawerLayout;
        ListView _drawerListView;
        List<Tuple<int, string>> _sections;
        ProductsListFragment _productsListF;
        StoresListFragment _storesListF;
        ProductManager _pm;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            _drawerListView = FindViewById<ListView>(Resource.Id.drawer_list);
            _pm = new ProductManager();
            _sections = _pm.GetCategories(0);
            _drawerListView.Adapter = new ArrayAdapter<Tuple<int, string>>(this, Android.Resource.Layout.SimpleListItem1, _sections);
            _drawerListView.ItemClick += _drawerListView_ItemClick;

            ActivityContainer.Initialize(this);

            //button.Click += async delegate
            //{
            //    var cn = (ConnectivityManager)GetSystemService(ConnectivityService);
            //    AnatoliClient.GetInstance(new AndroidWebClient(cn), new SQLiteAndroid(), new AndroidFileIO());
            //    //AnatoliUserManager um = new AnatoliUserManager();
            //    //var result = await um.RegisterAsync("a.toraby", "pass", "ALi Asghar", "Torabi", "09122073285");
            //    //button.Text = result.metaInfo.Result.ToString();
            //    ProductManager pm = new ProductManager();
            //    StoreManager sm = new StoreManager();
            //    try
            //    {
            //        var list = await pm.GetFrequentlyOrderedProducts(10);
            //    }
            //    catch (Exception)
            //    {
            //        throw;
            //    }
            //};

        }

        void _drawerListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Android.App.Fragment fragment = null;
            //switch (e.Position)
            //{
            //    case 0:
            //        if (_productsListF == null)
            //        {
            //            _productsListF = new ProductsListFragment();
            //        }
            //        fragment = _productsListF;
            //        break;
            //    case 1:
            //        if (_storesListF == null)
            //        {
            //            _storesListF = new StoresListFragment();
            //        }
            //        fragment = _storesListF;
            //        break;
            //    default:
            //        if (_storesListF == null)
            //        {
            //            _storesListF = new StoresListFragment();
            //        }
            //        fragment = _storesListF;
            //        break;
            //}
            _drawerListView.SetItemChecked(e.Position, true);
            ActionBar.Title = _sections[e.Position].Item2;
            var temp = _pm.GetCategories(_sections[e.Position].Item1);
            if (temp != null)
            {
                _productsListF.SetCatId(_sections[e.Position].Item1);
                _sections = temp;
                _drawerListView.Adapter = new ArrayAdapter<Tuple<int, string>>(this, Android.Resource.Layout.SimpleListItem1, _sections);

            }
            //FragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, fragment).Commit();
            _drawerLayout.CloseDrawer(_drawerListView);
        }
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            _productsListF = new ProductsListFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, _productsListF).Commit();
            var cn = (ConnectivityManager)GetSystemService(ConnectivityService);
            AnatoliClient.GetInstance(new AndroidWebClient(cn), new SQLiteAndroid(), new AndroidFileIO());
            //await AnatoliClient.GetInstance().WebClient.GetTokenAsync();
            //await AnatoliClient.GetInstance().WebClient.RefreshTokenAsync(new TokenRefreshParameters("petropay", "petropay", "foo bar"));
            //var a = await AnatoliClient.GetInstance().WebClient.SendGetRequestAsync<User>("/api/accounts/user/anatoli");
            //string n = a.UserName;
            //_drawerToggle.SyncState();
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.ActionBar, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.mainMenu:
                    if (_drawerLayout.IsDrawerOpen(_drawerListView))
                    {
                        _drawerLayout.CloseDrawer(_drawerListView);
                    }
                    else
                    {
                        _drawerLayout.OpenDrawer(_drawerListView);
                    }
                    return true;
                case Resource.Id.mainMenu2:
                    ShowDialog();
                    return true;
                case Resource.Id.shoppinCard:
                    if (ShoppingCard.GetInstance().Items != null)
                    {
                        Intent intent = new Intent(this, typeof(ShoppingCardActivity));
                        StartActivityForResult(intent, 0);
                    }
                    return true;
                default:
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
        public void ShowDialog()
        {
            var transaction = FragmentManager.BeginTransaction();
            var dialogFragment = new MenuDialogFragment();
            dialogFragment.Show(transaction, "dialog_fragment");
        }


        public class User
        {
            public string UserName { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }
    }
}

