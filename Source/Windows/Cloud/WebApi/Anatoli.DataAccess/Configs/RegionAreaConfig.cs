using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Anatoli.DataAccess.Models.Route;

namespace Anatoli.DataAccess.Configs
{
    public class RegionAreaConfig : EntityTypeConfiguration<RegionArea>
    {
        public RegionAreaConfig()
        {
            this.HasMany<RegionArea>(pg => pg.RegionAreaChilds)
                .WithOptional(pg => pg.RegionAreaParent)
                .WillCascadeOnDelete(false);

            this.HasMany<RegionAreaPoint>(pg => pg.RegionAreaPoints)
                .WithRequired(pg => pg.RegionArea)
                .WillCascadeOnDelete(true);

        }
    }
}
