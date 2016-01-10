using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class SupplierConfig : EntityTypeConfiguration<Supplier>
    {
        public SupplierConfig()
        {
            this.HasMany<StockProductRequestRule>(pp => pp.StockProductRequestRules)
                .WithOptional(p => p.Supplier)
                .WillCascadeOnDelete(false);
        }
    }
}