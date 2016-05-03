using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class BrandConfig : EntityTypeConfiguration<Brand>
    {
        public BrandConfig()
        {
            this.HasMany<Product>(pg => pg.ProductBrands)
                .WithOptional(pg => pg.Brand)
                .WillCascadeOnDelete(false);
        }
    }
}
