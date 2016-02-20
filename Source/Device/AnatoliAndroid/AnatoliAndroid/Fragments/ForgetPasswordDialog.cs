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
using Anatoli.App.Manager;

namespace AnatoliAndroid.Fragments
{
    public class ForgetPasswordDialog : DialogFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ForgetPasswordLayout, container, false);
            var editText1 = view.FindViewById<EditText>(Resource.Id.editText1);
            var editText2 = view.FindViewById<EditText>(Resource.Id.editText2);
            var editText3 = view.FindViewById<EditText>(Resource.Id.editText3);
            var button1 = view.FindViewById<Button>(Resource.Id.button1);
            button1.Click += async (s, e) =>
            {
                AlertDialog.Builder eAlert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                if (editText2.Text.Equals(editText3.Text))
                {
                    var result = await AnatoliUserManager.ResetPassword(editText1.Text, editText2.Text);
                    ResetPasswordDialog resetDialog = new ResetPasswordDialog();
                    resetDialog.UserName = editText1.Text;
                    var t = AnatoliApp.GetInstance().Activity.FragmentManager.BeginTransaction();
                    resetDialog.Show(t, "confirm_dialog");
                    resetDialog.PassWordChanged += (s2, e2) =>
                    {
                        eAlert.SetMessage("کلمه عبور با موفقیت تغییر یافت");
                        eAlert.Show();
                        eAlert.SetPositiveButton(Resource.String.Ok, (s3, e3) =>
                        {
                            Dismiss();
                        });
                    };
                    resetDialog.PassWordFailed += (msg) =>
                    {
                        eAlert.SetTitle(Resource.String.Error);
                        eAlert.SetMessage(msg);
                        eAlert.SetPositiveButton(Resource.String.Ok, (s3, e3) =>
                        {
                            Dismiss();
                        });
                        eAlert.Show();
                    };
                }
                else
                {
                    eAlert.SetTitle(Resource.String.Error);
                    eAlert.SetMessage("کلمه عبور و تکرار آن یکسان نیستند");
                    eAlert.SetPositiveButton(Resource.String.Ok, (s3, e3) => { });
                    eAlert.Show();
                }
            };
            return view;
        }
    }
}