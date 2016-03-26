using System;
using Anatoli.ViewModels.User;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.ViewModels
{
    public class StockProductRequestRequestModel : BaseRequestModel
    {
        public Guid ruleId { get; set; }
        public string ruleDate { get; set; }
        public List<StockProductRequestProductViewModel> stockProductRequestProductData { get; set; }
        public List<StockProductRequestViewModel> stockProductRequestData { get; set; }
        public List<StockProductRequestTypeViewModel> stockProductRequestTypeData { get; set; }
        public string stockId { get; set; }
        public string stockRequestProduct { get; set; }
        public Guid stockProductRequestId { get; set; }
        public Guid stockProductRequestProductId { get; set; }
    }
}
