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
using Anatoli.App.Manager;
using AnatoliAndroid.ListAdapters;

namespace AnatoliAndroid
{
    [Activity(Label = "ShoppingCardActivity")]
    public class ShoppingCardActivity : Activity
    {
        ListView _itemsListView;
        EditText _deliveryAddress;
        TextView _factorPrice;
        EditText _delivaryDate;
        EditText _deliveryTime;
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ShoppingCardLayout);
            _itemsListView = FindViewById<ListView>(Resource.Id.shoppingCardListView);
            _deliveryAddress = FindViewById<EditText>(Resource.Id.addressEditText);
            _factorPrice = FindViewById<TextView>(Resource.Id.factorPriceTextView);
            _delivaryDate = FindViewById<EditText>(Resource.Id.deliveryDateEditText);
            _deliveryTime = FindViewById<EditText>(Resource.Id.deliveryTimeEditText);
            List<ProductModel> products = new List<ProductModel>();
            ProductManager pm = new ProductManager();

            foreach (var item in ShoppingCard.GetInstance().Items)
            {
                var p = await pm.GetByIdAsync(item.Key.ToString());
                products.Add(p);
            }
            var adapter = new ProductsListAdapter();
            adapter.List = products;
            _itemsListView.Adapter = adapter;
            // Create your application here
        }
    }
}