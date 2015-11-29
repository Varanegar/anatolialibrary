namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class ProductSupplierGuarantee : BaseModel
    {
        public int SupplierProductGuaranteeId { get; set; }
        public long GuaranteeTypeValueId { get; set; }
        public int GuaranteeDuration { get; set; }
        public string GuaranteeDesc { get; set; }
    }
}
