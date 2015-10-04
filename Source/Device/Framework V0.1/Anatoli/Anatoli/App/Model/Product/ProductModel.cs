using Anatoli.Framework.Model;
using Anatoli.Anatoliclient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Product
{
    public class ProductModel : BaseDataModel
    {
        public int RateCount { get; set; }
        public ProductName Name { get; set; }
        public class ProductName
        {
            public string Name { get; set; }
            public static implicit operator ProductName(string name)
            {
                var p = new ProductName();
                p.Name = name;
                return p;
            }
            public static implicit operator string(ProductName pname)
            {
                return pname.Name;
            }
        }

        
    }
}
