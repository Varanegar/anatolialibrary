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
    public class CityRegionManager : BaseManager<BaseDataAdapter<CityRegionModel>, CityRegionModel>
    {
        public static GroupLeftRightModel GetLeftRight(string groupId)
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
        public static async Task<List<CityRegionModel>> GetFirstLevelAsync()
        {
            var query = new SelectQuery("cityregion", new EqFilterParam("level", "1"));
            query.Unlimited = true;
            var list = await GetListAsync(query, null);
            return list;
        }
        public static async Task<List<CityRegionModel>> GetGroupsAsync(string groupId)
        {
            var query = new SelectQuery("cityregion", new EqFilterParam("parent_id", groupId));
            query.Unlimited = true;
            var list = await GetListAsync(query, null);
            return list;
        }

        public static async Task<List<CityRegionModel>> GetGroups(string groupId)
        {
            var query = new SelectQuery("cityregion", new EqFilterParam("parent_id", groupId));
            query.Unlimited = true;
            var list = await GetListAsync(query, null);
            return list;
        }
        public static async Task<CityRegionModel> GetParentGroup(string p)
        {
            var current = await GetItemAsync(new SelectQuery("cityregion", new EqFilterParam("group_id", p)));
            var parent = await GetItemAsync(new SelectQuery("cityregion", new EqFilterParam("group_id", current.parent_id)));
            return parent;
        }

        public static async Task<CityRegionModel> GetGroupInfo(string p)
        {
            var c = await GetItemAsync(new SelectQuery("cityregion", new EqFilterParam("group_id", p)));
            return c;
        }
    }
}
