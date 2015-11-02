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
using Anatoli.App.Model.AnatoliUser;
using AnatoliAndroid.Activities;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("„‘Œ’«  „‰")]
    public class ProfileFragment : Fragment
    {
        TextView _firstNameTextView;
        TextView _lastNameTextView;
        TextView _emailTextView;
        TextView _telTextView;
        TextView _addressTextView;
        ShippingInfoModel _shippingInfo;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ProfileLayout, container, false);
            _firstNameTextView = view.FindViewById<TextView>(Resource.Id.firstNameTextView);
            _lastNameTextView = view.FindViewById<TextView>(Resource.Id.lastNameTextView);
            _emailTextView = view.FindViewById<TextView>(Resource.Id.emailTextView);
            _telTextView = view.FindViewById<TextView>(Resource.Id.telTextView);
            _addressTextView = view.FindViewById<TextView>(Resource.Id.addressTextView);
            return view;
        }
        public async override void OnStart()
        {
            base.OnStart();
            _shippingInfo = ShippingInfoManager.GetDefault();
            if (AnatoliApp.GetInstance().AnatoliUser != null)
            {
                _firstNameTextView.Text = AnatoliApp.GetInstance().AnatoliUser.FirstName;
                _lastNameTextView.Text = AnatoliApp.GetInstance().AnatoliUser.LastName;
                _emailTextView.Text = AnatoliApp.GetInstance().AnatoliUser.Email;
                _telTextView.Text = AnatoliApp.GetInstance().AnatoliUser.Tel;
            }
            if (_shippingInfo != null)
            {
                _addressTextView.Text = _shippingInfo.address;
            }
        }
    }
}