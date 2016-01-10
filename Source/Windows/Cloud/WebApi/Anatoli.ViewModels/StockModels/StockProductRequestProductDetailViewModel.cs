using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StockModels
{
    public class StockProductRequestProductDetailViewModel : BaseViewModel
    {
        public StockProductRequestProductDetailViewModel()
        {

        }

        public StockProductRequestProductDetailViewModel(Guid StockProductRequestProductId, Guid StockProductRequestRuleId, string RuleDesc, decimal RequestQty, bool IsMainRule)
        {
            this.UniqueId = Guid.NewGuid();
            this.StockProductRequestProductId = StockProductRequestProductId;
            this.StockProductRequestRuleId = StockProductRequestRuleId;
            this.RequestQty = RequestQty;
            this.IsMainRule = IsMainRule;

        }

        public decimal RequestQty { get; set; }
        public Guid StockProductRequestProductId { get; set; }
        public Guid StockProductRequestRuleId { get; set; }
        public string RuleDesc { get; set; }
        public bool IsMainRule { get; set; }

    }
}
