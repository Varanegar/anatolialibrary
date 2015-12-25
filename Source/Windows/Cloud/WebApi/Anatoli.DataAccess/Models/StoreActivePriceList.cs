namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class StoreActivePriceList : BaseModel
    {
        public decimal Price { get; set; }
        [ForeignKey("Store")]
        public Guid StoreId { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
