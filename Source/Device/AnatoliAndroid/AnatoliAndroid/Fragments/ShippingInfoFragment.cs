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
using Anatoli.App.Manager;
using AnatoliAndroid.Activities;

namespace AnatoliAndroid.Fragments
{
    public class ShippingInfoFragment : Fragment
    {
        TextView _deliveryAddress;

        Spinner _delivaryDate;
        Spinner _deliveryTime;
        ImageView _editAddressImageView;
        ImageView _checkoutImageView;
        ImageView _callImageView;
        DateOption[] _dateOptions;
        TimeOption[] _timeOptions;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ShippingInfoLayout, container, false);
            _deliveryAddress = view.FindViewById<TextView>(Resource.Id.addressTextView);

            _delivaryDate = view.FindViewById<Spinner>(Resource.Id.dateSpinner);
            _deliveryTime = view.FindViewById<Spinner>(Resource.Id.timeSpinner);
            _editAddressImageView = view.FindViewById<ImageView>(Resource.Id.editAddressImageView);
            _checkoutImageView = view.FindViewById<ImageView>(Resource.Id.checkoutImageView);

            if (DateTime.Now.ToLocalTime().Hour < 16)
                _dateOptions = new DateOption[] { new DateOption("ÇãÑæÒ", ShippingInfoManager.ShippingDateOptions.Today), new DateOption("ÝÑÏÇ", ShippingInfoManager.ShippingDateOptions.Tommorow) };
            else
                _dateOptions = new DateOption[] { new DateOption("ÝÑÏÇ", ShippingInfoManager.ShippingDateOptions.Tommorow) };
            _delivaryDate.Adapter = new ArrayAdapter(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _dateOptions);
            _delivaryDate.ItemSelected += (s, e) =>
            {
                var selectedDateOption = _dateOptions[_delivaryDate.SelectedItemPosition];
                var timeOptions = ShippingInfoManager.GetAvailableDeliveryTimes(DateTime.Now.ToLocalTime(), selectedDateOption.date);
                _deliveryTime.Adapter = new ArrayAdapter(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, timeOptions);
            };

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

        public async override void OnStart()
        {
            base.OnStart();
            var shippingInfo = await ShippingInfoManager.GetDefaultAsync();
            if (shippingInfo != null)
            {
                _deliveryAddress.Text = shippingInfo.address + " " + shippingInfo.name;
                _checkoutImageView.SetImageResource(Resource.Drawable.Checkout);
            }
        }
    }
}