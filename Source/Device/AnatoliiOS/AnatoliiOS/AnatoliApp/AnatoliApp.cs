using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using AnatoliIOS.Components;
using AnatoliIOS.ViewControllers;

namespace AnatoliIOS
{
    public class AnatoliApp
    {

        private LinkedList<Type> _views;
        private static AnatoliApp _instance;
		public UITableView MenuTableViewReference;
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
        public void PushViewController(UIViewController viewController)
        {
            if (viewController == null)
            {
                throw new ArgumentNullException();
            }
			else if (_views.Count > 0 && _views.Last.Value != viewController.GetType())
            {
                (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.NavController.PushViewController(viewController, true);
				_views.AddLast (viewController.GetType ());
            }
            else if (_views.Count == 0)
            {
                (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.NavController.PushViewController(viewController, true);
				_views.AddLast (viewController.GetType ());
            }
        }
		public bool PopViewController(){
			if (_views.Count > 0) {
				_views.RemoveLast ();
				return true;
			} else
				return false;
		}
			
		public void RefreshMenu(){
			var source = new MenuTableViewSource();
			source.Items = new System.Collections.Generic.List<MenuItem>();

			source.Items.Add(new MenuItem() { Title = "ورود " , Icon = UIImage.FromBundle("ic_person_gray_24dp") , Type = MenuItem.MenuType.Login});
			source.Items.Add(new MenuItem() { Title = "دسته بندی کالا " , Icon = UIImage.FromBundle("ic_list_orange_24dp") , Type = MenuItem.MenuType.Products});
			MenuTableViewReference.Source = source;
			MenuTableViewReference.ReloadData ();
		}
		public void SelectMenuItem(int index){
			var items = (MenuTableViewReference.Source as MenuTableViewSource).Items;
			switch (items[index].Type) {
			case MenuItem.MenuType.Login:
				break;
			case MenuItem.MenuType.Products:
				PushViewController (new ProductsViewController ());
				break;
			default:
				break;
			}
		}
    }
}
