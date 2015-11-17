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
using Anatoli.App.Model;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.AnatoliBase;
using AnatoliAndroid.Activities;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("سبد خرید")]
    class ShoppingCardFragment : Fragment
    {
        ListView _itemsListView;
        TextView _factorPrice;
        ProductsListAdapter _listAdapter;
        Button _checkoutButton;

        public override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ShoppingCardLayout, container, false);
            _itemsListView = view.FindViewById<ListView>(Resource.Id.shoppingCardListView);
            _factorPrice = view.FindViewById<TextView>(Resource.Id.factorPriceTextView);
            _checkoutButton = view.FindViewById<Button>(Resource.Id.checkoutButton);
            _checkoutButton.UpdateWidth();
            _checkoutButton.Click += (s, e) =>
                {
                    AnatoliApp.GetInstance().SetFragment<ShippingInfoFragment>(new ShippingInfoFragment(), "shipping_fragment");
                };
            return view;
        }
        public async override void OnStart()
        {
            base.OnStart();
            _factorPrice.Text = ShoppingCardManager.GetTotalPrice().ToString() + " تومان";
            _listAdapter = new ProductsListAdapter();
            _listAdapter.List = await ShoppingCardManager.GetAllItemsAsync();
            _listAdapter.NotifyDataSetChanged();
            _listAdapter.DataChanged += (s) =>
            {
                _factorPrice.Text = ShoppingCardManager.GetTotalPrice().ToString() + " تومان";
                if (_listAdapter.Count == 0)
                    _checkoutButton.Enabled = false;
                else
                    _checkoutButton.Enabled = true;
            };
            _listAdapter.ShoppingCardItemRemoved += (s, item) =>
            {
                _listAdapter.List.Remove(item);
                _itemsListView.InvalidateViews();
                if (_listAdapter.Count == 0)
                    _checkoutButton.Enabled = false;
            };
            _itemsListView.Adapter = _listAdapter;
            if (_listAdapter.Count == 0)
            {
                Toast.MakeText(AnatoliAndroid.Activities.AnatoliApp.GetInstance().Activity, "سبد خرید خالی است", ToastLength.Short).Show();
                _checkoutButton.Enabled = false;
            }


        }

    }
}