namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class CharType : BaseModel
    {
        public string CharTypeDesc { get; set; }
        public Guid DefaultCharValueGuid { get; set; }
    
        public virtual ICollection<CharGroup> CharGroups { get; set; }
        public virtual ICollection<CharValue> CharValues { get; set; }
    }
}
