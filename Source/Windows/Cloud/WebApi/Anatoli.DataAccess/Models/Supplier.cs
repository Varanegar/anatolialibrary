namespace Anatoli.DataAccess.Models
{  
	using System;
	using System.Collections.Generic;
  
    public class Supplier : BaseModel
    {
        public string SupplierName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
