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
using Anatoli.Framework.AnatoliBase;
using Android.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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