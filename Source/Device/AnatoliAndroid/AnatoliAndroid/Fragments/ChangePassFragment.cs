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
            AlertDialog.Builder alert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
            _saveButton.Click += async (s, e) =>
            {
                ProgressDialog pDialog = new ProgressDialog(AnatoliApp.GetInstance().Activity);
                pDialog.SetMessage("در حال ارسال درخواست");
                pDialog.Show();
                try
                {
                    var result = await AnatoliUserManager.ChangePassword(_currentPassEditText.Text, _passwordEditText.Text);
                    pDialog.Dismiss();
                    if (result != null)
                    {

                        if (result.IsValid)
                        {
                            alert.SetMessage("کلمه عبور با موفقیت تغییر کرد");
                            alert.Show();
                        }
                        else
                        {
                            alert.SetMessage("تغییر کلمه عبور با خطا مواحه شد");
                            alert.Show();
                        }
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    pDialog.Dismiss();
                }
            };
            _saveButton.UpdateWidth();
            return view;
        }
    }
}