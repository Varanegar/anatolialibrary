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
        EditText _firstNameEditText;
        EditText _lastNameEditText;
        EditText _emailEditText;
        EditText _telEditText;
        EditText _addressEditText;
        Spinner _zoneSpinner;
        Spinner _districtSpinner;
        Spinner _citySpinner;
        Spinner _provinceSpinner;
        ShippingInfoModel _shippingInfo;
        ImageButton _exitImageButton;
        Button _saveButton;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ProfileLayout, container, false);
            _firstNameEditText = view.FindViewById<EditText>(Resource.Id.firstNameEditText);
            _lastNameEditText = view.FindViewById<EditText>(Resource.Id.lastNameEditText);
            _emailEditText = view.FindViewById<EditText>(Resource.Id.emailEditText);
            _telEditText = view.FindViewById<EditText>(Resource.Id.telEditText);
            _addressEditText = view.FindViewById<EditText>(Resource.Id.addressEditText);
            _zoneSpinner = view.FindViewById<Spinner>(Resource.Id.zoneSpinner);
            _districtSpinner = view.FindViewById<Spinner>(Resource.Id.districtSpinner);
            _citySpinner = view.FindViewById<Spinner>(Resource.Id.citySpinner);
            _provinceSpinner = view.FindViewById<Spinner>(Resource.Id.provinceSpinner);
            _saveButton = view.FindViewById<Button>(Resource.Id.saveButton);
            _saveButton.UpdateWidth();
            _exitImageButton = view.FindViewById<ImageButton>(Resource.Id.exitImageButton);
            _exitImageButton.Click += async (s, e) =>
            {
                bool result = await AnatoliUserManager.LogoutAsync();
                if (result)
                {
                    AnatoliApp.GetInstance().AnatoliUser = null;
                    AnatoliApp.GetInstance().RefreshMenuItems();
                    AnatoliApp.GetInstance().SetFragment<ProductsListFragment>(null, "products_fragment");
                }
            };
            return view;
        }
        public override void OnStart()
        {
            base.OnStart();
            _shippingInfo = ShippingInfoManager.GetDefault();
            if (AnatoliApp.GetInstance().AnatoliUser != null)
            {
                _firstNameEditText.Text = AnatoliApp.GetInstance().AnatoliUser.FirstName;
                _lastNameEditText.Text = AnatoliApp.GetInstance().AnatoliUser.LastName;
                _emailEditText.Text = AnatoliApp.GetInstance().AnatoliUser.Email;
                _telEditText.Text = AnatoliApp.GetInstance().AnatoliUser.Tel;
            }
            if (_shippingInfo != null)
            {
                _addressEditText.Text = _shippingInfo.address;
            }
        }
    }
}