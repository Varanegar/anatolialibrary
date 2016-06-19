namespace Anatoli.DataAccess.Models
{  
	using System;
	using System.Collections.Generic;
  
    public class Supplier : AnatoliBaseModel
    {
        public string SupplierName { get; set; }
        public bool OrderAllProduct { get; set; }

        public virtual ICollection<StockProductRequestRule> StockProductRequestRules { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
