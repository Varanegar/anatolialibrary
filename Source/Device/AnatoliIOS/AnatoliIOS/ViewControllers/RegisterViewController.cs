using Anatoli.App.Manager;
using System;
using UIKit;

namespace AnatoliIOS.ViewControllers
{
    public partial class RegisterViewController : BaseController
    {
        public RegisterViewController()
            : base("RegisterViewController", null)
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
            Title = "À»  ‰«„";
            registerButton.TouchUpInside += async delegate
            {
                if (String.IsNullOrEmpty(phoneTextField.Text))
                {
                    return;
                }

                try
                {
                    await AnatoliUserManager.RegisterAsync(passwordTextField.Text, passwordTextField.Text, phoneTextField.Text, emailTextField.Text);
                    await AnatoliUserManager.LoginAsync(phoneTextField.Text,passwordTextField.Text);
                    AnatoliApp.GetInstance().Customer = await CustomerManager.ReadCustomerAsync();
                    AnatoliApp.GetInstance().User = await AnatoliUserManager.ReadUserInfoAsync();
                    AnatoliApp.GetInstance().RefreshMenu();
                    AnatoliApp.GetInstance().PushViewController(new ProfileViewController());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };
        }
    }
}