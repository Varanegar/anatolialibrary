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
        AnatoliUserModel _user;
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
            _user = await AnatoliUserManager.ReadUserInfoAsync();
            _shippingInfo = ShippingInfoManager.GetDefault();
            if (_user != null)
            {
                _firstNameTextView.Text = _user.FirstName;
                _lastNameTextView.Text = _user.LastName;
                _emailTextView.Text = _user.Email;
                _telTextView.Text = _user.Tel;
            }
            if (_shippingInfo != null)
            {
                _addressTextView.Text = _shippingInfo.address;
            }
        }
    }
}