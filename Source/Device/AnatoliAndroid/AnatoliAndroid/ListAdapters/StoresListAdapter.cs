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
using Anatoli.App.Model.Product;
using Anatoli.App.Model.Store;
using Anatoli.Framework.Model;
using Anatoli.App.Manager;
using AnatoliAndroid.Fragments;
using AnatoliAndroid.Activities;
using Android.Locations;

namespace AnatoliAndroid.ListAdapters
{
    class StoresListAdapter : BaseListAdapter<StoreManager, StoreDataModel>
    {
        StoreDataModel _item = null;
        string _distance = "äÇ ãÔÎÕ";
        public override View GetItemView(int position, View convertView, ViewGroup parent)
        {
            convertView = _context.LayoutInflater.Inflate(Resource.Layout.StoreSummaryLayout, null);


            if (List != null)
                _item = List[position];
            else
                return convertView;
            convertView.Click += async (s, e) =>
            {
                if (await StoreManager.SelectAsync(_item) == true)
                {
                    AnatoliApp.GetInstance().SetFragment<ProductsListFragment>(new ProductsListFragment(), "products_fragment");
                    OnStoreSelected(_item);
                }
            };
            if (_item.selected == 1)
            {
                convertView.SetBackgroundResource(Resource.Color.lightgray);
            }
            TextView storeNameTextView = convertView.FindViewById<TextView>(Resource.Id.storeNameTextView);
            TextView storeAddressTextView = convertView.FindViewById<TextView>(Resource.Id.storeAddressTextView);
            TextView storeStatusTextView = convertView.FindViewById<TextView>(Resource.Id.storeStatusTextView);
            TextView _storeDistance = convertView.FindViewById<TextView>(Resource.Id.distanceTextView);
            _storeDistance.Text = _distance;
            // todo : add store close open 
            int r = new Random().Next(0, 10);
            if (r > 5)
            {
                storeStatusTextView.Text = AnatoliApp.GetResources().GetText(Resource.String.Open);
                storeStatusTextView.SetTextColor(Android.Graphics.Color.Green);
            }
            else
            {
                storeStatusTextView.Text = AnatoliApp.GetResources().GetText(Resource.String.Close);
                storeStatusTextView.SetTextColor(Android.Graphics.Color.Red);
            }
            storeNameTextView.Text = _item.store_name;
            storeAddressTextView.Text = _item.store_address;
            // productIimageView.SetUrlDrawable(MadanerClient.Configuration.UsersImageBaseUri + "/" + item.User.image, null, 600000);
            return convertView;
        }

        void OnStoreSelected(StoreDataModel store)
        {
            if (StoreSelected != null)
            {
                StoreSelected.Invoke(store);
            }
        }
        public event StoreSelectedHandler StoreSelected;
        public delegate void StoreSelectedHandler(StoreDataModel item);
    }
}