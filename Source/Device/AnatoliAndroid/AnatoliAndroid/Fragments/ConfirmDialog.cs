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
using Anatoli.App.Manager;
using AnatoliAndroid.Activities;
using System.Threading.Tasks;

namespace AnatoliAndroid.Fragments
{
    public class ConfirmDialog : DialogFragment
    {
        public string UserName;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ConfirmDialog, container, false);
            EditText codeEditText = view.FindViewById<EditText>(Resource.Id.codeEditText);
            Button okButton = view.FindViewById<Button>(Resource.Id.okButton);
            TextView resendTextView = view.FindViewById<TextView>(Resource.Id.resendTextView);

            okButton.Click += async (s, e) =>
            {
                ProgressDialog pDialog = new ProgressDialog(AnatoliApp.GetInstance().Activity);
                pDialog.SetMessage(AnatoliApp.GetResources().GetString(Resource.String.PleaseWait));
                pDialog.Show();
                try
                {
                    var result = await AnatoliUserManager.SendConfirmCode(UserName, codeEditText.Text.Trim());
                    if (result.IsValid)
                        OnCodeConfirmed();
                    else
                        OnConfirmFailed();
                }
                catch (Exception)
                {
                    OnConfirmFailed();
                }
                finally
                {
                    pDialog.Dismiss();
                }
            };
            resendTextView.Click += async (s, e) =>
            {
                await RequestConfirmCode();
            };
            return view;
        }
        async Task<bool> RequestConfirmCode()
        {
            try
            {
                var result = await AnatoliUserManager.RequestConfirmCode(UserName);
                if (result.IsValid)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        void OnCodeConfirmed()
        {
            if (CodeConfirmed != null)
            {
                CodeConfirmed.Invoke(this, new EventArgs());
            }
        }
        public EventHandler CodeConfirmed;

        void OnConfirmFailed()
        {
            if (ConfirmFailed != null)
            {
                ConfirmFailed.Invoke(this, new EventArgs());
            }
        }
        public EventHandler ConfirmFailed;
    }
}