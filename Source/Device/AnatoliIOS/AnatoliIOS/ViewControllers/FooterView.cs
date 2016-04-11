using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using ObjCRuntime;

namespace AnatoliIOS.ViewControllers
{
	partial class FooterView : UIView
	{
		public FooterView (IntPtr handle) : base (handle)
		{
		}
		public static FooterView Create(){
			var arr = NSBundle.MainBundle.LoadNib ("ProformaFooter", null, null);
			var v = Runtime.GetNSObject<FooterView> (arr.ValueAt(0));
			return v;
		}
	}
}
