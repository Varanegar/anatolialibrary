using Anatoli.App.Model.Product;
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
    public class CityRegionManager : BaseManager<CityRegionModel>
    {
        public static async Task SyncDataBase(System.Threading.CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLastUpdateDateAsync(SyncManager.CityRegionTbl);
                var q = new RemoteQuery(TokenType.AppToken, Configuration.WebService.CityRegion + "&dateafter=" + lastUpdateTime.ToString(), new BasicParam("after", lastUpdateTime.ToString()));
                q.cancellationTokenSource = cancellationTokenSource;
                var list = await BaseDataAdapter<CityRegionUpdateModel>.GetListAsync(q);
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
                        if (items.ContainsKey(item.UniqueId.ToUpper()))
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
                await SyncManager.SaveUpdateDateAsync(SyncManager.CityRegionTbl);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static GroupLeftRightModel GetLeftRight(string groupId)
        {
            try
            {
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    SQLite.SQLiteCommand query;
                    if (groupId == null)
                        query = connection.CreateCommand("SELECT min(left) as left , max(right) as right FROM cityregion");
                    else
                        query = connection.CreateCommand(String.Format("SELECT left , right FROM cityregion WHERE cat_id ='{0}'", groupId));
                    var lr = query.ExecuteQuery<GroupLeftRightModel>();
                    return lr.First();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static async Task<List<CityRegionModel>> GetFirstLevelAsync()
        {
            try
            {
                var query = new StringQuery("SELECT * FROM cityregion WHERE level = 1");
                var list = await BaseDataAdapter<CityRegionModel>.GetListAsync(query);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static async Task<List<CityRegionModel>> GetGroupsAsync(string groupId)
        {
            try
            {
                var query = new StringQuery(string.Format("SELECT * FROM cityregion WHERE parent_id = '{0}'", groupId));
                var list = await BaseDataAdapter<CityRegionModel>.GetListAsync(query);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<CityRegionModel> GetParentGroup(string p)
        {
            try
            {
                var current = await BaseDataAdapter<CityRegionModel>.GetItemAsync(new StringQuery(string.Format("SELECT * FROM cityregion WHERE group_id='{0}'", p)));
                var parent = await BaseDataAdapter<CityRegionModel>.GetItemAsync(new StringQuery(string.Format("SELECT * FROM cityregion WHERE group_id='{0}'", current.parent_id)));
                return parent;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<CityRegionModel> GetGroupInfo(string p)
        {
            try
            {
                var c = await BaseDataAdapter<CityRegionModel>.GetItemAsync(new StringQuery(string.Format("SELECT * FROM cityregion WHERE group_id='{0}'", p)));
                return c;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
