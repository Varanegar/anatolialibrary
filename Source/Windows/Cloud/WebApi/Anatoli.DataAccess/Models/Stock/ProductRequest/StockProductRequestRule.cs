using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Anatoli.DataAccess.Models
{
    public class StockProductRequestRule : AnatoliBaseModel
    {
        [StringLength(200)]
        public string StockProductRequestRuleName { get; set; }
        public DateTime FromDate { get; set; }
        [StringLength(10)]
        public string FromPDate { get; set; }
        public DateTime ToDate { get; set; }
        [StringLength(10)]
        public string ToPDate { get; set; }
        public decimal Qty { get; set; }
        [ForeignKey("Product")]
        public Nullable<Guid> ProductId { get; set; }
        [ForeignKey("MainProductGroup")]
        public Nullable<Guid> MainProductGroupId { get; set; }
        [ForeignKey("ProductType")]
        public Nullable<Guid> ProductTypeId { get; set; }
        [ForeignKey("Supplier")]
        public Nullable<Guid> SupplierId { get; set; }
        [ForeignKey("ReorderCalcType")]
        public Guid ReorderCalcTypeId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Product Product { get; set; }
        public virtual ReorderCalcType ReorderCalcType { get; set; }
        public virtual MainProductGroup MainProductGroup { get; set; }
        public virtual ProductType ProductType { get; set; }
        [ForeignKey("StockProductRequestRuleCalcType")]
        public Guid StockProductRequestRuleCalcTypeId { get; set; }
        public virtual StockProductRequestRuleCalcType StockProductRequestRuleCalcType { get; set; }
        [ForeignKey("StockProductRequestRuleType")]
        public Guid StockProductRequestRuleTypeId { get; set; }
        public virtual StockProductRequestRuleType StockProductRequestRuleType { get; set; }

        public virtual ICollection<StockProductRequestProductDetail> StockProductRequestProductDetails { get; set; }
    }
}
