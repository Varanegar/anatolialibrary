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
        ImageView _readioButtonImageView;
        public override View GetItemView(int position, View convertView, ViewGroup parent)
        {
            convertView = _context.LayoutInflater.Inflate(Resource.Layout.StoreSummaryLayout, null);
            StoreDataModel item = null;
            if (List != null)
                item = List[position];
            else
                return convertView;
            TextView storeNameTextView = convertView.FindViewById<TextView>(Resource.Id.storeNameTextView);
            TextView storeAddressTextView = convertView.FindViewById<TextView>(Resource.Id.storeAddressTextView);
            _readioButtonImageView = convertView.FindViewById<ImageView>(Resource.Id.selectedRadioButtonImageView);
            storeNameTextView.Text = item.store_name;
            storeAddressTextView.Text = item.store_address;
            if (item.selected == 1)
            {
                _readioButtonImageView.SetImageResource(Android.Resource.Drawable.RadiobuttonOnBackground);
            }
            _readioButtonImageView.Click += async (s, e) =>
            {
                if (await StoreManager.SelectAsync(item) == true)
                {
                    _readioButtonImageView.SetImageResource(Android.Resource.Drawable.RadiobuttonOnBackground);
                    AnatoliApp.GetInstance().SetFragment<ProductsListFragment>(new ProductsListFragment(), "products_fragment");
                    OnStoreSelected(item);
                }
            };
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