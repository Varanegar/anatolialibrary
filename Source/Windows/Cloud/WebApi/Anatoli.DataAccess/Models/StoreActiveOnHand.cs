namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class StoreActiveOnHand : BaseModel
    {
        //public int StoreActiveOnHandId { get; set; }
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
        //public Nullable<Guid> StoreId { get; set; }
    
        public virtual Store Store { get; set; }
    }
}
