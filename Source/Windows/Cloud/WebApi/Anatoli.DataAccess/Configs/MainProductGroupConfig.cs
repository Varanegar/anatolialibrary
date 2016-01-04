using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class MainProductGroupConfig : EntityTypeConfiguration<MainProductGroup>
    {
        public MainProductGroupConfig()
        {
            this.HasMany<MainProductGroup>(pg => pg.ProductGroup1)
                .WithOptional(pg => pg.ProductGroup2);

            this.HasMany<Product>(pg => pg.Products)
                .WithOptional(pg => pg.MainProductGroup)
                .WillCascadeOnDelete(false);

            this.HasMany<StockProductRequestRule>(pg => pg.StockProductRequestRules)
                .WithOptional(pg => pg.MainProductGroup)
                .WillCascadeOnDelete(false);


        }
    }
}
