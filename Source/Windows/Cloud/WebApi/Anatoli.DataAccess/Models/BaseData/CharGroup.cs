namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class CharGroup : AnatoliBaseModel
    {
        public int CharGroupCode { get; set; }
        [StringLength(200)]
        public string CharGroupName { get; set; }

        public virtual ICollection<CharType> CharTypes { get; set; }
    }
}
