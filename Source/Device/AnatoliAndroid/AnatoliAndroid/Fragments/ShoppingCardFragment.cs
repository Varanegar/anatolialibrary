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

namespace AnatoliAndroid.Fragments
{
    class ShoppingCardFragment : Fragment
    {
        ListView _itemsListView;
        TextView _deliveryAddress;
        TextView _factorPrice;
        EditText _delivaryDate;
        EditText _deliveryTime;
        ImageView _editAddressImageView;
        ProductsListAdapter _listAdapter;
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
            _editAddressImageView = view.FindViewById<ImageView>(Resource.Id.editAddressImageView);

            _factorPrice.Text = ShoppingCardManager.GetTotalPrice().ToString();
            _listAdapter = new ProductsListAdapter();
            _listAdapter.List = ShoppingCardManager.GetAllItems();
            _listAdapter.NotifyDataSetChanged();
            _listAdapter.DataChanged += (s) =>
            {
                _factorPrice.Text = ShoppingCardManager.GetTotalPrice().ToString();
            };
            _listAdapter.DataRemoved += (s, item) =>
            {
                _listAdapter.List.Remove(item);
                _itemsListView.InvalidateViews();
            };
            _itemsListView.Adapter = _listAdapter;
            if (_listAdapter.Count == 0)
            {
                Toast.MakeText(AnatoliAndroid.Activities.AnatoliApp.GetInstance().Activity, "سبد خرید خالی است", ToastLength.Short).Show();
            }

            var shippingInfo = ShippingInfoManager.GetDefault();
            if (shippingInfo != null)
            {
                _deliveryAddress.Text = shippingInfo.address + " " + shippingInfo.name;
            }
            _editAddressImageView.Click += (s, e) =>
                {
                    var transaction = FragmentManager.BeginTransaction();
                    EditShippingInfoFragment editShippingDialog = new EditShippingInfoFragment();
                    editShippingDialog.ShippingInfoChanged += (address, name, tel) =>
                        {
                            _deliveryAddress.Text = address + " " + name;
                        };
                    editShippingDialog.Show(transaction, "shipping_dialog");
                };
            return view;
        }

    }
}