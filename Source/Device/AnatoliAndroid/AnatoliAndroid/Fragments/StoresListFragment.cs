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
using System.Threading.Tasks;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("انتخاب فروشگاه")]
    class StoresListFragment : BaseListFragment<StoreManager, StoresListAdapter, NoListToolsDialog, StoreDataModel>
    {
        Location _currentLocation;
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

        private async void UpdateDistances()
        {
            if (_currentLocation == null)
                return;
            for (int i = _listView.FirstVisiblePosition; i < _listView.LastVisiblePosition; i++)
            {
                var item = _listView.Adapter.GetItem(i);
                var store = item.Cast<StoreDataModel>();
                if (store != null && !String.IsNullOrEmpty(store.location))
                {
                    Location loc = new Location("destination");
                    string[] l = store.location.Split(new char[] { ',' });
                    double latitude = double.Parse(l[0]);
                    double langitude = double.Parse(l[1]);
                    loc.Latitude = latitude;
                    loc.Longitude = langitude;
                    var dist = CalcDistance(_currentLocation, loc);
                    store.distance = dist;
                    await StoreManager.UpdateDistanceAsync(store.store_id.ToString(), dist);
                    _listAdapter.NotifyDataSetChanged();
                }
            }
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
            _listView.Scroll += _listView_Scroll;
            UpdateDistances();
        }

        private void _listView_Scroll(object sender, AbsListView.ScrollEventArgs e)
        {
            UpdateDistances();
        }

        void StoresListFragment_LocationChanged(Location location)
        {
            if (_currentLocation == null)
            {
                _currentLocation = location;
                UpdateDistances();
            }
            else
                _currentLocation = location;
        }

        public float CalcDistance(Location location1, Location location2)
        {
            var dist = location1.DistanceTo(location2);
            return dist;
        }

        public override void OnPause()
        {
            base.OnPause();
            AnatoliApp.GetInstance().StopLocationUpdates();
            AnatoliApp.GetInstance().LocationChanged -= StoresListFragment_LocationChanged;
        }
    }
}