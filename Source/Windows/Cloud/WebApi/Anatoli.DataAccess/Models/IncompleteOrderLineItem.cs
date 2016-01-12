namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class IncompleteOrderLineItem : BaseModel
    {
        public decimal Qty { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        [ForeignKey("IncompleteOrder")]
        public Guid IncompleteOrderId { get; set; }
        public virtual Product Product { get; set; }

        public virtual IncompleteOrder IncompleteOrder { get; set; }
    }
}
