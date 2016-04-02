using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class CompanyCenterConfig : EntityTypeConfiguration<CompanyCenter>
    {
        public CompanyCenterConfig()
        {
            this.HasMany<CompanyCenter>(pg => pg.CompanyCenters)
                .WithOptional(pg => pg.Parent)
                .WillCascadeOnDelete(false);

            this.HasMany<Stock>(cr => cr.DistCenterStocks)
                .WithOptional(svr => svr.CompanyCenter)
                .WillCascadeOnDelete(false);

        }
    }
}
