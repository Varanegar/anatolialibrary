using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class CustomerShipAddressConfig : EntityTypeConfiguration<CustomerShipAddress>
    {
        public CustomerShipAddressConfig()
        {
            this.HasRequired<Customer>(csa => csa.Customer)
                .WithMany(c => c.CustomerShipAddresses)
                .WillCascadeOnDelete(true);

            this.HasMany<PurchaseOrder>(pc => pc.PurchaseOrders)
                .WithOptional(p => p.CustomerShipAddress)
                .WillCascadeOnDelete(false);

            this.HasMany<IncompletePurchaseOrder>(pc => pc.IncompletePurchaseOrders)
                .WithOptional(p => p.CustomerShipAddress)
                .WillCascadeOnDelete(false);
        }
    }
}