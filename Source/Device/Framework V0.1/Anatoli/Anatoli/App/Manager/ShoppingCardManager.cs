using Anatoli.Framework.AnatoliBase;
using Anatoli.App.Model.Product;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.Manager;
using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Manager
{
    public class ShoppingCardManager : BaseManager<BaseDataAdapter<ProductModel>, ProductModel>
    {
        public static async Task<bool> AddProductAsync(ProductModel item)
        {
            DBQuery query = null;
            if (item.count == 0)
                query = new InsertCommand("shopping_card", new BasicParam("count", (item.count + 1).ToString()), new BasicParam("product_id", item.product_id.ToString()));
            else
                query = new UpdateCommand("shopping_card", new BasicParam("count", (item.count + 1).ToString()), new EqFilterParam("product_id", item.product_id.ToString()));
            return await LocalUpdateAsync(query) > 0 ? true : false;
        }
        public static async Task<bool> RemoveProductAsync(ProductModel item)
        {
            DBQuery query = null;
            if (item.count <= 1)
                query = new DeleteCommand("shopping_card", new SearchFilterParam("product_id", item.product_id.ToString()));
            else
                query = new UpdateCommand("shopping_card", new BasicParam("count", (item.count - 1).ToString()), new EqFilterParam("product_id", item.product_id.ToString()));
            return await LocalUpdateAsync(query) > 0 ? true : false;
        }

        public async static Task<double> GetTotalPriceAsync()
        {
            SelectQuery query = new SelectQuery("shopping_card_view");
            query.Unlimited = true;
            var result = await GetListAsync(query, null);
            double p = 0;
            foreach (var item in result)
            {
                p += (item.count * item.price);
            }
            return p;
        }
        public static List<ProductModel> GetAllItems()
        {
            SelectQuery query = new SelectQuery("shopping_card_view");
            query.Unlimited = true;
            return GetList(query, null);
        }
        public static async Task<List<ProductModel>> GetAllItemsAsync()
        {
            SelectQuery query = new SelectQuery("shopping_card_view");
            query.Unlimited = true;
            var list = await GetListAsync(query, null);
            return list;
        }

        public static async Task<bool> ClearAsync()
        {
            DeleteCommand command = new DeleteCommand("shopping_card");
            return (await LocalUpdateAsync(command) > 0) ? true : false;
        }

        public static async Task<int> GetItemsCountAsync()
        {
            var list = await GetAllItemsAsync();
            if (list != null)
            {
                int count = 0;
                foreach (var item in list)
                {
                    count += item.count;
                }
                return count;
            }
            else
                return 0;
        }
    }
}
