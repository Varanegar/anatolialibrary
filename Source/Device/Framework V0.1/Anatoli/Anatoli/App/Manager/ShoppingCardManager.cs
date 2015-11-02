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
                query = new UpdateCommand("shopping_card", new BasicParam("count", (item.count + 1).ToString()), new SearchFilterParam("product_id", item.product_id.ToString()));
            return await LocalUpdateAsync(query) > 0 ? true : false;
        }
        public static async Task<bool> RemoveProductAsync(ProductModel item)
        {
            DBQuery query = null;
            if (item.count <= 1)
                query = new DeleteCommand("shopping_card", new SearchFilterParam("product_id", item.product_id.ToString()));
            else
                query = new UpdateCommand("shopping_card", new BasicParam("count", (item.count - 1).ToString()), new SearchFilterParam("product_id", item.product_id.ToString()));
            return await LocalUpdateAsync(query) > 0 ? true : false;
        }

        public static double GetTotalPrice()
        {
            DBQuery query = new SelectQuery("shopping_card_view");
            var result = GetList(query, null);
            double p = 0;
            foreach (var item in result)
            {
                p += (item.count * item.price);
            }
            return p;
        }
        public static List<ProductModel> GetAllItems()
        {
            DBQuery query = new SelectQuery("shopping_card_view");
            return GetList(query, null);
        }
        public static async Task<List<ProductModel>> GetAllItemsAsync()
        {
            DBQuery query = new SelectQuery("shopping_card_view");
            return await GetListAsync(query, null);
        }
    }
}
