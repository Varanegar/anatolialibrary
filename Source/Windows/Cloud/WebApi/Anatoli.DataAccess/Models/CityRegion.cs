namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class CityRegion : BaseModel
    {
        public string GroupName { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public Nullable<int> Priority { get; set; }
        
        public Guid? CityRegion2Id { get; set; }

        public virtual ICollection<Store> StoreValidRegionInfoes { get; set; }
        public virtual ICollection<Customer> CustomerInfos { get; set; }
        public virtual ICollection<CityRegion> CityRegion1 { get; set; }
        [ForeignKey("CityRegion2Id")]
        public virtual CityRegion CityRegion2 { get; set; }
    }
}
