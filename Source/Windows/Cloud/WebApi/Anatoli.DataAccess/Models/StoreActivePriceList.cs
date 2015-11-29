namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class StoreActivePriceList : BaseModel
    {
        //public int StoreActivePriceListId { get; set; }
        public Nullable<decimal> Price { get; set; }
        //public Nullable<Guid> StoreId { get; set; }
        public Nullable<Guid> ProductId { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
