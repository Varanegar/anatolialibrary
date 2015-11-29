namespace Anatoli.DataAccess.Models
{    
	using System;
	using System.Collections.Generic;
    
	    public class StoreValidRegionInfo : BaseModel
    {
        //public Guid StoreValidRegionInfoId { get; set; }
        //public Guid StoreId { get; set; }
    
        public virtual CityRegion CityRegion { get; set; }
        public virtual Store Store { get; set; }
    }
}
