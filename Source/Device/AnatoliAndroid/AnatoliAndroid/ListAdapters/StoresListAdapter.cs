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
        public override View GetItemView(int position, View convertView, ViewGroup parent)
        {
            convertView = _context.LayoutInflater.Inflate(Resource.Layout.StoreSummaryLayout, null);

            StoreDataModel item = null;
            if (List != null)
                item = List[position];
            else
                return convertView;
            convertView.Click += async (s, e) =>
            {
                if (await StoreManager.SelectAsync(item) == true)
                {
                    AnatoliApp.GetInstance().SetFragment<FirstFragment>(new FirstFragment(), "first_fragment");
                    OnStoreSelected(item);
                }
            };
            if (item.selected == 1)
            {
                convertView.SetBackgroundResource(Resource.Color.lightgray);
            }
            TextView storeNameTextView = convertView.FindViewById<TextView>(Resource.Id.storeNameTextView);
            TextView storeAddressTextView = convertView.FindViewById<TextView>(Resource.Id.storeAddressTextView);
            TextView storeStatusTextView = convertView.FindViewById<TextView>(Resource.Id.storeStatusTextView);
            TextView _storeDistance = convertView.FindViewById<TextView>(Resource.Id.distanceTextView);
            ImageView _mapIconImageView = convertView.FindViewById<ImageView>(Resource.Id.mapIconImageView);

            
            if (item.distance < 0.0005)
                _storeDistance.Text = AnatoliApp.GetResources().GetText(Resource.String.UnknownDistance);
            else if (item.distance < 1500)
                _storeDistance.Text = item.distance.ToString() + " " + AnatoliApp.GetResources().GetText(Resource.String.Meter);
            else
                _storeDistance.Text = Math.Round((item.distance / 1000), 1).ToString() + " " + AnatoliApp.GetResources().GetText(Resource.String.KMeter);

            if (!String.IsNullOrEmpty(item.location))
            {
                _mapIconImageView.Click += (s, e) =>
                {
                    OpenMap(item.location);
                };
                storeAddressTextView.Click += (s, e) =>
                {
                    OpenMap(item.location);
                };
                _storeDistance.Click += (s, e) =>
                {
                    OpenMap(item.location);
                };
            }

            // todo : add store close open 

            storeStatusTextView.Text = AnatoliApp.GetResources().GetText(Resource.String.Open);
            storeStatusTextView.SetTextColor(Android.Graphics.Color.Green);

            //storeStatusTextView.Text = AnatoliApp.GetResources().GetText(Resource.String.Close);
            //storeStatusTextView.SetTextColor(Android.Graphics.Color.Red);

            storeNameTextView.Text = item.store_name;
            storeAddressTextView.Text = item.store_address;
            // productIimageView.SetUrlDrawable(MadanerClient.Configuration.UsersImageBaseUri + "/" + item.User.image, null, 600000);
            return convertView;
        }
        void OpenMap(string location)
        {
            try
            {
                var geoUri = Android.Net.Uri.Parse("geo:" + location);
                var mapIntent = new Intent(Intent.ActionView, geoUri);
                AnatoliApp.GetInstance().Activity.StartActivity(mapIntent);
            }
            catch (Exception)
            {

            }
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