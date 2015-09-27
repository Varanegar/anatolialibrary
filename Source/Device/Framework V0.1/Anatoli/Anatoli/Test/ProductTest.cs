using Anatoli.App.Adapter.Product;
using Anatoli.App.Manager.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Test
{
    public class ProductTest
    {
        public ProductTest()
        {

        }

        public static void TestProduct()
        {
            ProductManager pm = new ProductManager();
            pm.GetById(0);
        }
    }
}
