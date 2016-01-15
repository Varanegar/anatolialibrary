using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Store
{
    public class OrderItemModel : BaseDataModel
    {
        public int order_id { get; set; }
        public double item_price { get; set; }
        public string product_name { get; set; }
        public string product_id { get; set; }
        public int item_count { get; set; }
        public double product_price { get; set; }
        public int favorit { get; set; }
        public string image { get; set; }
        public bool IsFavorit { get { return (favorit == 1) ? true : false; } }
    }
}
