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
    public class ProductPriceManager : BaseManager<BaseDataAdapter<ProductPriceModel>, ProductPriceModel>
    {
        public static async Task SyncDataBase()
        {
            try
            {
                var list = await GetListAsync(null, new RemoteQuery(TokenType.AppToken, Configuration.WebService.Stores.PricesView));
                int c = await LocalUpdateAsync(new DeleteCommand("products_price"));
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    foreach (var item in list)
                    {
                        InsertCommand command = new InsertCommand("products_price", new BasicParam("price", item.Price.ToString()),
                            new BasicParam("product_id", item.ProductGuid.ToUpper()));
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
