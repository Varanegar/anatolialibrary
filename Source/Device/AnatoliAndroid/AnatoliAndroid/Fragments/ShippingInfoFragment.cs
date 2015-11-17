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
        TextView _factorePriceTextView;
        TextView _deliveryTelTextView;
        TextView _storeTelTextView;
        TextView _countTextView;
        Spinner _delivaryDate;
        Spinner _deliveryTime;
        ImageView _editAddressImageView;
        Button _checkoutButton;
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

            _countTextView = view.FindViewById<TextView>(Resource.Id.itemCountTextView);
            _callImageView = view.FindViewById<ImageView>(Resource.Id.callImageView);
            _storeTelTextView = view.FindViewById<TextView>(Resource.Id.storeTelTextView);
            _factorePriceTextView = view.FindViewById<TextView>(Resource.Id.factorPriceTextView);
            _deliveryAddress = view.FindViewById<TextView>(Resource.Id.addressTextView);
            _deliveryTelTextView = view.FindViewById<TextView>(Resource.Id.telTextView);
            _delivaryDate = view.FindViewById<Spinner>(Resource.Id.dateSpinner);
            _deliveryTime = view.FindViewById<Spinner>(Resource.Id.timeSpinner);
            _editAddressImageView = view.FindViewById<ImageView>(Resource.Id.editAddressImageView);
            _checkoutButton = view.FindViewById<Button>(Resource.Id.checkoutButton);
            _checkoutButton.UpdateWidth();
            _checkoutButton.Click += async (s, e) =>
                {
                    try
                    {
                        await OrderManager.SaveOrder();
                        OrderSavedDialogFragment dialog = new OrderSavedDialogFragment();
                        var transaction = FragmentManager.BeginTransaction();
                        dialog.Show(transaction, "order_saved_dialog");
                        AnatoliApp.GetInstance().SetFragment<OrdersListFragment>(new OrdersListFragment(), "orders_fragment");
                    }
                    catch (Exception ex)
                    {
                        if (ex.GetType() == typeof(StoreManager.NullStoreException))
                        {
                            AnatoliApp.GetInstance().SetFragment<StoresListFragment>(new StoresListFragment(), "stores_fragment");
                        }
                    }

                };
            if (DateTime.Now.ToLocalTime().Hour < 16)
                _dateOptions = new DateOption[] { new DateOption("امروز", ShippingInfoManager.ShippingDateOptions.Today), new DateOption("فردا", ShippingInfoManager.ShippingDateOptions.Tommorow) };
            else
                _dateOptions = new DateOption[] { new DateOption("فردا", ShippingInfoManager.ShippingDateOptions.Tommorow) };
            _delivaryDate.Adapter = new ArrayAdapter(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _dateOptions);
            _delivaryDate.ItemSelected += (s, e) =>
            {
                var selectedDateOption = _dateOptions[_delivaryDate.SelectedItemPosition];
                _timeOptions = ShippingInfoManager.GetAvailableDeliveryTimes(DateTime.Now.ToLocalTime(), selectedDateOption.date);
                _deliveryTime.Adapter = new ArrayAdapter(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _timeOptions);
            };

            _editAddressImageView.Click += (s, e) =>
            {
                var transaction = FragmentManager.BeginTransaction();
                EditShippingInfoFragment editShippingDialog = new EditShippingInfoFragment();
                editShippingDialog.ShippingInfoChanged += (address, name, tel) =>
                {
                    _deliveryAddress.Text = address + " " + name;
                    _deliveryTelTextView.Text = tel;
                    if (String.IsNullOrWhiteSpace(_deliveryAddress.Text) || String.IsNullOrEmpty(_deliveryAddress.Text) || String.IsNullOrEmpty(_deliveryTelTextView.Text))
                    {
                        _checkoutButton.Enabled = false;
                    }
                    else
                    {
                        _checkoutButton.Enabled = true;
                    }
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
                _deliveryTelTextView.Text = shippingInfo.tel;
                _checkoutButton.Enabled = true;
            }
            else
            {
                _checkoutButton.Enabled = false;
            }
            _factorePriceTextView.Text = (await ShoppingCardManager.GetTotalPriceAsync()).ToString() + " تومان";
            _countTextView.Text = (await ShoppingCardManager.GetItemsCountAsync()).ToString() + " عدد";
            string tel = (await StoreManager.GetDefault()).store_tel;
            if (String.IsNullOrEmpty(tel))
            {
                _storeTelTextView.Text = "نا مشخص";
                _callImageView.Visibility = ViewStates.Invisible;
            }
            else
            {
                _storeTelTextView.Text = tel;
                _callImageView.Visibility = ViewStates.Visible;
                _callImageView.Click += (s, e) =>
                    {
                        var uri = Android.Net.Uri.Parse(String.Format("tel:{0}", tel));
                        var intent = new Intent(Intent.ActionDial, uri);
                        StartActivity(intent);
                    };
            }

        }
    }
    public class OrderSavedDialogFragment : DialogFragment
    {
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var builder = new AlertDialog.Builder(Activity)
                .SetMessage("سفارش شما با موفقیت ثبت گردید. برای اطلاع از وضعیت سفارش خود به بخش پیغام ها یا سفارشات قبلی مراجعه نمایید")
                .SetPositiveButton("Ok", (sender, args) =>
                {
                })
                .SetTitle("ثبت سفارش");
            return builder.Create();
        }
    }
}