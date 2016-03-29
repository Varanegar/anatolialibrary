﻿using Anatoli.App.Manager;
using System;
using UIKit;
using Anatoli.Framework.AnatoliBase;
namespace AnatoliIOS.ViewControllers
{
    public partial class LoginViewController : BaseController
    {
        public LoginViewController()
            : base("LoginViewController", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            EdgesForExtendedLayout = UIRectEdge.None;
            loginButton.TouchUpInside += async delegate
            {
                if (String.IsNullOrEmpty(userNameTextField.Text) || String.IsNullOrEmpty(passwordTextField.Text))
                {
                    var alert = UIAlertController.Create("خطا", "ورود نام کاربری و کلمه عبور اجباریست!", UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("خب", UIAlertActionStyle.Default, null));
                    PresentViewController(alert, true, null);
                }
                try
                {
                    var result = await AnatoliUserManager.LoginAsync(userNameTextField.Text, passwordTextField.Text);
                    if (result != null)
                    {
                        if (result.IsValid)
                        {
                            AnatoliApp.GetInstance().Customer = await CustomerManager.ReadCustomerAsync();
                            AnatoliApp.GetInstance().RefreshMenu();
                            AnatoliApp.GetInstance().PushViewController(new ProductsViewController());
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(TokenException))
                    {
                        var alert = UIAlertController.Create("خطا", "نام کاربری یا کلمه عبور اشتباه است", UIAlertControllerStyle.Alert);
                        alert.AddAction(UIAlertAction.Create("باشه", UIAlertActionStyle.Default, null));
                        PresentViewController(alert, true, null);
                    }
                }
            };
        }
    }
}