namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;    
    public class BasketItem : BaseModel
    {
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }
        public Nullable<int> Qty { get; set; }
        public string Comment { get; set; }
        [ForeignKey("Basket")]
        public Guid BasketId { get; set; }
    
        public virtual Basket Basket { get; set; }
    }
}
