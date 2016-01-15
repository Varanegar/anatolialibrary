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

        Spinner _zoneSpinner;
        Spinner _districtSpinner;
        Spinner _citySpinner;
        Spinner _provinceSpinner;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ShippingInfoEditLayout, container, false);

            _zoneSpinner = view.FindViewById<Spinner>(Resource.Id.zoneSpinner);
            _districtSpinner = view.FindViewById<Spinner>(Resource.Id.districtSpinner);
            _citySpinner = view.FindViewById<Spinner>(Resource.Id.citySpinner);
            _provinceSpinner = view.FindViewById<Spinner>(Resource.Id.provinceSpinner);

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
                    //if (await ShippingInfoManager.NewShippingAddress(_addressEditText.Text, _nameEditText.Text, _telEditText.Text))
                    if (await ShippingInfoManager.NewShippingAddress(_addressEditText.Text, "", "","","","",""))
                        OnShippingInfoChanged();
                    Dismiss();
                };
            return view;
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
                ShippingInfoChanged.Invoke(_addressEditText.Text, "","");
            }
        }
        public event ShippingInfoChangedHandler ShippingInfoChanged;
        public delegate void ShippingInfoChangedHandler(string address, string name, string tel);
    }
}