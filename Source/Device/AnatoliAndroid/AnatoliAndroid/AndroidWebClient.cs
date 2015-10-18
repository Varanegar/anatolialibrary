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

        public override AnatoliTokenInfo LoadTokenInfoFromFile()
        {
            var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documents, "tk.info");
            var info = new FileInfo(path);
            if (info.Exists)
            {
                var lines = File.ReadAllLines(path);
                AnatoliTokenInfo tk = new AnatoliTokenInfo();
                tk.AccessToken = lines[0];
                tk.ExpiresIn = long.Parse(lines[1]);
                return tk;
            }
            return null;
        }

        public async override void SaveTokenInfoToFile(AnatoliTokenInfo tokenInfo)
        {
            var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documents, "tk.info");
            using (var stream = File.CreateText(path))
            {
                await stream.WriteLineAsync(tokenInfo.AccessToken);
                await stream.WriteLineAsync(tokenInfo.ExpiresIn.ToString());
            }
        }
    }
}