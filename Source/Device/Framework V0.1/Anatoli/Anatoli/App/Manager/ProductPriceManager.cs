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
        public static async Task SyncDataBase(System.Threading.CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                var lastUpdateTiem = await SyncManager.GetLastUpdateDateAsync("products_price");
                var q = new RemoteQuery(TokenType.AppToken, Configuration.WebService.Stores.PricesView);
                q.cancellationTokenSource = cancellationTokenSource;
                var list = await GetListAsync(null, q);
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
                await SyncManager.SaveUpdateDateAsync("products_price");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
