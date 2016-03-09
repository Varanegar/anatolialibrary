using System;
using System.Collections.Generic;

namespace Anatoli.ViewModels.StockModels
{
    public class StockProductRequestProductViewModel : BaseViewModel
    {
        public StockProductRequestProductViewModel()
        {

        }

        public StockProductRequestProductViewModel(Guid productGuid, Guid reorderCalcTypeId, Guid ruleId)
            : this(productGuid, 0, reorderCalcTypeId, ruleId, "", 0, false, null, null, null)
        {
        }

        public StockProductRequestProductViewModel(Guid productGuid, decimal requestedQty, Guid reorderCalcTypeId, Guid ruleId, 
                                                    string ruleDesc, decimal ruleQty, bool isMainRule, Guid? SupplyTypeId, Guid? productTypeId,
                                                    Guid? supplierGuid)
        {
            this.ProductId = productGuid;
            this.IsRelatedExtracted = false;
            this.RequestQty = requestedQty;
            this.ReorderCalcTypeId = reorderCalcTypeId;
            this.UniqueId = Guid.NewGuid();
            this.StockProductRequestSupplyTypeId = SupplyTypeId;
            this.ProductTypeId = productTypeId;
            this.StockProductRequestProductDetails = new List<StockProductRequestProductDetailViewModel>();
            this.StockProductRequestProductDetails.Add(new StockProductRequestProductDetailViewModel(this.UniqueId, ruleId, ruleDesc, ruleQty, isMainRule));
            this.SupplierGuid = supplierGuid;


        }

        public decimal RequestQty { get; set; }
        public decimal Accepted1Qty { get; set; }
        public decimal Accepted2Qty { get; set; }
        public decimal Accepted3Qty { get; set; }
        public decimal DeliveredQty { get; set; }
        public Guid ProductId { get; set; }
        public Guid StockProductRequestId { get; set; }
        public Guid? ProductGroupGuid { get; set; }
        public Guid? SupplierGuid { get; set; }
        public Guid? ManufactureGuid { get; set; }

        public bool IsRelatedExtracted { get; set; }
        //در فرایتد محاسبه برای استخراج کالاهای مرتبط استفاده می شود
        public Guid ReorderCalcTypeId { get; set; }
        //برای تعیین نوع درخواست در زمان تولید درخواست پارامتر موقت
        public Guid? ProductTypeId { get; set; }
        public Guid? StockProductRequestSupplyTypeId { get; set; }

        public List<StockProductRequestProductDetailViewModel> StockProductRequestProductDetails { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        public decimal MyAcceptedQty { get; set; }
        public decimal StockLevelQty { get; set; }
    }
}
