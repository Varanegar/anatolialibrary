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
using Anatoli.App.Model.Store;
using AnatoliAndroid.Activities;
using Anatoli.App.Model;
using Anatoli.App.Manager;
using Anatoli.Framework.AnatoliBase;
using Anatoli.Framework;

namespace AnatoliAndroid.Fragments
{
    public class EditShippingInfoFragment : DialogFragment
    {
        EditText _addressEditText;
        //EditText _nameEditText;
        //EditText _telEditText;
        Button _button;
        string _address;
        string _tel;
        string _name;

        Spinner _level3Spinner;
        Spinner _level4Spinner;
        Spinner _level2Spinner;
        Spinner _level1Spinner;
        List<CityRegionModel> _level1SpinerDataAdapter;
        List<CityRegionModel> _level2SpinerDataAdapter = new List<CityRegionModel>();
        List<CityRegionModel> _level3SpinerDataAdapter = new List<CityRegionModel>();
        List<CityRegionModel> _level4SpinerDataAdapter = new List<CityRegionModel>();

        CustomerViewModel _customerViewModel;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ShippingInfoEditLayout, container, false);

            _level3Spinner = view.FindViewById<Spinner>(Resource.Id.level3Spinner);
            _level4Spinner = view.FindViewById<Spinner>(Resource.Id.level4Spinner);
            _level2Spinner = view.FindViewById<Spinner>(Resource.Id.level2Spinner);
            _level1Spinner = view.FindViewById<Spinner>(Resource.Id.level1Spinner);



            _level1Spinner.ItemSelected += _level1Spinner_ItemSelected;
            _level2Spinner.ItemSelected += _level2Spinner_ItemSelected;
            _level3Spinner.ItemSelected += _level3Spinner_ItemSelected;

            _addressEditText = view.FindViewById<EditText>(Resource.Id.addressEditText);
            //_nameEditText = view.FindViewById<EditText>(Resource.Id.nameEditText);
            //_telEditText = view.FindViewById<EditText>(Resource.Id.telEditText);
            if (_address != null)
                _addressEditText.Text = _address;
            //if (_tel != null)
            //    _telEditText.Text = _tel;
            //if (_name != null)
            //    _nameEditText.Text = _name;

