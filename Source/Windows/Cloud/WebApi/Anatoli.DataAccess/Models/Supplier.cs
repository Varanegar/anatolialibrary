namespace Anatoli.DataAccess.Models
{  
	using System;
	using System.Collections.Generic;
  
    public class Supplier : BaseModel
    {
        //public int SupplierId { get; set; }
        public Nullable<Guid> SupplierUniqueId { get; set; }
        public string SupplierName { get; set; }
        public Nullable<int> SupplierMainAppId { get; set; }
    }
}
