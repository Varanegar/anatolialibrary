using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class PurchaseOrderConfig : EntityTypeConfiguration<PurchaseOrder>
    {
        public PurchaseOrderConfig()
        {
            this.HasMany<PurchaseOrderPayment>(pop => pop.PurchaseOrderPayments)
                .WithRequired(po => po.PurchaseOrder)
                .WillCascadeOnDelete(true);

            this.HasMany<PurchaseOrderClearance>(poc => poc.PurchaseOrderClearances)
                .WithRequired(po => po.PurchaseOrder)
                .WillCascadeOnDelete(true);

            this.HasMany<PurchaseOrderStatusHistory>(poc => poc.PurchaseOrderHistories)
                .WithRequired(po => po.PurchaseOrder);

            this.HasMany<PurchaseOrderLineItem>(poc => poc.PurchaseOrderLineItems)
                .WithRequired(po => po.PurchaseOrder)
                .WillCascadeOnDelete(true);
        }
    }
}
