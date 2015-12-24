namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class StoreActiveOnHand : BaseModel
    {
        public int ProductId { get; set; }
        public Guid ProductGuid { get; set; }
        public decimal Qty { get; set; }
    
        public virtual Store Store { get; set; }
    }
}
