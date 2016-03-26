using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class DistCompanyCenterConfig : EntityTypeConfiguration<DistCompanyCenter>
    {
        public DistCompanyCenterConfig()
        {
            this.HasMany<DistCompanyCenter>(pg => pg.DistCompanyCenters)
                .WithOptional(pg => pg.Parent)
                .WillCascadeOnDelete(false);

            this.HasMany<Stock>(cr => cr.DistCenterStocks)
                .WithOptional(svr => svr.DistCompanyCenter)
                .WillCascadeOnDelete(false);

        }
    }
}
