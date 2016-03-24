namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class StoreAction : BaseModel
    {
        public long StoreActionValueId { get; set; }
        public DateTime ActionDate { get; set; }
        [StringLength(10)]
        public string ActionPDate { get; set; }
        public Nullable<TimeSpan> ActionTime { get; set; }
        [StringLength(100)]
        public string ActionDesc { get; set; }
        public Nullable<int> ActionDataId { get; set; }
    
        public virtual Store Store { get; set; }
    }
}
