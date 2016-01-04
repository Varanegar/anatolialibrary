using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class ReorderCalcTypeConfig : EntityTypeConfiguration<ReorderCalcType>
    {
        public ReorderCalcTypeConfig()
        {
            this.HasMany<StockProduct>(pp => pp.StockProducts)
                .WithOptional(p => p.ReorderCalcType)
                .WillCascadeOnDelete(false);

            this.HasMany<StockProductRequest>(pp => pp.StockProductRequests)
                .WithRequired(p => p.ReorderCalcType);
        }
    }
}