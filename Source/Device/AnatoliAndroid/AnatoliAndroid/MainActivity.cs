using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Anatoli.App.Manager;
using Anatoli.Anatoliclient;
using Android.Net;
using Parse;
using AnatoliAndroid.Fragments;
using Android.Support.V4.Widget;
using Android.Support.V4.App;

namespace AnatoliAndroid
{
    [Activity(Label = "آناتولی", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        DrawerLayout _drawerLayout;
        ListView _drawerListView;
        static readonly string[] _sections = new[] { "Products", "Stores", "Settings" };

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            _drawerListView = FindViewById<ListView>(Resource.Id.drawer_list);
            _drawerListView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _sections);
            _drawerListView.ItemClick += _drawerListView_ItemClick;


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
            Android.App.Fragment fragment = null;
            switch (e.Position)
            {
                case 0:
                    fragment = new ProductsListFragment();
                    break;
                default:
                    fragment = new StoresListFragment();
                    break;
            }
            _drawerListView.SetItemChecked(e.Position, true);
            ActionBar.Title = _sections[e.Position];
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, fragment).Commit();
            _drawerLayout.CloseDrawer(_drawerListView);
        }
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new StoresListFragment()).Commit();
            var cn = (ConnectivityManager)GetSystemService(ConnectivityService);
            AnatoliClient.GetInstance(new AndroidWebClient(cn), new SQLiteAndroid(), new AndroidFileIO());
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
                default:
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}

