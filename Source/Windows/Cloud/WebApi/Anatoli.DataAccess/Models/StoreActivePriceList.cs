namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class StoreActivePriceList : BaseModel
    {
        public Nullable<decimal> Price { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
