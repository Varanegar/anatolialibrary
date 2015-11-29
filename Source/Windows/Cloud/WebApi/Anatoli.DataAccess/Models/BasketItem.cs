namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;    
    public class BasketItem : BaseModel
    {
        //public int BasketItemId { get; set; }
        //public int BasketId { get; set; }
        public Nullable<int> ProductId { get; set; }
        //public Nullable<int> ProductBaseId { get; set; }
        public Nullable<int> Qty { get; set; }
        public string Comment { get; set; }
    
        public virtual Basket Basket { get; set; }
    }
}
