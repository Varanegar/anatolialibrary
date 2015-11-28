namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class ProductGroup : BaseModel
    {
        //public Guid ProductGroupId { get; set; }
        public string GroupName { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public Nullable<int> Priority { get; set; }
        public int CharGroupId { get; set; }
        //public Nullable<Guid> ParentId { get; set; }
    
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductGroup> ProductGroup1 { get; set; }
        public virtual ProductGroup ProductGroup2 { get; set; }
    }
}
