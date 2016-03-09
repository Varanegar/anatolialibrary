using System;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.ViewModels
{
    public class RequestModel
    {
        public string privateOwnerId { get; set; }
        public string stockId { get; set; }
        public string userId { get; set; }
        public string dateAfter { get; set; }
        public List<string> stockIds { get; set; }

        public string data { get; set; }
        public string ruleDate { get; set; }

        public string searchTerm { get; set; }
        public string stockRequestProduct { get; set; }
        public Guid ruleId { get; set; }

        public Guid stockProductRequestId { get; set; }
        public Guid stockProductRequestProductId { get; set; }

        public List<StockProductRequestProductViewModel> StockProductRequestProductList { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public List<StockActiveOnHandViewModel> StockActiveOnHand { get; set; }
    }
}
