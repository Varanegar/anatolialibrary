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
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ShippingInfoEditLayout, container, false);
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