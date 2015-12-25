namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class StoreActiveOnHand : BaseModel
    {
        public decimal Qty { get; set; }
    
        public virtual Store Store { get; set; }
        public virtual Product Product { get; set; }
    }
}
