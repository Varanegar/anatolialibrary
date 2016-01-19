using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AnatoliAndroid.Activities;

namespace AnatoliAndroid.Fragments
{
    public class ProgressDialog : Android.App.ProgressDialog
    {
        public delegate void CancelFunc();
        CancelFunc _cancel;
        public void SetCancel(CancelFunc function)
        {
            _cancel = function;
        }
        public void SetMessage(string message)
        {
            if (message != null)
            {
                var msgTextView = FindViewById<TextView>(Resource.Id.msgTextView);
                msgTextView.Text = message;
            }
        }
        public ProgressDialog(bool Cancelable = false)
            : base(AnatoliApp.GetInstance().Activity)
        {

            SetContentView(Resource.Layout.ProgressDialog);
            var msgTextView = FindViewById<TextView>(Resource.Id.msgTextView);
            var cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
            if (Cancelable)
                cancelButton.Click += cancelButton_Click;
            else
            {
                cancelButton.Visibility = ViewStates.Gone;
                SetCancelable(false);
            }
        }


        void cancelButton_Click(object sender, EventArgs e)
        {
            if (_cancel != null)
            {
                _cancel();
            }
            Dismiss();
        }
    }
}