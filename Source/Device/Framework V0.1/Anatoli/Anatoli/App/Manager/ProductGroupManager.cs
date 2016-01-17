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
        public static async Task SyncDataBase()
        {
            try
            {
                var list = await GetListAsync(null, new RemoteQuery(TokenType.AppToken, Configuration.WebService.Products.ProductGroups));
                if (list.Count == 0)
                {
                    throw new Exception("Could not load groups data");
                }
                int c = await LocalUpdateAsync(new DeleteCommand("categories"));
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    foreach (var item in list)
                    {
                        InsertCommand command = new InsertCommand("categories", new BasicParam("cat_id", item.UniqueId),
                            new BasicParam("Id", item.Id),
                            new BasicParam("cat_name", item.GroupName.Trim()),
                            new BasicParam("cat_parent", item.ParentUniqueIdString),
                            new BasicParam("cat_left", item.NLeft.ToString()),
                            new BasicParam("cat_right", item.NRight.ToString()),
                            new BasicParam("cat_depth", item.NLevel.ToString()));
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
