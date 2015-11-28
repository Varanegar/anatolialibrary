namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class ProductSupplier : BaseModel
    {
        //public int ProductSupplierId { get; set; }
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public string Comment { get; set; }
    }
}
