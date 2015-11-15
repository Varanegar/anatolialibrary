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
using Android.Locations;

namespace AnatoliAndroid.Activities
{
    [Activity(Label = "آناتولی", Icon = "@drawable/icon")]
    public class MainActivity : ActionBarActivity
    {
        Toolbar _toolbar;
        LocationManager _locationManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            _broadcastReceiver = new NetworkStatusBroadcastReceiver();
            _broadcastReceiver.ConnectionStatusChanged += OnNetworkStatusChanged;
            Application.Context.RegisterReceiver(_broadcastReceiver, new IntentFilter(ConnectivityManager.ConnectivityAction));

            //Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_toolbar);
        }

        protected async override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);

            var user = await AnatoliUserManager.ReadUserInfoAsync();
            AnatoliApp.Initialize(this, user, FindViewById<ListView>(Resource.Id.drawer_list), _toolbar);
            _locationManager = (LocationManager)GetSystemService(LocationService);
            AnatoliApp.GetInstance().DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            AnatoliApp.GetInstance().LocationManager = _locationManager;
            AnatoliApp.GetInstance().RefreshMenuItems();
            try
            {
                await StoreManager.GetDefault();
                AnatoliApp.GetInstance().SetFragment<ProductsListFragment>(null, "products_fragment");
            }
            catch (Exception)
            {
                AnatoliApp.GetInstance().SetFragment<StoresListFragment>(null, "stores_fragment");
            }


        }

        private void OnNetworkStatusChanged(object sender, EventArgs e)
        {


        }

        bool exit = false;
        public override void OnBackPressed()
        {
            if (AnatoliApp.GetInstance().DrawerLayout.IsDrawerOpen(AnatoliApp.GetInstance().DrawerListView))
            {
                AnatoliApp.GetInstance().DrawerLayout.CloseDrawer(AnatoliApp.GetInstance().DrawerListView);
                return;
            }
            if (!AnatoliApp.GetInstance().BackFragment())
            {
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

