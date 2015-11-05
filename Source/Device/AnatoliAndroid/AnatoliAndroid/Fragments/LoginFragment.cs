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
using AnatoliAndroid.Fragments;
using AnatoliAndroid.Activities;
using Anatoli.App.Manager;
using Anatoli.App.Model.AnatoliUser;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("ورود")]
    public class LoginFragment : DialogFragment
    {
        EditText _userNameEditText;
        EditText _passwordEditText;
        TextView _loginResultTextView;
        Button _loginButton;
        TextView _registerTextView;
        Switch _saveSwitch;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.LoginLayout, container, false);
            _userNameEditText = view.FindViewById<EditText>(Resource.Id.userNameEditText);
            _passwordEditText = view.FindViewById<EditText>(Resource.Id.passwordEditText);
            _registerTextView = view.FindViewById<TextView>(Resource.Id.registerTextView);
            _registerTextView.Click += _registerTextView_Click;
            _loginResultTextView = view.FindViewById<TextView>(Resource.Id.loginResultTextView);
            _loginButton = view.FindViewById<Button>(Resource.Id.loginButton);
            _saveSwitch = view.FindViewById<Switch>(Resource.Id.saveSwitch);
            _loginButton.Click += loginButton_Click;
            return view;
        }

        void _registerTextView_Click(object sender, EventArgs e)
        {
            Dismiss();
            var fragment = new RegisterFragment();
            AnatoliApp.GetInstance().SetFragment<RegisterFragment>(fragment, "register_fragment");
        }
        async void loginButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_userNameEditText.Text) || String.IsNullOrEmpty(_passwordEditText.Text))
            {
                _loginResultTextView.Text = Resources.GetText(Resource.String.EneterUserNamePass);
                return;
            }
            _loginButton.Enabled = false;
            AnatoliApp.GetInstance().AnatoliUser = await AnatoliUserManager.LoginAsync(_userNameEditText.Text, _passwordEditText.Text);
            if (AnatoliApp.GetInstance().AnatoliUser != null)
            {
                try
                {
                    if (_saveSwitch.Checked)
                    {
                        await AnatoliUserManager.SaveUserInfoAsync(AnatoliApp.GetInstance().AnatoliUser);
                    }
                    AnatoliApp.GetInstance().RefreshMenuItems();
                    Dismiss();
                    AnatoliApp.GetInstance().SetFragment<ProductsListFragment>(new ProductsListFragment(), "products_fragment");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                _loginResultTextView.Text = Resources.GetText(Resource.String.LoginFailed);
            }
            _loginButton.Enabled = true;
        }
    }
}