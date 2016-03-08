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
            this.HasMany<StockProductRequestRule>(pp => pp.StockProductRequestRules)
                .WithRequired(p => p.ReorderCalcType)
                .WillCascadeOnDelete(false);
        }
    }
}