using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Anatoli.App.Model.Store;
using Anatoli.Framework.Model;
using Anatoli.App.Manager;
using AnatoliAndroid.ListAdapters;
using Anatoli.Framework.AnatoliBase;
using Android.Locations;
using AnatoliAndroid.Activities;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("انتخاب فروشگاه")]
    class StoresListFragment : BaseListFragment<StoreManager, StoresListAdapter, NoListToolsDialog, StoreDataModel>
    {

        public StoresListFragment()
        {
            _listAdapter.StoreSelected += (store) =>
                {
                    foreach (var item in _listAdapter.List)
                    {
                        if (item != store)
                            item.selected = 0;
                        else
                            item.selected = 1;
                    }
                    AnatoliApp.GetInstance().DefaultStore = store.store_name;
                    AnatoliApp.GetInstance().RefreshMenuItems();
                    _listAdapter.NotifyDataSetChanged();
                };
        }


        protected override List<QueryParameter> CreateQueryParameters()
        {
            var parameters = new List<QueryParameter>();
            return parameters;
        }

        protected override string GetTableName()
        {
            return "stores";
        }

        protected override string GetWebServiceUri()
        {
            return Configuration.WebService.Stores.StoresView;
        }
        public override void OnStart()
        {
            base.OnStart();
            AnatoliApp.GetInstance().HideSearchIcon();
            AnatoliApp.GetInstance().StartLocationUpdates();
            AnatoliApp.GetInstance().LocationChanged += StoresListFragment_LocationChanged;
        }

        void StoresListFragment_LocationChanged(Location location)
        {
            Toast.MakeText(AnatoliApp.GetInstance().Activity, location.Longitude.ToString(), ToastLength.Short).Show();
            UpdateDistances(location);
        }
        public void UpdateDistances(Location location)
        {
            
            if (!String.IsNullOrEmpty(_item.location))
            {
                try
                {
                    string[] l = _item.location.Split(new char[] { ',' });
                    double langitude = double.Parse(l[0]);
                    double latitude = double.Parse(l[1]);
                    Location loc = new Location("destination");
                    loc.Latitude = latitude;
                    loc.Longitude = langitude;
                    var dist = location.DistanceTo(loc);
                    _distance = dist.ToString();
                    NotifyDataSetChanged();
                }
                catch (Exception)
                {

                }
            }
        }
        public override void OnPause()
        {
            base.OnPause();
            AnatoliApp.GetInstance().StopLocationUpdates();
            AnatoliApp.GetInstance().LocationChanged -= StoresListFragment_LocationChanged;
        }
    }
}