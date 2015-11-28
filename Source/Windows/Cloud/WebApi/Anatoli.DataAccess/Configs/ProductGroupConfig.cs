using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class ProductGroupConfig : EntityTypeConfiguration<ProductGroup>
    {
        public ProductGroupConfig()
        {
            this.HasMany<ProductGroup>(pg => pg.ProductGroup1)
                .WithOptional(pg => pg.ProductGroup2);
        }
    }
}
