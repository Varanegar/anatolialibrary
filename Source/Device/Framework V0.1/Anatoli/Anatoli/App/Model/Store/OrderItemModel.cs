using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Store
{
    public class OrderItemModel
    {
        public int order_id { get; set; }
        public int product_id { get; set; }
        public int product_count { get; set; }
        public double product_price { get; set; }
    }
}
