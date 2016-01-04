namespace Anatoli.DataAccess.Models
{
    using Anatoli.DataAccess.Models.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class StockProductRequestProduct : BaseModel
    {
        public decimal RequestQty { get; set; }
        public decimal Accepted1Qty { get; set; }
        public decimal Accepted2Qty { get; set; }
        public decimal Accepted3Qty { get; set; }
        public decimal DeliveredQty { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        [ForeignKey("StockProductRequest")]
        public Guid StockProductRequestId { get; set; }
        public virtual Product Product { get; set; }
        public virtual StockProductRequest StockProductRequest { get; set; }
    }
}
