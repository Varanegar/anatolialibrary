using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using AnatoliIOS.ViewControllers;
using Anatoli.App.Manager;
using System.Threading.Tasks;
using Anatoli.Framework.AnatoliBase;
using AnatoliIOS.Clients;
using Anatoli.App.Model.Store;

namespace AnatoliIOS
{
	public class AnatoliApp
	{

		private LinkedList<Type> _views;
		private static AnatoliApp _instance;
		public UITableView MenuTableViewReference;
		public StoreDataModel DefaultStore;
		public static AnatoliApp GetInstance ()
		{
			if (_instance == null) {
				_instance = new AnatoliApp ();
			}
			return _instance;
		}

		private AnatoliApp ()
		{
			AnatoliClient.GetInstance (new IosWebClient (), new IosSqliteClient (), new IosFileIO ());
			_views = new LinkedList<Type> ();
		}

		public async Task SyncDataBase ()
		{
			var updateTime = await SyncManager.GetLogAsync (SyncManager.UpdateCompleted);
			if (updateTime < (DateTime.Now - TimeSpan.FromDays (1000))) {
				try {
					SyncManager.ProgressChanged += (status, step) => {
						Console.WriteLine (status);
					};
					await SyncManager.SyncDatabase ();

				} catch (Exception ex) {
					Console.WriteLine (ex.Message);
				}
			}
		}

		public void PushViewController (UIViewController viewController)
		{
			if (viewController == null) {
				throw new ArgumentNullException ();
			} else if (_views.Count > 0 && _views.Last.Value != viewController.GetType ()) {
				(UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.NavController.PushViewController (viewController, true);
				_views.AddLast (viewController.GetType ());
			} else if (_views.Count == 0) {
				(UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.NavController.PushViewController (viewController, true);
				_views.AddLast (viewController.GetType ());
			}
		}

		public bool PopViewController ()
		{
			if (_views.Count > 0) {
				_views.RemoveLast ();
				return true;
			} else
				return false;
		}

		public void RefreshMenu ()
		{
			var source = new MenuTableViewSource ();
			source.Items = new System.Collections.Generic.List<MenuItem> ();

			source.Items.Add (new MenuItem () {
				Title = "ورود " ,
				Icon = UIImage.FromBundle ("ic_person_gray_24dp") ,
				Type = MenuItem.MenuType.Login
			});
			source.Items.Add (new MenuItem () {
				Title = "دسته بندی کالا " ,
				Icon = UIImage.FromBundle ("ic_list_orange_24dp") ,
				Type = MenuItem.MenuType.Products
			});
			MenuTableViewReference.Source = source;
			MenuTableViewReference.ReloadData ();
		}

		public void SelectMenuItem (int index)
		{
			var items = (MenuTableViewReference.Source as MenuTableViewSource).Items;
			switch (items [index].Type) {
			case MenuItem.MenuType.Login:
				break;
			case MenuItem.MenuType.Products:
				if (DefaultStore != null) {
					PushViewController (new ProductsViewController ());	
				} else {
					PushViewController(new StoresViewController());
				}

				break;
			default:
				break;
			}
		}
	}
}
