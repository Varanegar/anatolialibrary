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
using Anatoli.App.Model.AnatoliUser;

namespace AnatoliAndroid.Fragments
{
    public class ShoppingCardFragment : Fragment
    {
        ListView _itemsListView;
        TextView _deliveryAddress;
        TextView _factorPrice;
        EditText _delivaryDate;
        EditText _deliveryTime;
        List<ProductModel> _products;
        public override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ShoppingCardLayout, container, false);
            _itemsListView = view.FindViewById<ListView>(Resource.Id.shoppingCardListView);
            _deliveryAddress = view.FindViewById<TextView>(Resource.Id.addressTextView);
            _factorPrice = view.FindViewById<TextView>(Resource.Id.factorPriceTextView);
            _delivaryDate = view.FindViewById<EditText>(Resource.Id.deliveryDateEditText);
            _deliveryTime = view.FindViewById<EditText>(Resource.Id.deliveryTimeEditText);
            double price = 0;
            _products = new List<ProductModel>();
            foreach (var item in ShoppingCard.GetInstance().Items)
            {
                price += (item.Value.productModel.price * item.Value.Count);
                _products.Add(item.Value.productModel);
            }
            _factorPrice.Text = price.ToString();
            var adapter = new ProductsListAdapter();
            adapter.DataChanged += adapter_DataChanged;
            adapter.List = _products;
            _itemsListView.Adapter = adapter;
            if (ShoppingCard.GetInstance().Items.Count() == 0)
            {
                Toast.MakeText(AnatoliAndroid.Activities.AnatoliApp.GetInstance().Activity, "سبد خرید خالی است", ToastLength.Short).Show();
            }
            return view;
            // Create your application here
        }

        void adapter_DataChanged(object sender)
        {
            double price = 0;
            _products = new List<ProductModel>();
            foreach (var item in ShoppingCard.GetInstance().Items)
            {
                price += (item.Value.productModel.price * item.Value.Count);
                _products.Add(item.Value.productModel);
            }
            _factorPrice.Text = price.ToString();
            var adapter = new ProductsListAdapter();
            adapter.List = _products;
            adapter.DataChanged += adapter_DataChanged;
            _itemsListView.Adapter = adapter;
        }
    }
}