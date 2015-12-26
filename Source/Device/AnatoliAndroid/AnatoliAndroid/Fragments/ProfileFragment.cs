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
using Anatoli.Framework.AnatoliBase;
using Anatoli.App.Model;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("مشخصات من")]
    public class ProfileFragment : Fragment
    {
        EditText _firstNameEditText;
        EditText _lastNameEditText;
        EditText _emailEditText;
        EditText _telEditText;
        EditText _addressEditText;
        EditText _idEditText;
        Spinner _zoneSpinner;
        Spinner _districtSpinner;
        Spinner _citySpinner;
        Spinner _provinceSpinner;
        ShippingInfoModel _shippingInfo;
        ImageButton _exitImageButton;
        Button _saveButton;
        CustomerViewModel _customerViewModel;
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
            _idEditText = view.FindViewById<EditText>(Resource.Id.idEditText);
            _telEditText = view.FindViewById<EditText>(Resource.Id.telEditText);
            _addressEditText = view.FindViewById<EditText>(Resource.Id.addressEditText);
            _zoneSpinner = view.FindViewById<Spinner>(Resource.Id.zoneSpinner);
            _districtSpinner = view.FindViewById<Spinner>(Resource.Id.districtSpinner);
            _citySpinner = view.FindViewById<Spinner>(Resource.Id.citySpinner);
            _provinceSpinner = view.FindViewById<Spinner>(Resource.Id.provinceSpinner);
            _saveButton = view.FindViewById<Button>(Resource.Id.saveButton);
            _saveButton.UpdateWidth();
            _saveButton.Click += async (s, e) =>
            {
                _customerViewModel.Address = _addressEditText.Text;
                _customerViewModel.Email = _emailEditText.Text;
                _customerViewModel.Mobile = _telEditText.Text;
                _customerViewModel.NationalCode = _idEditText.Text;
                AlertDialog.Builder errDialog = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                try
                {
                    var result = await AnatoliUserManager.UploadUserInfoAsync(_customerViewModel);
                    if (result.IsValid)
                    {
                        errDialog.SetTitle("");
                        errDialog.SetMessage("اطلاعات بروزرسانی شد");
                        errDialog.Show();
                    }
                    else
                    {
                        errDialog.SetTitle("خطا");
                        errDialog.SetMessage(result.ModelStateString);
                        errDialog.Show();
                    }
                }
                catch (Exception ex)
                {
                    errDialog.SetMessage(ex.Message);
                    errDialog.SetTitle("خطا");
                    errDialog.Show();
                }
            };
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
        public async override void OnStart()
        {
            base.OnStart();
            _shippingInfo = ShippingInfoManager.GetDefault();
            if (AnatoliClient.GetInstance().WebClient.IsOnline())
            {
                AlertDialog.Builder errDialog = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);

                try
                {
                    var userModel = await AnatoliUserManager.DownloadUserInfoAsync(AnatoliApp.GetInstance().AnatoliUser);
                    if (userModel.IsValid)
                    {
                        _customerViewModel = userModel;
                        _firstNameEditText.Text = _customerViewModel.CustomerName;
                        _lastNameEditText.Text = _customerViewModel.CustomerName;
                        _idEditText.Text = _customerViewModel.NationalCode;
                        _addressEditText.Text = _customerViewModel.Address;
                        _emailEditText.Text = _customerViewModel.Email;
                        _telEditText.Text = _customerViewModel.Mobile;
                    }
                }
                catch (Exception ex)
                {
                    errDialog.SetMessage(ex.Message);
                    errDialog.Show();
                }
            }
            else if (_shippingInfo != null)
            {
                _addressEditText.Text = _shippingInfo.address;
            }
        }
    }
}