namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class CharType : BaseModel
    {
        public string CharTypeDesc { get; set; }
        //public Guid CharTypeId { get; set; }
        public Nullable<int> DefaultCharValueID { get; set; }
    
        public virtual ICollection<CharGroupTypeInfo> CharGroupTypeInfoes { get; set; }
        public virtual ICollection<CharValue> CharValues { get; set; }
    }
}
