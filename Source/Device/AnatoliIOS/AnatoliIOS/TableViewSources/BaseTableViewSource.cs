using System;
using UIKit;
using Anatoli.Framework.Model;
using Anatoli.Framework.Manager;
using System.Collections.Generic;
using Anatoli.App.Manager;
using System.Threading.Tasks;

namespace AnatoliIOS.TableViewSources
{
	public abstract class BaseTableViewSource<BaseDataManager,DataModel> : UITableViewSource
			where DataModel : BaseViewModel , new()
			where BaseDataManager : BaseManager<DataModel>, new()
	{
		protected List<DataModel> Items { get; set; }
		protected BaseDataManager DataManager;
		public BaseTableViewSource ()
		{
			Items = new List<DataModel> ();
			DataManager = new BaseDataManager ();
			DataManager.SetQueries (ProductManager.GetAll ("111"),null);
		}
		public async Task Refresh(){
			Items = await DataManager.GetNextAsync ();
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return Items.Count;
		}
	}
}

