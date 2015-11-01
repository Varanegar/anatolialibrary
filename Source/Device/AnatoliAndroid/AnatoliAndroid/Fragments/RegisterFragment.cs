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

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("ثبت نام")]
    public class RegisterFragment : Fragment
    {
        EditText _userNameEditText;
        EditText _passwordEditText;
        EditText _confirmPassEditText;
        TextView _registerResultTextView;
        Button _registerButton;
        public override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.RegisterLayout, container, false);
            _userNameEditText = view.FindViewById<EditText>(Resource.Id.userNameEditText);
            _passwordEditText = view.FindViewById<EditText>(Resource.Id.passwordEditText);
            _confirmPassEditText = view.FindViewById<EditText>(Resource.Id.confirmPassEditText);
            _registerResultTextView = view.FindViewById<TextView>(Resource.Id.registerResultTextView);
            _registerButton = view.FindViewById<Button>(Resource.Id.registerButton);
            _registerButton.Click += _registerButton_Click;
            return view;
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