using Anatoli.App.Model.Store;
using Anatoli.Framework.AnatoliBase;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Manager
{
    public class CityRegionUpdateManager : BaseManager<BaseDataAdapter<CityRegionUpdateModel>, CityRegionUpdateModel>
    {
        public static async Task SyncDataBase(System.Threading.CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLastUpdateDateAsync("cityregion");
                var q = new RemoteQuery(TokenType.AppToken, Configuration.WebService.CityRegion + "&dateafter=" + lastUpdateTime.ToString(), new BasicParam("after", lastUpdateTime.ToString()));
                q.cancellationTokenSource = cancellationTokenSource;
                var list = await GetListAsync(null, q);
                Dictionary<string, CityRegionModel> items = new Dictionary<string, CityRegionModel>();
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    var query = connection.CreateCommand("SELECT * FROM cityregion");
                    var currentList = query.ExecuteQuery<CityRegionModel>();
                    foreach (var item in currentList)
                    {
                        items.Add(item.group_id, item);
                    }
                }
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    foreach (var item in list)
                    {
                        if (items.ContainsKey(item.UniqueId))
                        {
                            UpdateCommand command = new UpdateCommand("cityregion", new BasicParam("group_name", item.GroupName),
                            new EqFilterParam("group_id", item.UniqueId.ToUpper()),
                            new BasicParam("parent_id", item.ParentUniqueIdString.ToUpper()),
                            new BasicParam("level", item.NLevel.ToString()),
                            new BasicParam("left", item.NLeft.ToString()),
                            new BasicParam("right", item.NRight.ToString()));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                        else
                        {
                            InsertCommand command = new InsertCommand("cityregion", new BasicParam("group_name", item.GroupName),
                                new BasicParam("group_id", item.UniqueId.ToUpper()),
                                new BasicParam("parent_id", item.ParentUniqueIdString.ToUpper()),
                                new BasicParam("level", item.NLevel.ToString()),
                                new BasicParam("left", item.NLeft.ToString()),
                                new BasicParam("right", item.NRight.ToString()));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                    }

                    connection.Commit();
                }
                await SyncManager.SaveUpdateDateAsync("cityregion");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
