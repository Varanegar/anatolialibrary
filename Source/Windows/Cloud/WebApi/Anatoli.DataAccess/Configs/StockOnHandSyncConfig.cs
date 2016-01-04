using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class StockOnHandSyncConfig : EntityTypeConfiguration<StockOnHandSync>
    {
        public StockOnHandSyncConfig()
        {
            this.HasMany<StockActiveOnHand>(pp => pp.StockActiveOnHands)
                .WithRequired(p => p.StockOnHandSync);
            this.HasMany<StockHistoryOnHand>(pp => pp.StockHistoryOnHands)
                .WithRequired(p => p.StockOnHandSync);
            this.HasMany<StockProductRequest>(pp => pp.StockProductRequests)
                .WithRequired(p => p.StockOnHandSync);
        }
    }
}