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
    public class OrderManager : BaseManager<BaseDataAdapter<OrderModel>, OrderModel>
    {
        public static async Task<bool> SaveOrder()
        {
            try
            {
                var store = await StoreManager.GetDefault();
                InsertCommand command = new InsertCommand("orders", new BasicParam("store_id", store.store_id.ToString()),
                    new BasicParam("order_status", "0"),
                    new BasicParam("order_date", DateTime.Now.ToLocalTime().ToString())
                    );
                var result = await LocalUpdateAsync(command);
                if (result > 0)
                {
                    var latestOrder = await GetLatestOrder();
                    List<List<BasicParam>> parametres = new List<List<BasicParam>>();
                    var items = await ShoppingCardManager.GetAllItemsAsync();
                    foreach (var item in items)
                    {
                        var p = new List<BasicParam>();
                        p.Add(new BasicParam("order_id", latestOrder.order_id));
                        p.Add(new BasicParam("product_id", item.product_id.ToString()));
                        p.Add(new BasicParam("product_count", item.count.ToString()));
                        p.Add(new BasicParam("product_price", item.price.ToString()));
                        parametres.Add(p);
                    }
                    InsertAllCommand command2 = new InsertAllCommand("order_items", parametres);
                    var r = await LocalUpdateAsync(command2);
                    if (r > 0)
                    {
                        await ShoppingCardManager.ClearAsync();
                        return true;
                    }
                }
                return false;
            }

            catch (Exception)
            {
                throw;
            }
        }
        public static async Task<OrderModel> GetLatestOrder()
        {
            SelectQuery query = new SelectQuery("orders", new SortParam("order_id", SortTypes.DESC));
            return await GetItemAsync(query);
        }
    }
}
