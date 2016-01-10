using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.DataAccess.Configs
{
    public class StockProductRequestTypeSupplyConfig : EntityTypeConfiguration<StockProductRequestSupplyType>
    {
        public StockProductRequestTypeSupplyConfig()
        {
            this.HasMany<StockProductRequest>(pp => pp.StockProductRequests)
                .WithRequired(p => p.StockProductRequestSupplyType)
                .WillCascadeOnDelete(false);

            this.HasMany<StockProduct>(pp => pp.StockProducts)
                .WithOptional(p => p.StockProductRequestSupplyType)
                .WillCascadeOnDelete(false);

        }
    }
}