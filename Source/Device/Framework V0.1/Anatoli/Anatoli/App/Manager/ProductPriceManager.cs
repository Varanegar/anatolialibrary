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
    public class ProductPriceManager : BaseManager<BaseDataAdapter<ProductPriceUpdateModel>, ProductPriceUpdateModel>
    {
        public static async Task SyncDataBase(System.Threading.CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLastUpdateDateAsync("products_price");
                var q = new RemoteQuery(TokenType.AppToken, Configuration.WebService.Stores.PricesView + "&dateafter=" + lastUpdateTime.ToString(), new BasicParam("after", lastUpdateTime.ToString()));
                q.cancellationTokenSource = cancellationTokenSource;
                var list = await GetListAsync(null, q);
                Dictionary<string, ProductPriceModel> items = new Dictionary<string, ProductPriceModel>();
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    var query = connection.CreateCommand("SELECT * FROM products_price");
                    var currentList = query.ExecuteQuery<ProductPriceModel>();
                    foreach (var item in currentList)
                    {
                        items.Add(item.product_id, item);
                    }
                }
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    foreach (var item in list)
                    {
                        if (items.ContainsKey(item.UniqueId))
                        {
                            UpdateCommand command = new UpdateCommand("products_price", new BasicParam("price", item.Price.ToString()),
                            new EqFilterParam("product_id", item.ProductGuid.ToUpper()));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                        else
                        {
                            InsertCommand command = new InsertCommand("products_price", new BasicParam("price", item.Price.ToString()),
                            new BasicParam("product_id", item.ProductGuid.ToUpper()));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
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
