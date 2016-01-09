using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.DataAccess.Configs
{
    public class IncompletePurchaseOrderConfig : EntityTypeConfiguration<IncompletePurchaseOrder>
    {
        public IncompletePurchaseOrderConfig()
        {
            this.HasMany<IncompletePurchaseOrderLineItem>(pp => pp.IncompletePurchaseOrderLineItems)
                .WithRequired(p => p.IncompletePurchaseOrder)
                .WillCascadeOnDelete(true);
        }
    }
}