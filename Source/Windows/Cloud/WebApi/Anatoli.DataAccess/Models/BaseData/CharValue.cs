namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class CharValue : AnatoliBaseModel
    {
        [StringLength(200)]
        public string CharValueText { get; set; }
        public Nullable<decimal> CharValueFromAmount { get; set; }
        public Nullable<decimal> CharValueToAmount { get; set; }
        [ForeignKey("CharType")]
        public Guid CharTypeId { get; set; }
        public virtual CharType CharType { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
