using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class StockTypeConfig : EntityTypeConfiguration<StockType>
    {
        public StockTypeConfig()
        {
            this.HasMany<Stock>(pp => pp.Products)
                .WithOptional(p => p.StockType)
                .WillCascadeOnDelete(false);

            this.HasMany<StockProductRequest>(pp => pp.StockProductRequests)
                .WithRequired(p => p.StockType)
                .WillCascadeOnDelete(false);
        }
    }
}