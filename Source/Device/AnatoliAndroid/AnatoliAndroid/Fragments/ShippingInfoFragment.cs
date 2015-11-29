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
            var view = inflater.Inflate(Resource.Layout.ShoppingCardLayout, container, false);

            

            return view;
        }

        public async override void OnStart()
        {
            base.OnStart();
            
        }
    }

}