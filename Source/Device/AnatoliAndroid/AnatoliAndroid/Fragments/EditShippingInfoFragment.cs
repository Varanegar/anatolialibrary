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
        EditText _nameEditText;
        EditText _telEditText;
        Button _button;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ShippingInfoEditLayout, container, false);
            _addressEditText = view.FindViewById<EditText>(Resource.Id.addressEditText);
            _nameEditText = view.FindViewById<EditText>(Resource.Id.nameEditText);
            _telEditText = view.FindViewById<EditText>(Resource.Id.telEditText);
            _button = view.FindViewById<Button>(Resource.Id.saveButton);
            _button.Click += async (s, e) =>
                {
                    if (await ShippingInfoManager.NewShippingAddress(_addressEditText.Text, _nameEditText.Text, _telEditText.Text))
                        OnShippingInfoChanged();
                    Dismiss();
                };
            return view;
        }

        void OnShippingInfoChanged()
        {
            if (ShippingInfoChanged != null)
            {
                ShippingInfoChanged.Invoke(_addressEditText.Text, _nameEditText.Text, _telEditText.Text);
            }
        }
        public event ShippingInfoChangedHandler ShippingInfoChanged;
        public delegate void ShippingInfoChangedHandler(string address, string name, string tel);
    }
}