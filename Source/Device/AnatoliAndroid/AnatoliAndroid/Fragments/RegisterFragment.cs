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
using AnatoliAndroid.Activities;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("ثبت نام")]
    public class RegisterFragment : DialogFragment
    {
        EditText _passwordEditText;
        EditText _telEditText;
        EditText _emailEditText;
        Button _registerButton;
        public override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.RegisterLayout, container, false);
            _passwordEditText = view.FindViewById<EditText>(Resource.Id.passwordEditText);
            _telEditText = view.FindViewById<EditText>(Resource.Id.telEditText);
            _emailEditText = view.FindViewById<EditText>(Resource.Id.emailEditText);
            _registerButton = view.FindViewById<Button>(Resource.Id.registerButton);
            _registerButton.UpdateWidth();
            _registerButton.Click += _registerButton_Click;
            return view;
        }
        async void _registerButton_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alertDialog = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
            if (String.IsNullOrEmpty(_telEditText.Text) || String.IsNullOrEmpty(_passwordEditText.Text))
            {
                alertDialog.SetMessage(AnatoliApp.GetResources().GetText(Resource.String.EneterUserNamePass));
                alertDialog.SetTitle("خطا");
                alertDialog.SetPositiveButton("خب", (s, ev) => { });
                alertDialog.Show();
                return;
            }
            _registerButton.Enabled = false;
            AnatoliUserManager usermanager = new AnatoliUserManager();
            ProgressDialog dialog = new ProgressDialog();
            dialog.SetMessage(AnatoliApp.GetResources().GetText(Resource.String.PleaseWait));
            dialog.SetCancel(() => { usermanager.CancelRegisterTask(); });
            dialog.Show();

            try
            {
                var result = await usermanager.RegisterAsync(_passwordEditText.Text, _passwordEditText.Text, _telEditText.Text, _emailEditText.Text);
                dialog.Dismiss();
                if (result.IsValid)
                {
                    alertDialog.SetMessage(AnatoliApp.GetResources().GetText(Resource.String.SaveSuccess));
                    alertDialog.SetPositiveButton("خب", async (s, a) =>
                    {
                        ProgressDialog pDialog = new ProgressDialog();
                        AlertDialog.Builder errDialog = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                        errDialog.SetPositiveButton("خب", (s1, e1) => { });

                        pDialog.SetTitle(Resources.GetText(Resource.String.Login));
                        pDialog.SetMessage(Resources.GetText(Resource.String.PleaseWait));
                        pDialog.Show();
                        try
                        {
                            var userModel = await AnatoliUserManager.LoginAsync(_telEditText.Text, _passwordEditText.Text);
                            pDialog.Dismiss();
                            if (userModel.IsValid)
                            {
                                AnatoliApp.GetInstance().AnatoliUser = userModel;
                                try
                                {
                                    await AnatoliUserManager.SaveUserInfoAsync(AnatoliApp.GetInstance().AnatoliUser);
                                    AnatoliApp.GetInstance().RefreshMenuItems();
                                    Dismiss();
                                    AnatoliApp.GetInstance().SetFragment<ProductsListFragment>(new ProductsListFragment(), "products_fragment");
                                }
                                catch (Exception ex)
                                {
                                    if (ex.GetType() == typeof(TokenException))
                                    {
                                        errDialog.SetMessage("خطا در ذخیره سازی اطلاعات");
                                        errDialog.Show();
                                    }
                                }
                            }
                            else
                            {
                                errDialog.SetTitle(Resources.GetText(Resource.String.LoginFailed));
                                errDialog.SetMessage(userModel.ModelStateString);
                                errDialog.Show();
                            }
                        }
                        catch (Exception ex)
                        {
                            pDialog.Dismiss();
                            if (ex.GetType() == typeof(ServerUnreachable))
                            {
                                errDialog.SetMessage(Resources.GetText(Resource.String.ServerUnreachable));
                                errDialog.Show();
                            }
                            else if (ex.GetType() == typeof(TokenException))
                            {
                                errDialog.SetMessage(Resources.GetText(Resource.String.AuthenticationFailed));
                                errDialog.Show();
                            }
                        }
                    });
                    alertDialog.Show();
                }
                else
                {
                    alertDialog.SetMessage(result.ModelStateString);
                    alertDialog.SetPositiveButton("خب", (s, a) => { });
                    alertDialog.Show();
                }
            }
            catch (Exception ex)
            {
                dialog.Dismiss();
                if (ex.GetType() == typeof(ServerUnreachable))
                {
                    alertDialog.SetMessage("خطا در برقراری ارتباط");
                    alertDialog.SetPositiveButton("خب", (s, a) => { });
                    alertDialog.Show();
                }
                else if (ex.GetType() == typeof(TokenException))
                {

                    alertDialog.SetMessage("خطای تصدیق هویت");
                    alertDialog.SetPositiveButton("خب", (s, a) => { });
                    alertDialog.Show();
                }
                else
                {
                    alertDialog.SetMessage(ex.Message);
                    alertDialog.SetPositiveButton("خب", (s, a) => { });
                    alertDialog.Show();
                }
            }
            _registerButton.Enabled = true;
        }
    }
}