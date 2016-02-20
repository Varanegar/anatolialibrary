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
using Parse;
using Anatoli.Framework;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("ورود")]
    public class LoginFragment : DialogFragment
    {
        EditText _userNameEditText;
        EditText _passwordEditText;
        Button _loginButton;
        Button _registerButton;
        TextView _fgTextView;
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
            _fgTextView = view.FindViewById<TextView>(Resource.Id.fgTextView);
            _registerButton = view.FindViewById<Button>(Resource.Id.registerButton);
            _registerButton.UpdateWidth();
            _registerButton.Click += _registerTextView_Click;
            _loginButton = view.FindViewById<Button>(Resource.Id.loginButton);
            _loginButton.UpdateWidth();
            _loginButton.Click += loginButton_Click;
            _fgTextView.Click += _fgTextView_Click;
            return view;
        }

        void _fgTextView_Click(object sender, EventArgs e)
        {
            ForgetPasswordDialog fDialog = new ForgetPasswordDialog();
            var t = AnatoliApp.GetInstance().Activity.FragmentManager.BeginTransaction();
            fDialog.Show(t, "forgetpass_dialog");
        }

        void _registerTextView_Click(object sender, EventArgs e)
        {
            Dismiss();
            var transaction = Activity.FragmentManager.BeginTransaction();
            var regFragment = new RegisterFragment();
            regFragment.Show(transaction, "register_fragment");
        }

        async void loginButton_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder errDialog = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
            errDialog.SetPositiveButton("خب", (s1, e1) => { });
            if (String.IsNullOrEmpty(_userNameEditText.Text) || String.IsNullOrEmpty(_passwordEditText.Text))
            {
                errDialog.SetMessage(Resources.GetText(Resource.String.EneterUserNamePass));
                errDialog.Show();
                return;
            }
            if (!AnatoliClient.GetInstance().WebClient.IsOnline())
            {
                errDialog.SetTitle(Resources.GetText(Resource.String.NetworkAccessFailed));
                errDialog.SetMessage(Resources.GetText(Resource.String.PleaseConnectToInternet));
                errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) =>
                {
                    Intent intent = new Intent(Android.Provider.Settings.ActionSettings);
                    AnatoliApp.GetInstance().Activity.StartActivity(intent);
                });
                errDialog.SetNegativeButton(Resource.String.Cancel, (s2, e2) => { });
                errDialog.Show();
                return;
            }
            _loginButton.Enabled = false;
            ProgressDialog pDialog = new ProgressDialog(AnatoliApp.GetInstance().Activity);
            try
            {
                pDialog.SetTitle(Resources.GetText(Resource.String.Login));
                pDialog.SetMessage(Resources.GetText(Resource.String.PleaseWait));
                pDialog.Show();
                var userModel = await AnatoliUserManager.LoginAsync(_userNameEditText.Text, _passwordEditText.Text);
                pDialog.Dismiss();
                if (userModel.IsValid)
                {
                    AnatoliApp.GetInstance().AnatoliUser = userModel;
                    try
                    {
                        await AnatoliUserManager.SaveUserInfoAsync(AnatoliApp.GetInstance().AnatoliUser);
                        await AnatoliApp.GetInstance().RefreshCutomerProfile();
                        AnatoliApp.GetInstance().RefreshMenuItems();
                        OnLoginSuccess();
                        ParseInstallation installation = ParseInstallation.CurrentInstallation;
                        try
                        {

                            installation["userUniqueId"] = userModel.UniqueId;
                            installation.AddUniqueToList("channels", "b2c");
#pragma warning disable
                            installation.SaveAsync();
#pragma warning restore
                        }
                        catch (Exception)
                        {
                        }
                        Dismiss();
                    }
                    catch (Exception ex)
                    {
                        HockeyApp.TraceWriter.WriteTrace(new AnatoliHandledException(ex), false);
                        if (ex.GetType() == typeof(TokenException))
                        {
                            errDialog.SetMessage("خطا در ذخیره سازی اطلاعات");
                            errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                            errDialog.Show();
                        }
                        else if (ex.GetType() == typeof(System.Threading.Tasks.TaskCanceledException))
                        {
                            errDialog.SetMessage("خطا در برقراری ارتباط");
                            errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                            errDialog.Show();
                        }
                    }
                }
                else
                {
                    errDialog.SetTitle(Resources.GetText(Resource.String.LoginFailed));
                    errDialog.SetMessage(userModel.ModelStateString);
                    errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                    errDialog.Show();
                }
            }
            catch (Exception ex)
            {
                HockeyApp.TraceWriter.WriteTrace(new AnatoliHandledException(ex), false);
                pDialog.Dismiss();
                if (ex.GetType() == typeof(ServerUnreachable))
                {
                    errDialog.SetMessage(Resources.GetText(Resource.String.ServerUnreachable));
                    errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                    errDialog.Show();
                }
                else if (ex.GetType() == typeof(TokenException))
                {
                    errDialog.SetMessage(Resources.GetText(Resource.String.AuthenticationFailed));
                    errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                    errDialog.Show();
                }
                else if (ex.GetType() == typeof(UnConfirmedUser))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(AnatoliApp.GetInstance().Activity);
                    alert.SetTitle(Resource.String.Error);
                    alert.SetMessage("شماره شما هنوز تایید نشده است. کد فعال سازی ارسال شود؟");
                    alert.SetPositiveButton(Resource.String.Yes, async (ssss, eeee) =>
                    {
                        await AnatoliUserManager.RequestConfirmCode(_userNameEditText.Text);
                        ConfirmDialog confirmDialog = new ConfirmDialog();
                        confirmDialog.UserName = _userNameEditText.Text;
                        confirmDialog.CodeConfirmed += async (sss, e2d) =>
                        {
                            // Login 
                            errDialog.SetPositiveButton(Resource.String.Ok, (s1, e1) => { });
                            pDialog.SetTitle(Resources.GetText(Resource.String.Login));
                            pDialog.SetMessage(Resources.GetText(Resource.String.PleaseWait));
                            pDialog.Show();
                            try
                            {
                                var userModel = await AnatoliUserManager.LoginAsync(_userNameEditText.Text, _passwordEditText.Text);
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
                                    catch (Exception ex2)
                                    {
                                        HockeyApp.TraceWriter.WriteTrace(new AnatoliHandledException(ex2), false);
                                        if (ex2.GetType() == typeof(TokenException))
                                        {
                                            errDialog.SetMessage(Resource.String.SaveFailed);
                                            errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                                            errDialog.Show();
                                        }

                                    }
                                }
                                else
                                {
                                    errDialog.SetTitle(Resources.GetText(Resource.String.LoginFailed));
                                    errDialog.SetMessage(userModel.ModelStateString);
                                    errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                                    errDialog.Show();
                                }
                            }
                            catch (Exception ex2)
                            {
                                HockeyApp.TraceWriter.WriteTrace(new AnatoliHandledException(ex2), false);
                                pDialog.Dismiss();
                                if (ex2.GetType() == typeof(ServerUnreachable))
                                {
                                    errDialog.SetMessage(Resources.GetText(Resource.String.ServerUnreachable));
                                    errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                                    errDialog.Show();
                                }
                                else if (ex2.GetType() == typeof(TokenException))
                                {
                                    errDialog.SetMessage(Resources.GetText(Resource.String.AuthenticationFailed));
                                    errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                                    errDialog.Show();
                                }
                                else if (ex.GetType() == typeof(System.Threading.Tasks.TaskCanceledException))
                                {
                                    errDialog.SetMessage("خطا در برقراری ارتباط");
                                    errDialog.SetPositiveButton(Resource.String.Ok, (s2, e2) => { });
                                    errDialog.Show();
                                }
                            }

                        };
                        confirmDialog.ConfirmFailed += (msg) =>
                        {
                            alert.SetMessage(msg);
                            alert.SetTitle(Resource.String.Error);
                            alert.Show();
                        };
                        FragmentTransaction t = AnatoliApp.GetInstance().Activity.FragmentManager.BeginTransaction();
                        confirmDialog.Show(t, "confirm_dialog");
                    });
                    alert.SetNegativeButton(Resource.String.Cancel, (ssss, eeee) => { });
                    alert.Show();
                }
            }
            _loginButton.Enabled = true;
        }

        void OnLoginSuccess()
        {
            if (LoginSuceeded != null)
            {
                LoginSuceeded.Invoke();
            }
        }

        public event LoginSuccessEventHandler LoginSuceeded;
        public delegate void LoginSuccessEventHandler();
    }
}