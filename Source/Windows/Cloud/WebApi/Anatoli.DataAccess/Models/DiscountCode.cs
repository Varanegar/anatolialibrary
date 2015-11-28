namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class DiscountCode : BaseModel
    {
        //public string DiscountCodeId { get; set; }
        public Guid DiscountCodeUniqueId { get; set; }
        public string DiscountDesc { get; set; }
        public Nullable<long> DiscountTypeValueId { get; set; }
    }
}
