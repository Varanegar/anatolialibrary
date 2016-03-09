using System;

namespace Anatoli.ViewModels.StockModels
{
    public class StockProductRequestRuleViewModel : BaseViewModel
    {
        public static readonly StockProductRequestRuleViewModel ReorderRule = new StockProductRequestRuleViewModel()
        {
            UniqueId = Guid.Parse("cc9b0f95-c067-4ee4-bff9-e1fb19853f52"),
            StockProductRequestRuleName = "»— «”«” ‰ﬁÿÂ ”›«—‘"
        };
        public StockProductRequestRuleViewModel()
        {

        }
        public string StockProductRequestRuleName { get; set; }
        public DateTime FromDate { get; set; }
        public string FromPDate { get; set; }
        public DateTime ToDate { get; set; }
        public string ToPDate { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? MainProductGroupId { get; set; }
        public Guid? ProductTypeId { get; set; }
        public Guid RuleTypeId { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid ReorderCalcTypeId { get; set; }
        public decimal Qty { get; set; }
        public dynamic RuleCalcTypeId { get; set; }
        public string ProductName { get; set; }
        public string SupplierName { get; set; }
        public string MainProductGroupName { get; set; }
    }
}
