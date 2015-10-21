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
    [Activity(Label = "ورود")]
    class LoginActivity : Activity
    {
        EditText _userNameEditText;
        EditText _passwordEditText;
        TextView _loginResultTextView;
        Button _loginButton;
        TextView _registerTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginLayout);
            _userNameEditText = FindViewById<EditText>(Resource.Id.userNameEditText);
            _passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            _registerTextView = FindViewById<TextView>(Resource.Id.registerTextView);
            _registerTextView.Click += _registerTextView_Click;
            _loginResultTextView = FindViewById<TextView>(Resource.Id.loginResultTextView);
            _loginButton = FindViewById<Button>(Resource.Id.loginButton);
            _loginButton.Click += loginButton_Click;
        }

        void _registerTextView_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RegisterActivity));
            StartActivityForResult(intent, 0);
        }

        async void loginButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_userNameEditText.Text) || String.IsNullOrEmpty(_passwordEditText.Text))
            {
                _loginResultTextView.Text = "Please input user name and password";
                return;
            }
            AnatoliUserManager um = new AnatoliUserManager();
            var user = await um.LoginAsync(_userNameEditText.Text, _passwordEditText.Text);

        }

    }
}