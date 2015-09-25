using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Anatoli.Anatoliclient;
using Android.Net;

namespace AnatoliAndroid
{
    class AndroidWebClient : AnatoliWebClient
    {
        ConnectivityManager _cm;
        public AndroidWebClient(ConnectivityManager cm)
        {
            _cm = cm;
        }
        public override bool IsOnline()
        {
            return _cm.ActiveNetworkInfo == null ? false : _cm.ActiveNetworkInfo.IsConnected;
        }
    }
}