using Anatoli.Framework.AnatoliBase;
using Anatoli.App.Model.Store;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.Manager;
using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Anatoli.App.Manager
{
    public class StoreManager : BaseManager<StoreDataModel>
    {
        public static async Task SyncDataBase(System.Threading.CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLogAsync(SyncManager.StoresTbl);
                RemoteQuery q;
                if (lastUpdateTime == DateTime.MinValue)
                    q = new RemoteQuery(TokenType.AppToken, Configuration.WebService.Stores.StoresView, HttpMethod.Get);
                else
                    q = new RemoteQuery(TokenType.AppToken, Configuration.WebService.Stores.StoresViewAfter + "&dateafter=" + lastUpdateTime.ToString(), HttpMethod.Get,new BasicParam("after", lastUpdateTime.ToString()));
                q.cancellationTokenSource = cancellationTokenSource;
                var list = await BaseDataAdapter<StoreUpdateModel>.GetListAsync(q);
                Dictionary<string, StoreDataModel> items = new Dictionary<string, StoreDataModel>();
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    var query = connection.CreateCommand("SELECT * FROM stores");
                    var currentList = query.ExecuteQuery<StoreDataModel>();
                    foreach (var item in currentList)
                    {
                        if (!items.ContainsKey(item.store_id))
                        {
                            items.Add(item.store_id, item);
                        }
                    }
                }
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    foreach (var item in list)
                    {
                        if (items.ContainsKey(item.UniqueId))
                        {
                            UpdateCommand command = new UpdateCommand("stores", new EqFilterParam("store_id", item.UniqueId.ToUpper()),
                            new BasicParam("store_name", item.storeName.Trim()),
                            new BasicParam("store_tel", item.Phone),
                            new BasicParam("lat", item.lat.ToString()),
                            new BasicParam("lng", item.lng.ToString()),
                            new BasicParam("store_address", item.address));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                        else
                        {
                            InsertCommand command = new InsertCommand("stores", new BasicParam("store_id", item.UniqueId.ToUpper()),
                            new BasicParam("store_name", item.storeName.Trim()),
                            new BasicParam("store_tel", item.Phone),
                            new BasicParam("lat", item.lat.ToString()),
                            new BasicParam("lng", item.lng.ToString()),
                            new BasicParam("store_address", item.address));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                    }
                    connection.Commit();
                }



                var q2 = new RemoteQuery(TokenType.AppToken, Configuration.WebService.Stores.StoreCalendar, HttpMethod.Get);
                q2.cancellationTokenSource = cancellationTokenSource;
                var list2 = await BaseDataAdapter<StoreCalendarViewModel>.GetListAsync(q2);


                Dictionary<string, StoreCalendarViewModel> timeItems = new Dictionary<string, StoreCalendarViewModel>();
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    var query = connection.CreateCommand("SELECT * FROM stores_calendar");
                    var currentList = query.ExecuteQuery<StoreCalendarViewModel>();
                    foreach (var item in currentList)
                    {
                        if (!timeItems.ContainsKey(item.UniqueId))
                        {
                            timeItems.Add(item.UniqueId, item);
                        }
                    }
                }
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    foreach (var item in list2)
                    {
                        if (timeItems.ContainsKey(item.UniqueId))
                        {
                            UpdateCommand command = new UpdateCommand(SyncManager.StoreCalendarTbl, new EqFilterParam("UniqueId", item.UniqueId),
                                new BasicParam("StoreId", item.StoreId),
                                new BasicParam("Date", item.Date.ConvertToUnixTimestamp().ToString()),
                            new BasicParam("PDate", item.PDate),
                            new BasicParam("FromTimeString", item.FromTimeString),
                            new BasicParam("ToTimeString", item.ToTimeString),
                            new BasicParam("CalendarTypeValueId", item.CalendarTypeValueId.ToUpper()));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                        else
                        {
                            InsertCommand command = new InsertCommand(SyncManager.StoreCalendarTbl, new BasicParam("UniqueId", item.UniqueId),
                            new BasicParam("StoreId", item.StoreId),
                            new BasicParam("Date", item.Date.ConvertToUnixTimestamp().ToString()),
                            new BasicParam("PDate", item.PDate),
                            new BasicParam("FromTimeString", item.FromTimeString),
                            new BasicParam("ToTimeString", item.ToTimeString),
                            new BasicParam("CalendarTypeValueId", item.CalendarTypeValueId.ToUpper()));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                    }
                    connection.Commit();
                }
                await SyncManager.AddLogAsync(SyncManager.StoreCalendarTbl);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static StringQuery Search(string value)
        {
            StringQuery query = new StringQuery(string.Format("SELECT * FROM stores WHERE store_name LIKE '%{0}%'", value));
            return query;
        }
		public static StringQuery GetAll(){
			StringQuery query = new StringQuery(string.Format("SELECT * FROM stores"));
			return query;
		}
        public static async Task<bool> SelectAsync(StoreDataModel store)
        {
            UpdateCommand command1 = new UpdateCommand("stores", new BasicParam("selected", "0"));
            UpdateCommand command2 = new UpdateCommand("stores", new BasicParam("selected", "1"), new EqFilterParam("store_id", store.store_id));
            try
            {
                int clear = await DataAdapter.UpdateItemAsync(command1);
                int result = await DataAdapter.UpdateItemAsync(command2);
                return (result > 0) ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static async Task<StoreDataModel> GetDefaultAsync()
        {
            SelectQuery query = new SelectQuery("stores", new EqFilterParam("selected", "1"));
            try
            {
                var store = await BaseDataAdapter<StoreDataModel>.GetItemAsync(query);
                if (store == null)
                    return null;
                return store;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<bool> UpdateDistanceAsync(string store_id, float dist)
        {
            UpdateCommand command = new UpdateCommand("stores", new BasicParam("distance", dist.ToString()), new EqFilterParam("store_id", store_id));
            try
            {
                int result = await DataAdapter.UpdateItemAsync(command);
                return (result > 0) ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
