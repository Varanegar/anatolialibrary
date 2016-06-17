namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class StockActiveOnHand : AnatoliBaseModel
    {

        public decimal Qty { get; set; }
        [ForeignKey("Stock")]
        public Guid StockId { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        [ForeignKey("StockOnHandSync")]
        public Guid StockOnHandSyncId { get; set; }

        public virtual StockOnHandSync StockOnHandSync { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual Product Product { get; set; }
    }
}
