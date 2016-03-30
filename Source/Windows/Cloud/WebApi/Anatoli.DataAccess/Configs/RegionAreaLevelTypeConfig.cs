using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Anatoli.DataAccess.Models.Route;

namespace Anatoli.DataAccess.Configs
{
    public class RegionAreaLevelTypeConfig : EntityTypeConfiguration<RegionAreaLevelType>
    {
        public RegionAreaLevelTypeConfig()
        {
            this.HasMany<RegionArea>(pp => pp.RegionAreas)
                .WithRequired(p => p.RegionAreaLevelType)
                .WillCascadeOnDelete(false);

        }
    }
}