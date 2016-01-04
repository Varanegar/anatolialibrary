namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class StockProduct : BaseModel
    {
        public decimal MinQty { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal MaxQty { get; set; }
        public Guid OrderType { get; set; }
        [ForeignKey("Stock")]
        public Guid StockId { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        [ForeignKey("ReorderCalcType")]
        public Nullable<Guid> ReorderCalcTypeId { get; set; }
        public virtual ReorderCalcType ReorderCalcType { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual Product Product { get; set; }
    }
}
