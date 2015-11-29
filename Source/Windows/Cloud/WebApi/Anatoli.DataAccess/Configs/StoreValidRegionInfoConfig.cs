using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class StoreValidRegionInfoConfig : EntityTypeConfiguration<StoreValidRegionInfo>
    {
        public StoreValidRegionInfoConfig()
        {
            this.HasRequired<CityRegion>(cr => cr.CityRegion)
                .WithMany(svr => svr.StoreValidRegionInfoes);
        }
    }
}