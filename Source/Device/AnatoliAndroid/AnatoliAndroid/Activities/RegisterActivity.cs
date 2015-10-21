using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anatoli.Framework.AnatoliBase;
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
        EditText _confirmPassEditText;
        TextView _registerResultTextView;
        Button _registerButton;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.RegisterLayout);
            _userNameEditText = FindViewById<EditText>(Resource.Id.userNameEditText);
            _passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            _confirmPassEditText = FindViewById<EditText>(Resource.Id.confirmPassEditText);
            _registerResultTextView = FindViewById<TextView>(Resource.Id.registerResultTextView);
            _registerButton = FindViewById<Button>(Resource.Id.registerButton);
            _registerButton.Click += _registerButton_Click;
        }

        async void _registerButton_Click(object sender, EventArgs e)
        {
            if (!_passwordEditText.Text.Equals(_confirmPassEditText.Text))
            {
                _registerResultTextView.Text = "Password and confirm are not the same";
            }
            _registerResultTextView.Text = "Please wait";
            AnatoliUserManager um = new AnatoliUserManager();
            try
            {
                var result = await um.RegisterAsync(_userNameEditText.Text, _passwordEditText.Text, _passwordEditText.Text, "", "", "", "");
                if (result != null)
                {
                    _registerResultTextView.Text = "User " + result.UserName + " created successfully";
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(ServerUnreachable))
                {
                    _registerResultTextView.Text = "Server not available";
                }
                else
                {
                    _registerResultTextView.Text = ex.Message;
                }
            }
        }
    }
}