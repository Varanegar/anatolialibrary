using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.DataAccess.Configs
{
    public class StockConfig : EntityTypeConfiguration<Stock>
    {
        public StockConfig()
        {
            this.HasMany<Stock>(pg => pg.MainSCMStock1)
                 .WithOptional(pg => pg.MainSCMStock2)
                 .WillCascadeOnDelete(false);

            this.HasMany<Stock>(pg => pg.RelatedSCMStock1)
                 .WithOptional(pg => pg.RelatedSCMStock2)
                 .WillCascadeOnDelete(false);

            this.HasMany<StockProduct>(pp => pp.StockProducts)
                .WithRequired(p => p.Stock)
                .WillCascadeOnDelete(false);

            this.HasMany<StockOnHandSync>(pp => pp.StockOnHandSyncs)
                .WithRequired(p => p.Stock)
                .WillCascadeOnDelete(false);

            this.HasMany<StockProductRequest>(pp => pp.StockProductRequests)
                .WithRequired(p => p.Stock)
                .WillCascadeOnDelete(false);

            this.HasMany<StockProductRequest>(pp => pp.StockProductRequestSourceStocks)
                .WithOptional(p => p.SupplyByStock)
                .WillCascadeOnDelete(false);

            this.HasOptional<User>(p => p.Accept1By);
            this.HasOptional<User>(p => p.Accept2By);
            this.HasOptional<User>(p => p.Accept3By);
        }
    }
}