namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class CityRegion : BaseModel
    {
        public string GroupName { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public Nullable<int> Priority { get; set; }
        //public Guid CityRegionId { get; set; }
        public Nullable<Guid> ParentId { get; set; }
    
        public virtual ICollection<StoreValidRegionInfo> StoreValidRegionInfoes { get; set; }
    }
}
