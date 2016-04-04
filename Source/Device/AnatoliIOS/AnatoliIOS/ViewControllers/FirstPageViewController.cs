using System;

using UIKit;
using System.Drawing;
using System.Collections.Generic;
using XpandItComponents;
using CoreGraphics;
using Foundation;

namespace AnatoliIOS.ViewControllers
{
	public partial class FirstPageViewController : ParallaxViewController
    {
        public FirstPageViewController()
			: base("FirstPageViewController", NSBundle.MainBundle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			StartAutomaticScroll ();
		}
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
			Title = "صفحه خانگی";





			SetImageHeight(UIScreen.MainScreen.Bounds.Height * 0.4f);

			// Creting a list UIImages to present in the ParallaxViewController
			var images = new List<UIImage>();
			images.Add(UIImage.FromBundle("igicon"));
			images.Add(UIImage.FromBundle("splash"));
			images.Add(UIImage.FromBundle("image3"));

			//View will be the ContentView of ParallaxViewController
			var view = new UIView(new CGRect(0, 0, View.Frame.Size.Width, 1000));
			view.BackgroundColor = UIColor.White;
			view.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin |
				UIViewAutoresizing.FlexibleRightMargin |
				UIViewAutoresizing.FlexibleWidth;

			//You can check if the image is tapped by set the ImageTapped property
			ImageTaped = (i) =>
			{
				UIAlertView alertView = new UIAlertView("Image tapped", "Image at index " + i, null, "Ok", null);
				alertView.Show();
			};

			SetupFor(view);
			SetImages(images);


        }
    }
}