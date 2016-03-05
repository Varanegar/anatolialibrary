using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class DistCompanyRegionConfig : EntityTypeConfiguration<DistCompanyRegion>
    {
        public DistCompanyRegionConfig()
        {
            this.HasMany<DistCompanyRegion>(pg => pg.DistCompanyRegions)
                .WithOptional(pg => pg.Parent)
                .WillCascadeOnDelete(false);

            this.HasMany<DistCompanyRegionPolygon>(pg => pg.DistCompanyRegionPolygons)
                .WithRequired(pg => pg.DistCompanyRegion)
                .WillCascadeOnDelete(true);

        }
    }
}
