namespace Anatoli.DataAccess.Models
{
    using Anatoli.DataAccess.Models.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Stock : BaseModel
    {
        public int StockCode { get; set; }
        public string StockName { get; set; }
        [ForeignKey("Store")]
        public Guid StoreId { get; set; }
        [ForeignKey("Accept1By")]
        public Nullable<Guid> Accept1ById { get; set; }
        [ForeignKey("Accept2By")]
        public Nullable<Guid> Accept2ById { get; set; }
        [ForeignKey("Accept3By")]
        public Nullable<Guid> Accept3ById { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<StockProduct> StockProducts { get; set; }
        public virtual ICollection<StockOnHandSync> StockOnHandSyncs { get; set; }
        public virtual ICollection<StockProductRequest> StockProductRequests { get; set; }
        public virtual Principal Accept1By { get; set; }
        public virtual Principal Accept2By { get; set; }
        public virtual Principal Accept3By { get; set; }
    }
}
    