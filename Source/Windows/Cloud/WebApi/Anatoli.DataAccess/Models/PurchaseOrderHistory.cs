namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class PurchaseOrderHistory : BaseModel
    {
        //public int PurchaseOrderHistoryId { get; set; }
        public Nullable<long> PurchaseOrderStatusValueId { get; set; }
        //public int PurchaseOrderId { get; set; }
        public Nullable<DateTime> StatusDate { get; set; }
        [StringLength(10)]
        public string StatusPDate { get; set; }
        public Nullable<TimeSpan> StatusTime { get; set; }
        public Nullable<int> PurchaseOrderStatusDataId { get; set; }
        public Nullable<Guid> CreateBy { get; set; }
        public Nullable<DateTime> CreateDate { get; set; }
    
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
