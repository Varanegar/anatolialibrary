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

namespace AnatoliAndroid.Activities
{
    [Activity(Label = "آناتولی", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ActionBarActivity
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
            SetContentView(Resource.Layout.Main);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            var toolbarImageView = toolbar.FindViewById<ImageView>(Resource.Id.toolbarImageView);
            toolbarImageView.Click += toolbarImageView_Click;
            var searchImageView = toolbar.FindViewById<ImageView>(Resource.Id.searchImageView);
            searchImageView.Click += searchImageView_Click;
            var shoppingCardImageView = toolbar.FindViewById<ImageView>(Resource.Id.shoppingCardImageView);
            shoppingCardImageView.Click += shoppingCardImageView_Click;
            //Toolbar will now take on default actionbar characteristics
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "آناتولی";
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            _drawerListView = FindViewById<ListView>(Resource.Id.drawer_list);
            _pm = new ProductManager();
            _sections = _pm.GetCategories(0);
            _drawerListView.Adapter = new ArrayAdapter<Tuple<int, string>>(this, Android.Resource.Layout.SimpleListItem1, _sections);
            _drawerListView.ItemClick += _drawerListView_ItemClick;
            ActivityContainer.Initialize(this);
        }

        void shoppingCardImageView_Click(object sender, EventArgs e)
        {
            if (ShoppingCard.GetInstance().Items != null)
            {
                Intent intent = new Intent(this, typeof(ShoppingCardActivity));
                StartActivityForResult(intent, 0);
            }
        }

        void searchImageView_Click(object sender, EventArgs e)
        {
            ShowDialog();
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

        }
      
        public void ShowDialog()
        {
            var transaction = FragmentManager.BeginTransaction();
            var dialogFragment = new MenuDialogFragment();
            dialogFragment.Show(transaction, "dialog_fragment");
        }
    }
}

