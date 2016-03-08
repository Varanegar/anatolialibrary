using System;
using System.Collections.Generic;

namespace Anatoli.ViewModels.StockModels
{
    public class StockProductRequestViewModel : BaseViewModel
    {
        public DateTime RequestDate { get; set; }
        public string RequestPDate { get; set; }
        public DateTime? Accept1Date { get; set; }
        public string Accept1PDate { get; set; }
        public DateTime? Accept2Date { get; set; }
        public string Accept2PDate { get; set; }
        public DateTime? Accept3Date { get; set; }
        public string Accept3PDate { get; set; }
        public DateTime? SendToSourceStockDate { get; set; }
        public string SendToSourceStockDatePDate { get; set; }
        public Guid? SourceStockRequestId { get; set; }

        public string RequestNo { get; set; }
        public string SourceStockRequestNo { get; set; }

        public DateTime? TargetStockIssueDate { get; set; }
        public string TargetStockIssueDatePDate { get; set; }
        public Guid? TargetStockPaperId { get; set; }
        public string TargetStockPaperNo { get; set; }
        public Guid StockProductRequestStatusId { get; set; }
        public Guid StockId { get; set; }
        public Guid? SupplyByStockId { get; set; }
        public Guid StockTypeId { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid StockProductRequestTypeId { get; set; }
        public Guid StockProductRequestSupplyTypeId { get; set; }
        public Guid ProductTypeId { get; set; }
        public Guid? Accept1ById { get; set; }
        public Guid? Accept2ById { get; set; }
        public Guid? Accept3ById { get; set; }
        public Guid? StockOnHandSyncId { get; set; }
        public List<StockProductRequestProductViewModel> StockProductRequestProducts { get; set; }

        public string AcceptName1 { get; set; }
        public string AcceptName2 { get; set; }
        public string AcceptName3 { get; set; }
        public string StockProductRequestStatus { get; set; }
        public string StockName { get; set; }
        public string SupplyType { get; set; }
        public string RequestType { get; set; }
        public string SupplyByStock { get; set; }
        public string SupplierName { get; set; }
    }
}
