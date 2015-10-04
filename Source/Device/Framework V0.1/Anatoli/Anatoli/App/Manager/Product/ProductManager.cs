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
            return await GetAllAsync("SELECT * FROM products", new RemoteQueryParams(Configuration.WebService.Products.ProductsView, null));
        }
    }
}
