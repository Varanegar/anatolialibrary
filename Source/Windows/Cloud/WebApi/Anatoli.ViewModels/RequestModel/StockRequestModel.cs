using System;
using Anatoli.ViewModels.User;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.ViewModels
{
    public class StockRequestModel : BaseRequestModel
    {
        public List<string> stockIds { get; set; }

        public Guid stockId { get; set; }

        public List<StockActiveOnHandViewModel> stockActiveOnHandData { get; set; }
        public List<StockViewModel> stockData { get; set; }
        public List<StockProductViewModel> stockProductData { get; set; }
    }
}
