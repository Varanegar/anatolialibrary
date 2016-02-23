using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Store
{
    public class StoreActiveOnhandViewModel : BaseDataModel
    {
        public string store_id { get; set; }
        public string product_id { get; set; }
        public decimal qty { get; set; }
        public string StoreGuid { get { return (store_id == null) ? null : store_id.ToUpper(); } set { store_id = (value == null) ? null : value.ToUpper(); } }
        public string ProductGuid { get { return (product_id == null) ? null : product_id.ToUpper(); } set { product_id = (value == null) ? null : value.ToUpper(); } }
        public decimal Qty { get { return qty; } set { qty = value; } }

    }
}
