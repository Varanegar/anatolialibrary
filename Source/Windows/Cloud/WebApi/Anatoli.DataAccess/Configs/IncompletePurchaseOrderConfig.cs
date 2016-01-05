using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class IncompletePurchaseOrderConfig : EntityTypeConfiguration<IncompletePurchaseOrder>
    {
        public IncompletePurchaseOrderConfig()
        {
            this.HasMany<IncompletePurchaseOrderLineItem>(pp => pp.IncompletePurchaseOrderLineItems)
                .WithRequired(p => p.IncompletePurchaseOrder).WillCascadeOnDelete(false);
        }
    }
}