namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;    
    public class BasketItem : BaseModel
    {
        public Product Product { get; set; }
        public Nullable<int> Qty { get; set; }
        public string Comment { get; set; }
    
        public virtual Basket Basket { get; set; }
    }
}
