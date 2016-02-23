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
    public class ResetPasswordDialog : DialogFragment
    {
        public string UserName;
        public string PassWord;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ResetPassWordLayout, container, false);

            EditText codeEditText = view.FindViewById<EditText>(Resource.Id.codeEditText);
            Button okButton = view.FindViewById<Button>(Resource.Id.okButton);
            okButton.UpdateWidth();
            Dialog.SetTitle(Resource.String.ChangePassword);
            Dialog.SetCanceledOnTouchOutside(false);

            okButton.Click += async delegate
            {
                ProgressDialog pDialog = new ProgressDialog(AnatoliApp.GetInstance().Activity);
                pDialog.SetMessage(AnatoliApp.GetResources().GetString(Resource.String.PleaseWait));
                pDialog.Show();
                try
                {
                    var result = await AnatoliUserManager.SendConfirmCode(UserName, codeEditText.Text.Trim());
                    pDialog.Dismiss();
                    if (result.IsValid)
                        OnPassWordChanged();
                    else
                        OnPassWordFailed(result.message);
                }
                catch (Exception)
                {
                    OnPassWordFailed("ÎØÇ!");
                }
                finally
                {
                    pDialog.Dismiss();
                    Dismiss();
                }
            };

            return view;
        }
        void OnPassWordChanged()
        {
            if (PassWordChanged != null)
            {
                PassWordChanged.Invoke(this, new EventArgs());
            }
        }
        public EventHandler PassWordChanged;
        void OnPassWordFailed(string msg)
        {
            if (PassWordFailed != null)
            {
                PassWordFailed.Invoke(msg);
            }
        }
        public event PassWordFailedEventHandler PassWordFailed;
        public delegate void PassWordFailedEventHandler(string msg);
    }
}