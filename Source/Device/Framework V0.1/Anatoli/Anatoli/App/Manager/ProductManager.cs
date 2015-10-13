using Anatoli.Anatoliclient;
using Anatoli.App.Model.Product;
using Anatoli.Framework.AnatoliBase;
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
    public class ProductManager : BaseManager<BaseDataAdapter<ProductModel>, ProductModel>
    {
        protected override string GetDataTable()
        {
            return "products";
        }

        protected override string GetWebServiceUri()
        {
            return Configuration.WebService.Products.ProductsView;
        }
    }
}
