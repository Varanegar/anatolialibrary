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

namespace AnatoliAndroid.Fragments
{
    public class ProformaFragment : DialogFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ProformaLayout, container, false);
            view.FindViewById<TextView>(Resource.Id.storeNameTextView).Text = "شعبه شریعتی";
            view.FindViewById<TextView>(Resource.Id.paymentTypeTextView).Text = "نقدی";
            view.FindViewById<TextView>(Resource.Id.orderTimeTextView).Text = "1/1/1 12:00";
            return view;
        }
    }
}