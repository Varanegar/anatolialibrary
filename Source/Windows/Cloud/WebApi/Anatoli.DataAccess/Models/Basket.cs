namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;  
      
    public class Basket : BaseModel
    {       
        public int BasketTypeValueId { get; set; }
        public string BasketName { get; set; }
        public Nullable<int> CustomerId { get; set; }

        public virtual ICollection<BasketItem> BasketItems { get; set; }
        public virtual ICollection<BasketNote> BasketNotes { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
