using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.DataAccess.Configs
{
    public class StockProductRequestRuleConfig : EntityTypeConfiguration<StockProductRequestRule>
    {
        public StockProductRequestRuleConfig()
        {

            this.HasMany<StockProductRequestProductDetail>(pp => pp.StockProductRequestProductDetails)
                .WithRequired(p => p.StockProductRequestRule)
                .WillCascadeOnDelete(false);
        }
    }
}