            _button = view.FindViewById<Button>(Resource.Id.saveButton);
            _button.UpdateWidth();
            _button.Click += async (s, e) =>
                {
                    _customerViewModel.MainStreet = _addressEditText.Text;
                    _customerViewModel.RegionLevel1Id = _level1SpinerDataAdapter[_level1Spinner.SelectedItemPosition].group_id;
                    _customerViewModel.RegionLevel2Id = _level2SpinerDataAdapter[_level2Spinner.SelectedItemPosition].group_id;
                    _customerViewModel.RegionLevel3Id = _level3SpinerDataAdapter[_level3Spinner.SelectedItemPosition].group_id;
                    _customerViewModel.RegionLevel4Id = _level4SpinerDataAdapter[_level4Spinner.SelectedItemPosition].group_id;
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
                    ProgressDialog pDialog = new ProgressDialog(AnatoliApp.GetInstance().Activity);
                    try
                    {
                        pDialog.SetTitle(AnatoliApp.GetResources().GetText(Resource.String.Updating));
                        pDialog.SetMessage(AnatoliApp.GetResources().GetText(Resource.String.PleaseWait));
                        pDialog.Show();
                        var result = await CustomerManager.UploadCustomerAsync(_customerViewModel);
                        pDialog.Dismiss();
                        if (result.IsValid)
                        {
                            await CustomerManager.SaveCustomerAsync(_customerViewModel);
                            errDialog.SetTitle("");
                            errDialog.SetMessage("اطلاعات بروزرسانی شد");
                            errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) =>
                            {
                                OnShippingInfoChanged();
                                Dismiss();
                            });
                            errDialog.Show();

                        }
                        else
                        {
                            pDialog.Dismiss();
                            errDialog.SetTitle("خطا");
                            errDialog.SetMessage(result.ModelStateString);
                            errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                            errDialog.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.SendTrace();
                        pDialog.Dismiss();
                        errDialog.SetMessage(Resource.String.ErrorOccured);
                        errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                        errDialog.SetTitle("خطا");
                        errDialog.Show();
                    }
                };
            return view;
        }
        public override async void OnStart()
        {
            base.OnStart();

            _level1SpinerDataAdapter = await CityRegionManager.GetFirstLevelAsync();
            _level1Spinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _level1SpinerDataAdapter);

            if (_customerViewModel == null)
                _customerViewModel = await CustomerManager.ReadCustomerAsync();
            if (_customerViewModel == null)
                _customerViewModel = await AnatoliApp.GetInstance().RefreshCutomerProfile(true);

            if (_customerViewModel.IsValid)
            {
                _addressEditText.Text = _customerViewModel.MainStreet;
                _level1Spinner.ItemSelected -= _level1Spinner_ItemSelected;
                for (int i = 0; i < _level1SpinerDataAdapter.Count; i++)
                {
                    if (_level1SpinerDataAdapter[i].group_id == _customerViewModel.RegionLevel1Id)
                    {
                        _level1Spinner.SetSelection(i);
                        break;
                    }
                }
                CityRegionModel selectedItem = _level1SpinerDataAdapter[_level1Spinner.SelectedItemPosition];
                _level2SpinerDataAdapter = await CityRegionManager.GetGroupsAsync(selectedItem.group_id);
                _level2Spinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _level2SpinerDataAdapter);
                _level2Spinner.ItemSelected -= _level2Spinner_ItemSelected;
                for (int i = 0; i < _level2SpinerDataAdapter.Count; i++)
                {
                    if (_level2SpinerDataAdapter[i].group_id == _customerViewModel.RegionLevel2Id)
                    {
                        _level2Spinner.SetSelection(i);
                    }
                }

                selectedItem = _level2SpinerDataAdapter[_level2Spinner.SelectedItemPosition];
                _level3SpinerDataAdapter = await CityRegionManager.GetGroupsAsync(selectedItem.group_id);
                _level3Spinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _level3SpinerDataAdapter);
                _level3Spinner.ItemSelected -= _level3Spinner_ItemSelected;
                for (int i = 0; i < _level3SpinerDataAdapter.Count; i++)
                {
                    if (_level3SpinerDataAdapter[i].group_id == _customerViewModel.RegionLevel3Id)
                    {
                        _level3Spinner.SetSelection(i);
                    }
                }

                selectedItem = _level3SpinerDataAdapter[_level3Spinner.SelectedItemPosition];
                _level4SpinerDataAdapter = await CityRegionManager.GetGroupsAsync(selectedItem.group_id);
                _level4Spinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _level4SpinerDataAdapter);


                for (int i = 0; i < _level4SpinerDataAdapter.Count; i++)
                {
                    if (_level4SpinerDataAdapter[i].group_id == _customerViewModel.RegionLevel4Id)
                    {
                        _level4Spinner.SetSelection(i);
                    }
                }

                _level1Spinner.ItemSelected += _level1Spinner_ItemSelected;
                _level2Spinner.ItemSelected += _level2Spinner_ItemSelected;
                _level3Spinner.ItemSelected += _level3Spinner_ItemSelected;

            }

        }

        public void SetAddress(string address)
        {
            _address = address;
        }
        public void SetTel(string tel)
        {
            _tel = tel;
        }
        public void SetName(string name)
        {
            _name = name;
        }
        void OnShippingInfoChanged()
        {
            if (ShippingInfoChanged != null)
            {
                //ShippingInfoChanged.Invoke(_addressEditText.Text, _nameEditText.Text, _telEditText.Text);
                ShippingInfoChanged.Invoke(_addressEditText.Text, "", "");
            }
        }
        public event ShippingInfoChangedHandler ShippingInfoChanged;
        public delegate void ShippingInfoChangedHandler(string address, string name, string tel);

        async void _level2Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                CityRegionModel selectedItem = _level2SpinerDataAdapter[e.Position];
                _level3SpinerDataAdapter = await CityRegionManager.GetGroupsAsync(selectedItem.group_id);
                _level3Spinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _level3SpinerDataAdapter);
            }
            catch (Exception ex)
            {
                ex.SendTrace();
            }
        }

        async void _level3Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                CityRegionModel selectedItem = _level3SpinerDataAdapter[e.Position];
                _level4SpinerDataAdapter = await CityRegionManager.GetGroupsAsync(selectedItem.group_id);
                _level4Spinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _level4SpinerDataAdapter);
            }
            catch (Exception)
            {

            }
        }

        async void _level1Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            try
            {
                CityRegionModel selectedItem = _level1SpinerDataAdapter[e.Position];
                _level2SpinerDataAdapter = await CityRegionManager.GetGroupsAsync(selectedItem.group_id);
                _level2Spinner.Adapter = new ArrayAdapter<CityRegionModel>(AnatoliApp.GetInstance().Activity, Android.Resource.Layout.SimpleListItem1, _level2SpinerDataAdapter);
            }
            catch (Exception)
            {

            }
        }

    }
}