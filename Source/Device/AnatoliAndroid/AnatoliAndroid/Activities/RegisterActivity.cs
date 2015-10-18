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
using Anatoli.App.Manager;

namespace AnatoliAndroid.Activities
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        EditText _userNameEditText;
        EditText _passwordEditText;
        EditText _firstNameEditText;
        EditText _lastNameEditText;
        TextView _registerResultTextView;
        Button _registerButton;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.RegisterLayout);
            _userNameEditText = FindViewById<EditText>(Resource.Id.userNameEditText);
            _passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            _firstNameEditText = FindViewById<EditText>(Resource.Id.firstNameEditText);
            _lastNameEditText = FindViewById<EditText>(Resource.Id.lastNameEditText);
            _registerResultTextView = FindViewById<TextView>(Resource.Id.registerResultTextView);
            _registerButton = FindViewById<Button>(Resource.Id.registerButton);
            _registerButton.Click += _registerButton_Click;
            // Create your application here
        }

        async void _registerButton_Click(object sender, EventArgs e)
        {
            AnatoliUserManager um = new AnatoliUserManager();
            //var result = await um.RegisterAsync(_userNameEditText.Text, _passwordEditText.Text, _firstNameEditText.Text, _lastNameEditText.Text, "");
            //if (result.userModel == null)
            //{
                
            //}
            // todo : register
        }
    }
}