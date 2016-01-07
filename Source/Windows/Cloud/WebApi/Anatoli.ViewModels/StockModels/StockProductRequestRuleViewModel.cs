using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StockModels
{
    public class StockProductRequestRuleViewModel : BaseViewModel
    {
        public string StockProductRequestRuleName { get; set; }
        public DateTime FromDate { get; set; }
        public string FromPDate { get; set; }
        public DateTime ToDate { get; set; }
        public string ToPDate { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? MainProductGroupId { get; set; }
        public Guid? ProductTypeId { get; set; }
    }
}
