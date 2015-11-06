using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Store
{
    public class OrderModel : BaseDataModel
    {
        public string order_id { get; set; }
        public string store_id { get; set; }
        public string store_name { get; set; }
        public int order_status { get; set; }
        public string order_date { get; set; }
        public double order_price { get; set; }
    }
}
