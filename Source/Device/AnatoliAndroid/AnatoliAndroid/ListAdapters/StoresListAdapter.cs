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
                    AnatoliApp.GetInstance().SetFragment<ProductsListFragment>(new ProductsListFragment(), "products_fragment");
                    OnStoreSelected(item);
                }
            };
            if (item.selected == 1)
            {
                convertView.SetBackgroundResource(Resource.Color.lgreen);
            }
            TextView storeNameTextView = convertView.FindViewById<TextView>(Resource.Id.storeNameTextView);
            TextView storeAddressTextView = convertView.FindViewById<TextView>(Resource.Id.storeAddressTextView);
            ImageView storeStatusImageView = convertView.FindViewById<ImageView>(Resource.Id.storeStatusImageView);
            // todo : add store close open 
            int r = new Random().Next(0, 10);
            if (r > 5)
                storeStatusImageView.SetImageResource(Resource.Drawable.sopen);
            else
                storeStatusImageView.SetImageResource(Resource.Drawable.sclose);
            storeNameTextView.Text = item.store_name;
            storeAddressTextView.Text = item.store_address;
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