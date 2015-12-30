using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class CityRegionConfig : EntityTypeConfiguration<CityRegion>
    {
        public CityRegionConfig()
        {
            this.HasMany<Customer>(pp => pp.CustomerInfos)
                .WithRequired(p => p.RegionInfo);

            this.HasMany<CityRegion>(pg => pg.CityRegion1)
                .WithOptional(pg => pg.CityRegion2);
        }
    }
}