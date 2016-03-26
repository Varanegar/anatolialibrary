using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace AnatoliIOS
{
    public class AnatoliApp
    {

        private LinkedList<Type> _views;
        private static AnatoliApp _instance;
        public static AnatoliApp GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AnatoliApp();
            }
            return _instance;
        }
        private AnatoliApp()
        {
            _views = new LinkedList<Type>();
        }
        public void SetView(UIViewController viewController)
        {
            if (viewController == null)
            {
                throw new ArgumentNullException();
            }
            else if (_views.Count > 0 && _views.Last.GetType() != viewController.GetType())
            {
                (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.NavController.PushViewController(viewController, true);
            }
            else if (_views.Count == 0)
            {
                (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.NavController.PushViewController(viewController, true);
            }
        }
    }
}
