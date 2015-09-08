using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.products
{
    public class Product
    {
        List<ProductField> _fields;
        
        public List<ProductField> Fields
        {
            get { return _fields; }
        }
        public static Product GetProduct(string productId)
        {
            return new Product();
        }
    }
}
