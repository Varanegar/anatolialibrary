using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

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
        public DateTime? SendtoSourceStockDate { get; set; }
        public string SendtoSourceStockDatePDate { get; set; }
        public Guid? SrouceStockRequestId { get; set; }
        public string SrouceStockRequestNo { get; set; }
        public DateTime TargetStockIssueDate { get; set; }
        public string TargetStockIssueDatePDate { get; set; }
        public Guid TargetStockPaperId { get; set; }
        public Guid StockProductRequestStatusId { get; set; }
        public Guid StockId { get; set; }
        public Guid StockTypeId { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? ReorderCalcTypeId { get; set; }
        public Guid StockProductRequestTypeId { get; set; }
        public Guid PorductTypeId { get; set; }
        public Guid? Accept1ById { get; set; }
        public Guid? Accept2ById { get; set; }
        public Guid? Accept3ById { get; set; }
        public Guid StockOnHandSyncId { get; set; }
    }
}
