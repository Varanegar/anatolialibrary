using Anatoli.Anatoliclient;
using Anatoli.App.Adapter.Product;
using Anatoli.App.Model.Product;
using Anatoli.Framework.AnatoliBase;
using Anatoli.Framework.Manager;
using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Manager.Product
{
    public class ProductManager : BaseManager<ProductAdapter, ProductListModel, ProductModel>
    {
        public async Task<ProductListModel> GetFrequentlyOrderedProducts(int count)
        {
            return await GetAllAsync(string.Format("SELECT * FROM products ORDER BY `order_count` DESC LIMIT {0}", count.ToString()), new RemoteQueryParams(Configuration.WebService.Products.ProductsView, new Tuple<string, string>("count", count.ToString())));
        }
    }
}
