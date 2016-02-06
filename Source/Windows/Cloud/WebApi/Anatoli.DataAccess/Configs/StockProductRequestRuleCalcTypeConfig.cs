using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.DataAccess.Configs
{
    public class StockProductRequestRuleCalcTypeConfig : EntityTypeConfiguration<StockProductRequestRuleCalcType>
    {
        public StockProductRequestRuleCalcTypeConfig()
        {
            this.HasMany<StockProductRequestRule>(pp => pp.StockProductRequestRules)
                .WithRequired(p => p.StockProductRequestRuleCalcType)
                .WillCascadeOnDelete(false);

        }
    }
}