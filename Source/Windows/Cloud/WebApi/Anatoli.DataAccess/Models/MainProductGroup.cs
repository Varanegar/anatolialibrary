namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class MainProductGroup : BaseModel
    {
        public string GroupName { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public Nullable<int> Priority { get; set; }
        
        public Guid? ProductGroup2Id { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<StockProductRequestRule> StockProductRequestRules { get; set; }
        public virtual ICollection<MainProductGroup> ProductGroup1 { get; set; }
        [ForeignKey("ProductGroup2Id")]
        public virtual MainProductGroup ProductGroup2 { get; set; }
    }
}
