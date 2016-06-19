namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class PurchaseOrderStatusHistory : AnatoliBaseModel
    {
        public Guid StatusValueId { get; set; }
        public DateTime StatusDate { get; set; }
        [StringLength(10)]
        public string StatusPDate { get; set; }
        [StringLength(100)]
        public string Comment { get; set; }
        [ForeignKey("PurchaseOrder")]
        public Guid PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
