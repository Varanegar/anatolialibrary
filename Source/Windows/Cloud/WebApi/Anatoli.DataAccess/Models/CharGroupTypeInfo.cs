namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class CharGroupTypeInfo : BaseModel
    {
        //public int CharGroupId { get; set; }
        //public Nullable<Guid> CharTypeId { get; set; }
        //public Guid CharGroupTypeInfoId { get; set; }
    
        public virtual CharType CharType { get; set; }
    }
}
