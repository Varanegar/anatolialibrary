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
using Anatoli.App.Model.Store;

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
        TextView _exitTextView;
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
                if (!AnatoliClient.GetInstance().WebClient.IsOnline())
                {
                    errDialog.SetTitle(Resources.GetText(Resource.String.NetworkAccessFailed));
                    errDialog.SetMessage(Resources.GetText(Resource.String.PleaseConnectToInternet));
                    errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) =>
                    {
                        Intent intent = new Intent(Android.Provider.Settings.ActionSettings);
                        AnatoliApp.GetInstance().Activity.StartActivity(intent);
                    });
                    errDialog.SetNegativeButton(Resource.String.Cancel, (s2, e2) => { });
                    errDialog.Show();
                    return;
                }
                ProgressDialog pDialog = new ProgressDialog();
                try
                {
                    pDialog.SetTitle(AnatoliApp.GetResources().GetText(Resource.String.Updating));
                    pDialog.SetMessage(AnatoliApp.GetResources().GetText(Resource.String.PleaseWait));
                    pDialog.Show();
                    var result = await CustomerManager.UploadCustomerAsync(_customerViewModel);
                    await CustomerManager.SaveCustomerAsync(_customerViewModel);
                    pDialog.Dismiss();
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
                    pDialog.Dismiss();
                    errDialog.SetMessage(ex.Message);
                    errDialog.SetTitle("خطا");
                    errDialog.Show();
                }
            };
            _exitTextView = view.FindViewById<TextView>(Resource.Id.logoutTextView);
            _exitTextView.Click += (s, e) =>
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                alert.SetMessage(AnatoliApp.GetResources().GetText(Resource.String.AreYouSure));
                alert.SetPositiveButton(AnatoliApp.GetResources().GetText(Resource.String.Yes), async (s2, e2) =>
                {
                    bool result = await AnatoliUserManager.LogoutAsync();
                    if (result)
                    {
                        AnatoliApp.GetInstance().AnatoliUser = null;
                        AnatoliApp.GetInstance().RefreshMenuItems();
                        AnatoliApp.GetInstance().SetFragment<ProductsListFragment>(null, "products_fragment");
                    }

                });
                alert.SetNegativeButton(AnatoliApp.GetResources().GetText(Resource.String.No), (s2, e2) => { });
                alert.Show();
            };
            return view;
        }
        public async override void OnStart()
        {
            base.OnStart();

            // manipulate spin boxes
            var cities = CityRegionManager.GetFirstLevel();
            List<CityRegionModel> zones = new List<CityRegionModel>();
            List<CityRegionModel> districts = new List<CityRegionModel>();
            _citySpinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, cities);
            _citySpinner.ItemSelected += async (s, e) =>
            {
                try
                {
                    CityRegionModel selectedItem = cities[e.Position];
                    zones = await CityRegionManager.GetGroupsAsync(selectedItem.group_id);
                    _zoneSpinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, zones);
                }
                catch (Exception)
                {

                }
            };
            _zoneSpinner.ItemSelected += async (s, e) =>
            {
                try
                {
                    CityRegionModel selectedItem = zones[e.Position];
                    districts = await CityRegionManager.GetGroupsAsync(selectedItem.group_id);
                    _districtSpinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, districts);
                }
                catch (Exception)
                {

                }
            };
            try
            {
                _shippingInfo = ShippingInfoManager.GetDefault();
                if (AnatoliClient.GetInstance().WebClient.IsOnline())
                {
                    AlertDialog.Builder errDialog = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                    ProgressDialog pDialog = new ProgressDialog();
                    pDialog.SetTitle(AnatoliApp.GetResources().GetText(Resource.String.Updating));
                    pDialog.SetMessage(AnatoliApp.GetResources().GetText(Resource.String.PleaseWait));
                    pDialog.Show();
                    try
                    {
                        var userModel = await CustomerManager.DownloadCustomerAsync(AnatoliApp.GetInstance().AnatoliUser);
                        pDialog.Dismiss();
                        if (userModel.IsValid)
                        {
                            _customerViewModel = userModel;
                            await CustomerManager.SaveCustomerAsync(_customerViewModel);
                        }
                    }
                    catch (Exception ex)
                    {
                        errDialog.SetMessage(ex.Message);
                        errDialog.Show();
                    }
                }
                else
                {
                    _customerViewModel = await CustomerManager.ReadCustomerAsync();
                }
                _firstNameEditText.Text = _customerViewModel.CustomerName;
                _lastNameEditText.Text = _customerViewModel.CustomerName;
                _idEditText.Text = _customerViewModel.NationalCode;
                _addressEditText.Text = _customerViewModel.Address;
                _emailEditText.Text = _customerViewModel.Email;
                _telEditText.Text = _customerViewModel.Mobile;
            }
            catch (Exception)
            {

            }

        }
    }
}