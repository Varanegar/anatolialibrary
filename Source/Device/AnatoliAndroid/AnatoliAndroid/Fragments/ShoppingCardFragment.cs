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
    class ShoppingCardFragment : Fragment
    {
        ListView _itemsListView;
        TextView _deliveryAddress;
        TextView _factorPrice;
        Spinner _delivaryDate;
        Spinner _deliveryTime;
        ImageView _editAddressImageView;
        ProductsListAdapter _listAdapter;
        DateOption[] _dateOptions;
        TimeOption[] _timeOptions;
        ImageView _checkoutImageView;
        ImageView _callImageView;
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
            _delivaryDate = view.FindViewById<Spinner>(Resource.Id.dateSpinner);
            _deliveryTime = view.FindViewById<Spinner>(Resource.Id.timeSpinner);
            _editAddressImageView = view.FindViewById<ImageView>(Resource.Id.editAddressImageView);
            _checkoutImageView = view.FindViewById<ImageView>(Resource.Id.checkoutImageView);

            if (DateTime.Now.ToLocalTime().Hour < 16)
                _dateOptions = new DateOption[] { new DateOption("امروز", ShippingInfoManager.ShippingDateOptions.Today), new DateOption("فردا", ShippingInfoManager.ShippingDateOptions.Tommorow) };
            else
                _dateOptions = new DateOption[] { new DateOption("فردا", ShippingInfoManager.ShippingDateOptions.Tommorow) };
            _delivaryDate.Adapter = new ArrayAdapter(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _dateOptions);
            _delivaryDate.ItemSelected += (s, e) =>
                {
                    var selectedDateOption = _dateOptions[_delivaryDate.SelectedItemPosition];
                    var timeOptions = ShippingInfoManager.GetAvailableDeliveryTimes(DateTime.Now.ToLocalTime(), selectedDateOption.date);
                    _deliveryTime.Adapter = new ArrayAdapter(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, timeOptions);
                };


            _factorPrice.Text = ShoppingCardManager.GetTotalPrice().ToString() + " تومان";
            _listAdapter = new ProductsListAdapter();
            _listAdapter.List = ShoppingCardManager.GetAllItems();
            _listAdapter.NotifyDataSetChanged();
            _listAdapter.DataChanged += (s) =>
            {
                _factorPrice.Text = ShoppingCardManager.GetTotalPrice().ToString() + " تومان";
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
                _checkoutImageView.SetImageResource(Resource.Drawable.Checkout);
            }
            _editAddressImageView.Click += (s, e) =>
                {
                    var transaction = FragmentManager.BeginTransaction();
                    EditShippingInfoFragment editShippingDialog = new EditShippingInfoFragment();
                    editShippingDialog.ShippingInfoChanged += (address, name, tel) =>
                        {
                            _deliveryAddress.Text = address + " " + name;
                            if (String.IsNullOrWhiteSpace(_deliveryAddress.Text) || String.IsNullOrEmpty(_deliveryAddress.Text))
                                _checkoutImageView.SetImageResource(Resource.Drawable.CheckoutGray);
                            else
                                _checkoutImageView.SetImageResource(Resource.Drawable.Checkout);
                        };
                    editShippingDialog.Show(transaction, "shipping_dialog");
                };
            return view;
        }




    }
}