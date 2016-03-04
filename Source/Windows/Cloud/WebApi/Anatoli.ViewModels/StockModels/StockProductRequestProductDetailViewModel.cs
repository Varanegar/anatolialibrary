using System;

namespace Anatoli.ViewModels.StockModels
{
    public class StockProductRequestProductDetailViewModel : BaseViewModel
    {
        public StockProductRequestProductDetailViewModel() { }

        public StockProductRequestProductDetailViewModel(Guid stockProductRequestProductId, Guid stockProductRequestRuleId, string ruleDesc,
                                                         decimal requestQty, bool isMainRule)
        {
            UniqueId = Guid.NewGuid();

            StockProductRequestProductId = stockProductRequestProductId;
            StockProductRequestRuleId = stockProductRequestRuleId;
            RuleDesc = RuleDesc;
            RequestQty = requestQty;
            IsMainRule = isMainRule;
        }

        public decimal RequestQty { get; set; }
        public Guid StockProductRequestProductId { get; set; }
        public Guid StockProductRequestRuleId { get; set; }
        public string RuleDesc { get; set; }
        public bool IsMainRule { get; set; }
        public string StockProductRequestRuleName { get; set; }
    }
}
