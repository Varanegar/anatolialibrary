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
using Anatoli.App.Model.AnatoliUser;

namespace AnatoliIOS
{
	public class AnatoliApp
	{

		private LinkedList<Type> _views;
		private static AnatoliApp _instance;
		public UITableView MenuTableViewReference;
		public StoreDataModel DefaultStore;

		public Anatoli.App.Model.CustomerViewModel Customer { get; set; }

		public AnatoliUserModel User { get; set; }

		public async Task Initialize ()
		{
			try {
				Customer = await CustomerManager.ReadCustomerAsync ();
				User = await AnatoliUserManager.ReadUserInfoAsync ();
				DefaultStore = await StoreManager.GetDefaultAsync ();
			} catch (Exception) {

			}
		}

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
			AnatoliClient.GetInstance ().WebClient.TokenExpire += delegate {
			};
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

		public void ReplaceViewController (UIViewController viewController)
		{
			var allviewControllers = (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.NavController.ViewControllers;
			UIViewController[] newViewControllerStack = new UIViewController[allviewControllers.Length - 1];
			for (int i = 0; i < allviewControllers.Length - 1; i++) {
				newViewControllerStack [i] = allviewControllers [i];
			}
			(UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.NavController.ViewControllers = newViewControllerStack;
			PushViewController (viewController);
		}

		public void PresentViewController (UIViewController view)
		{
			(UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.NavController.PresentViewController (view, true, null);
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

			if (Customer == null) {
				source.Items.Add (new MenuItem () {
					Title = "ورود ",
					Icon = UIImage.FromBundle ("ic_log_in_green_24dp.png"),
					Type = MenuItem.MenuType.Login
				});
			} else {
				if (Customer != null && Customer.FirstName != null && Customer.LastName != null) {
					source.Items.Add (new MenuItem () {
						Title = Customer.FirstName + " " + Customer.LastName,
						Icon = UIImage.FromBundle ("ic_person_gray_24dp"),
						Type = MenuItem.MenuType.Profile
					});
				} else {
					source.Items.Add (new MenuItem () {
						Title = "پروفایل ",
						Icon = UIImage.FromBundle ("ic_person_gray_24dp"),
						Type = MenuItem.MenuType.Profile
					});
				}
			}
			source.Items.Add (new MenuItem () {
				Title = "صفحه اول",
				Type = MenuItem.MenuType.FirstPage
			});
			source.Items.Add (new MenuItem () {
				Title = "دسته بندی کالا ",
				Icon = UIImage.FromBundle ("ic_list_orange_24dp"),
				Type = MenuItem.MenuType.Products
			});
			MenuTableViewReference.Source = source;
			MenuTableViewReference.ReloadData ();
		}

		public void SelectMenuItem (int index)
		{
			var items = (MenuTableViewReference.Source as MenuTableViewSource).Items;
			switch (items [index].Type) {
			case MenuItem.MenuType.FirstPage:
				PushViewController (new FirstPageViewController ());
				break;
			case MenuItem.MenuType.Favorits:
				PushViewController (new FavoritsViewController ());
				break;
			case MenuItem.MenuType.Login:
				PushViewController (new LoginViewController ());
				break;
			case MenuItem.MenuType.Profile:
				PushViewController (new ProfileViewController ());
				break;
			case MenuItem.MenuType.Products:
				if (DefaultStore != null) {
					PushViewController (new ProductsViewController ());
				} else {
					PushViewController (new StoresViewController ());
				}

				break;
			default:
				break;
			}
		}

		public async Task LogOutAsync ()
		{
			await AnatoliUserManager.LogoutAsync ();
			Customer = null;
			User = null;
			RefreshMenu ();
			ReplaceViewController (new FirstPageViewController ());
		}


		public UIBarButtonItem CreateMenuButton(){
			return new UIBarButtonItem (UIImage.FromBundle ("ic_reorder_white_24dp").Scale (new CoreGraphics.CGSize (26, 26))
				, UIBarButtonItemStyle.Plain
				, (sender, args) => {
				(UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.SidebarController.ToggleMenu ();
			});
		}

	}

	public static class Extensions
	{
		public static UIColor FromHex (this UIColor color, int hexValue)
		{
			return UIColor.FromRGB (
				(((float)((hexValue & 0xFF0000) >> 16)) / 255.0f),
				(((float)((hexValue & 0xFF00) >> 8)) / 255.0f),
				(((float)(hexValue & 0xFF)) / 255.0f)
			);
		}
	}


}
