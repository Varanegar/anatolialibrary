using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using ObjCRuntime;

namespace AnatoliIOS.ViewControllers
{
	partial class ProformHeader : UIView
	{
		public ProformHeader (IntPtr handle) : base (handle)
		{
		}
		public static ProformHeader Create(){
			var arr = NSBundle.MainBundle.LoadNib ("ProformHeader", null, null);
			var v = Runtime.GetNSObject<ProformHeader> (arr.ValueAt (0));
			return v;
		}
	}
}
