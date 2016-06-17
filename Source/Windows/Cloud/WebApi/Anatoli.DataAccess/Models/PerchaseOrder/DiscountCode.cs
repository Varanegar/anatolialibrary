namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class DiscountCode : AnatoliBaseModel
    {
        //public string DiscountCodeId { get; set; }
        public Guid DiscountCodeUniqueId { get; set; }
        [StringLength(200)]
        public string DiscountDesc { get; set; }
        public Nullable<long> DiscountTypeValueId { get; set; }
    }
}
