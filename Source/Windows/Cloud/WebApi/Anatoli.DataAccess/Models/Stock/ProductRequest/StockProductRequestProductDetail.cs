namespace Anatoli.DataAccess.Models
{
    using Anatoli.DataAccess.Models.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class StockProductRequestProductDetail : BaseModel
    {
        public decimal RequestQty { get; set; }
        [ForeignKey("StockProductRequestProduct")]
        public Guid StockProductRequestProductId { get; set; }
        public virtual StockProductRequestProduct StockProductRequestProduct { get; set; }
        [ForeignKey("StockProductRequestRule")]
        public Guid StockProductRequestRuleId { get; set; }
        public virtual StockProductRequestRule StockProductRequestRule { get; set; }
    }
}
