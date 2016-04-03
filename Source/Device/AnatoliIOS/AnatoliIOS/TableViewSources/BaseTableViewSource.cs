using System;
using UIKit;
using Anatoli.Framework.Model;
using Anatoli.Framework.Manager;
using System.Collections.Generic;
using Anatoli.App.Manager;
using System.Threading.Tasks;
using Anatoli.Framework.AnatoliBase;

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
		}
		public void SetDataQuery(DBQuery query){
			DataManager.SetQueries (query, null);
		}
		public async Task RefreshAsync(){
			Items = await DataManager.GetNextAsync ();
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return Items.Count;
		}
	}
}

