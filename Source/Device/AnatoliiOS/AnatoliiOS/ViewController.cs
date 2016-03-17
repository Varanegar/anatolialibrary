using Anatoli.App.Manager;
using Anatoli.Framework.AnatoliBase;
using AnatoliIOS.Clients;
using System;
using UIKit;

namespace AnatoliIOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
        
            // Perform any additional setup after loading the view, typically from a nib.
            //AnatoliClient.GetInstance(new IosWebClient(), new IosSqliteClient(), new IosFileIO());
            //var result = await AnatoliUserManager.LoginAsync("09192403525", "123456");
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}