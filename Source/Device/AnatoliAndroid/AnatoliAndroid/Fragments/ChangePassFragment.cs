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
using Anatoli.Framework.AnatoliBase;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("ورود")]
    public class ChangePassFragment : DialogFragment
    {
        EditText _currentPassEditText;
        EditText _passwordEditText;
        Button _saveButton;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ChangePassLayout, container, false);
            _currentPassEditText = view.FindViewById<EditText>(Resource.Id.currentPassEditText);
            _passwordEditText = view.FindViewById<EditText>(Resource.Id.passwordEditText);
            _saveButton = view.FindViewById<Button>(Resource.Id.saveButton);
            _saveButton.Click += async (s, e) =>
            {
                var result = await AnatoliUserManager.ChangePassword(_currentPassEditText.Text, _passwordEditText.Text);
                if (result != null)
                {

                    if (result.IsValid)
                    {
                        Console.WriteLine("Pass changed");
                    }
                    else
                        Console.WriteLine(result.ModelStateString);
                }
            };
            _saveButton.UpdateWidth();
            return view;
        }
    }
}