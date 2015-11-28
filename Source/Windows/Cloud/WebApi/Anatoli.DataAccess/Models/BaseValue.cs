namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
        
    public class BaseValue : BaseModel
    {
        //public long BaseValueId { get; set; }
        public long BaseTypeId { get; set; }
        public string BaseValueName { get; set; }
    }
}
