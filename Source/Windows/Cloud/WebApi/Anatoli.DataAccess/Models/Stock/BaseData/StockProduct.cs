namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class StockProduct : AnatoliBaseModel
    {
        public decimal MinQty { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal MaxQty { get; set; }
        public bool IsEnable { get; set; }
        [ForeignKey("Stock")]
        public Guid StockId { get; set; }
        [ForeignKey("FiscalYear")]
        public Guid FiscalYearId { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        [ForeignKey("ReorderCalcType")]
        public Nullable<Guid> ReorderCalcTypeId { get; set; }
        [ForeignKey("StockProductRequestSupplyType")]
        public Nullable<Guid> StockProductRequestSupplyTypeId { get; set; }
        public virtual StockProductRequestSupplyType StockProductRequestSupplyType { get; set; }
        public virtual ReorderCalcType ReorderCalcType { get; set; }
        public virtual FiscalYear FiscalYear { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual Product Product { get; set; }
    }
}
