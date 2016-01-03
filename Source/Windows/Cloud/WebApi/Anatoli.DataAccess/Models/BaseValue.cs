namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
        
    public class BaseValue : BaseModel
    {
        public string BaseValueName { get; set; }
        [ForeignKey("BaseType")]
        public Guid BaseTypeId { get; set; }
        public virtual BaseType BaseType { get; set; }
    }
}
