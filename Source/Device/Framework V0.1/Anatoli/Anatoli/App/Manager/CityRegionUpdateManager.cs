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
        public static async Task SyncDataBase()
        {
            try
            {
                //var lll = await AnatoliClient.GetInstance().WebClient.SendGetRequestAsync<List<StoreCalendarViewModel>>(TokenType.AppToken, Configuration.WebService.Stores.DeliveryTime);

                var list = await GetListAsync(null, new RemoteQuery(TokenType.AppToken, Configuration.WebService.Stores.CityRegion));
                if (list.Count == 0)
                {
                    throw new Exception("Could not download cities data");
                }
                int c = await LocalUpdateAsync(new DeleteCommand("cityregion"));
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    foreach (var item in list)
                    {
                        InsertCommand command = new InsertCommand("cityregion", new BasicParam("group_name", item.GroupName),
                            new BasicParam("group_id", item.UniqueId),
                            new BasicParam("parent_id", item.ParentUniqueIdString),
                            new BasicParam("level", item.NLevel.ToString()),
                            new BasicParam("left", item.NLeft.ToString()),
                            new BasicParam("right", item.NRight.ToString()));
                        var query = connection.CreateCommand(command.GetCommand());
                        int t = query.ExecuteNonQuery();
                    }
                    connection.Commit();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
