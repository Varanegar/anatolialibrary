namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class StoreAction : BaseModel
    {
        public long StoreActionValueId { get; set; }
        public DateTime ActionDate { get; set; }
        public string ActionPDate { get; set; }
        public Nullable<TimeSpan> ActionTime { get; set; }
        public string ActionDesc { get; set; }
        public Nullable<int> ActionDataId { get; set; }
        //public Nullable<Guid> StoreId { get; set; }
        //public Guid StoreActionId { get; set; }
    
        public virtual Store Store { get; set; }
    }
}
