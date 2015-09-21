using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Anatoli.App.Manager.Product;
using AnatoliLibrary.Anatoliclient;
using Android.Net;

namespace AnatoliAndroid
{
    [Activity(Label = "AnatoliAndroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate
            {
                var cn = (ConnectivityManager)GetSystemService(ConnectivityService);
                AnatoliClient.GetInstance(new AndroidWebClient(cn), new SQLiteAndroid(), new AndroidFileIO());
                ProductManager pm = new ProductManager();
                var p = pm.GetById(0);
                button.Text = p.Name;
            };

        }
    }
}

