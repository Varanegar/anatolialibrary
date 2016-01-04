namespace Anatoli.DataAccess.Models
{
    using Anatoli.DataAccess.Models.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class StockProductRequest : BaseModel
    {
        public DateTime RequestDate { get; set; }
        public string RequestPDate { get; set; }
        public Nullable<DateTime> Accept1Date { get; set; }
        public string Accept1PDate { get; set; }
        public Nullable<DateTime> Accept2Date { get; set; }
        public string Accept2PDate { get; set; }
        public Nullable<DateTime> Accept3Date { get; set; }
        public string Accept3PDate { get; set; }
        public Nullable<DateTime> SendtoSourceStockDate { get; set; }
        public string SendtoSourceStockDatePDate { get; set; }
        public Nullable<Guid> SrouceStockRequestId { get; set; }
        public string SrouceStockRequestNo { get; set; }
        public Nullable<DateTime> TargetStockIssueDate { get; set; }
        public string TargetStockIssueDatePDate { get; set; }
        public Nullable<Guid> TargetStockPaperId { get; set; }
        public string TargetStockPaperNo { get; set; }
        [ForeignKey("StockProductRequestStatus")]
        public Guid StockProductRequestStatusId { get; set; }
        [ForeignKey("Stock")]
        public Guid StockId { get; set; }
        [ForeignKey("StockType")]
        public Guid StockTypeId { get; set; }
        [ForeignKey("Supplier")]
        public Nullable<Guid> SupplierId { get; set; }
        [ForeignKey("ReorderCalcType")]
        public Nullable<Guid> ReorderCalcTypeId { get; set; }
        [ForeignKey("StockProductRequestType")]
        public Guid StockProductRequestTypeId { get; set; }
        [ForeignKey("ProductType")]
        public Guid PorductTypeId { get; set; }

        [ForeignKey("Accept1By")]
        public Nullable<Guid> Accept1ById { get; set; }
        [ForeignKey("Accept2By")]
        public Nullable<Guid> Accept2ById { get; set; }
        [ForeignKey("Accept3By")]
        public Nullable<Guid> Accept3ById { get; set; }
        [ForeignKey("StockOnHandSync")]
        public Guid StockOnHandSyncId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ReorderCalcType ReorderCalcType { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual StockType StockType { get; set; }
        public virtual StockOnHandSync StockOnHandSync { get; set; }
        public virtual Principal Accept1By { get; set; }
        public virtual Principal Accept2By { get; set; }
        public virtual Principal Accept3By { get; set; }
        public virtual StockProductRequestType StockProductRequestType { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual StockProductRequestStatus StockProductRequestStatus { get; set; }
        public virtual ICollection<StockProductRequestProduct> StockProductRequestProducts { get; set; }
    }
}
