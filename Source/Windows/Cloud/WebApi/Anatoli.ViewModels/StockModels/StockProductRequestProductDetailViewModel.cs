using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StockModels
{
    public class StockProductRequestProductDetailViewModel : BaseViewModel
    {
        public decimal RequestQty { get; set; }
        public Guid StockProductRequestProductId { get; set; }
        public Guid StockProductRequestRuleId { get; set; }

    }
}
