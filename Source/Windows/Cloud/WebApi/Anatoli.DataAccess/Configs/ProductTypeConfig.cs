using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class ProductTypeConfig : EntityTypeConfiguration<ProductType>
    {
        public ProductTypeConfig()
        {
            this.HasMany<Product>(pp => pp.Products)
                .WithOptional(p => p.ProductType)
                .WillCascadeOnDelete(false);

            this.HasMany<StockProductRequest>(pp => pp.StockProductRequests)
                .WithRequired(p => p.ProductType)
                .WillCascadeOnDelete(false);

            this.HasMany<StockProductRequestRule>(pp => pp.StockProductRequestRules)
                .WithOptional(p => p.ProductType)
                .WillCascadeOnDelete(false);
        }
    }
}