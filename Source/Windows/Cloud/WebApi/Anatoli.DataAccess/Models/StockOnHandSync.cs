namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class StockOnHandSync : BaseModel
    {
        public DateTime SyncDate { get; set; }
        public string SyncPDate { get; set; }
        [ForeignKey("Stock")]
        public Guid StockId { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual ICollection<StockActiveOnHand> StockActiveOnHands { get; set; }
        public virtual ICollection<StockProductRequest> StockProductRequests { get; set; }
        public virtual ICollection<StockHistoryOnHand> StockHistoryOnHands { get; set; }
    }
}
