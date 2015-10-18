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

namespace AnatoliAndroid
{
    public class MenuDialogFragment : DialogFragment
    {
        private int _clickCount;
        public override void OnCreate(Bundle savedInstanceState)
        {
            _clickCount = 0;
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.MenuDialogFragmentLayout, container, false);

            // Set up a handler to dismiss this DialogFragment when this button is clicked.
            view.FindViewById<Button>(Resource.Id.button7).Click += (sender, args) => Dismiss();
            view.FindViewById<Button>(Resource.Id.loginButton).Click += (sender, args) => ShowLoginFragment();
            return view;
        }

        private void ShowLoginFragment()
        {
            Dismiss();
            Intent intent = new Intent(this.Activity, typeof(LoginActivity));
            StartActivityForResult(intent, 0);
        }
    }
}