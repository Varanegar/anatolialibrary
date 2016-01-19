using Anatoli.App.Model.Product;
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
    public class ProductGroupManager : BaseManager<BaseDataAdapter<ProductGroupModel>, ProductGroupModel>
    {
        public static async Task SyncDataBase(System.Threading.CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLastUpdateDateAsync("categories");
                var q = new RemoteQuery(TokenType.AppToken, Configuration.WebService.Products.ProductGroups + "&dateafter=" + lastUpdateTime.ToString(), new BasicParam("after", lastUpdateTime.ToString()));
                q.cancellationTokenSource = cancellationTokenSource;
                var list = await GetListAsync(null, q);
                Dictionary<string, CategoryInfoModel> items = new Dictionary<string, CategoryInfoModel>();
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    var query = connection.CreateCommand("SELECT * FROM categories");
                    var currentList = query.ExecuteQuery<CategoryInfoModel>();
                    foreach (var item in currentList)
                    {
                        items.Add(item.cat_id, item);
                    }
                }
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    foreach (var item in list)
                    {
                        if (items.ContainsKey(item.UniqueId))
                        {
                            UpdateCommand command = new UpdateCommand("categories", new EqFilterParam("cat_id", item.UniqueId.ToUpper()),
                           new BasicParam("cat_name", item.GroupName.Trim()),
                           new BasicParam("cat_parent", item.ParentUniqueIdString.ToUpper()),
                           new BasicParam("cat_left", item.NLeft.ToString()),
                           new BasicParam("cat_right", item.NRight.ToString()),
                           new BasicParam("cat_depth", item.NLevel.ToString()));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                        else
                        {
                            InsertCommand command = new InsertCommand("categories", new BasicParam("cat_id", item.UniqueId.ToUpper()),
                           new BasicParam("cat_name", item.GroupName.Trim()),
                           new BasicParam("cat_parent", item.ParentUniqueIdString.ToUpper()),
                           new BasicParam("cat_left", item.NLeft.ToString()),
                           new BasicParam("cat_right", item.NRight.ToString()),
                           new BasicParam("cat_depth", item.NLevel.ToString()));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                    }
                    connection.Commit();
                    await SyncManager.SaveUpdateDateAsync("categories");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
