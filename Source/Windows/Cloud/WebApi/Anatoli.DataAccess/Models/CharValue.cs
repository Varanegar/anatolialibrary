namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class CharValue : BaseModel
    {
        public string CharValueText { get; set; }
        public Nullable<decimal> CharValueFromAmount { get; set; }
        public Nullable<decimal> CharValueToAmount { get; set; }
    
        public virtual CharType CharType { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
