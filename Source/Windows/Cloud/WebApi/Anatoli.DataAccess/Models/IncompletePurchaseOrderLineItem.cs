namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class IncompletePurchaseOrderLineItem : BaseModel
    {
        public decimal Qty { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        [ForeignKey("IncompletePurchaseOrder")]
        public Guid IncompletePurchaseOrderId { get; set; }
        public virtual Product Product { get; set; }

        public virtual IncompletePurchaseOrder IncompletePurchaseOrder{ get; set; }
    }
}
