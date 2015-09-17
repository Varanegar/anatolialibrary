using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AnatoliaLibrary;
using Parse;
using AnatoliaLibrary.user;
using Android.Net;
using AnatoliaLibrary.anatoliaclient;
using AnatoliaLibrary.products;

namespace AnatoliaAndroid
{
    [Activity(Label = "AnatoliaAndroid", MainLauncher = true, Icon = "@drawable/icon")]

    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += async delegate
            {
                var webClient = new AndroidWebClient((ConnectivityManager)GetSystemService(Context.ConnectivityService));
                var sqlite = new SQLiteAndroid();
                var fileIO = new AndroidFileIO();
                AnatoliaClient client = new AnatoliaClient(webClient, sqlite, fileIO);
                var user = new AnatoliaUserModel(client);
                await user.RegisterAsync("a.toraby", "Ali asghar", "Torabi");
                button.Text = "User was saved in parse with id: " + user.UserId;
                user.NewFavoritsModel("Weekly");
                var f = user.Favorits["Weekly"];
                f.AddItem(new ProductModel("123"));
                f.SaveAsync();
            };
        }
    }
}

