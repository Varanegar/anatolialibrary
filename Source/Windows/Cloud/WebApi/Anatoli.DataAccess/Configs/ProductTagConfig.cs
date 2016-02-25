using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class ProductTagConfig : EntityTypeConfiguration<ProductTag>
    {
        public ProductTagConfig()
        {
            this.HasMany<ProductTagValue>(pg => pg.ProductTagValues)
           .WithRequired(pg => pg.ProductTag)
           .WillCascadeOnDelete(false);
        }
    }
}