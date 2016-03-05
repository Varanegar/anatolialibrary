using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class DistCompanyRegionLevelTypeConfig : EntityTypeConfiguration<DistCompanyRegionLevelType>
    {
        public DistCompanyRegionLevelTypeConfig()
        {
            this.HasMany<DistCompanyRegion>(pp => pp.DistCompanyRegions)
                .WithRequired(p => p.DistCompanyRegionLevelType)
                .WillCascadeOnDelete(false);

        }
    }
}