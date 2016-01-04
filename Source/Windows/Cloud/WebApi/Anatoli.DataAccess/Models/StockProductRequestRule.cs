using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Anatoli.DataAccess.Models
{
    public class StockProductRequestRule : BaseModel
    {
        public string StockProductRequestRuleName { get; set; }
        public DateTime FromDate { get; set; }
        public string FromPDate { get; set; }
        public TimeSpan FromTime { get; set; }
        public DateTime ToDate { get; set; }
        public string ToPDate { get; set; }
        public TimeSpan ToTime { get; set; }
        [ForeignKey("Product")]
        public Nullable<Guid> ProductId { get; set; }
        [ForeignKey("MainProductGroup")]
        public Nullable<Guid> MainProductGroupId { get; set; }
        [ForeignKey("ProductType")]
        public Nullable<Guid> ProductTypeId { get; set; }
        public virtual Product Product { get; set; }
        public virtual MainProductGroup MainProductGroup { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual ICollection<StockProductRequestProductDetail> StockProductRequestProductDetails { get; set; }
    }
}
