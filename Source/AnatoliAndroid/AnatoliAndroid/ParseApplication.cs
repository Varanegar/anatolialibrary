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
using Parse;
using Anatoli.App;
using Anatoli.Anatoliclient;

namespace AnatoliAndroid
{
    [Application(Name = "anatoliandroid.ParseApplication")]
    public class ParseApplication : Application
    {
        public ParseApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            // Initialize the Parse client with your Application ID and .NET Key found on
            // your Parse dashboard
            ParseClient.Initialize(Configuration.parseAppId, Configuration.parseDotNetKey);
            ParsePush.ParsePushNotificationReceived += ParsePush.DefaultParsePushNotificationReceivedHandler;

        }
    }
}