using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Store
{
    public class ProductPriceUpdateModel : BaseViewModel
    {
        public string StoreGuid { get; set; }
        public string ProductGuid { get; set; }
        public decimal? Price { get; set; }
        public string StoreGuidString { get; set; }
        public string ProductGuidString { get; set; }
    }
}
