namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class ProductSupplierGuarantee : AnatoliBaseModel
    {
        public int SupplierProductGuaranteeId { get; set; }
        public long GuaranteeTypeValueId { get; set; }
        public int GuaranteeDuration { get; set; }
        [StringLength(200)]
        public string GuaranteeDesc { get; set; }
    }
}
