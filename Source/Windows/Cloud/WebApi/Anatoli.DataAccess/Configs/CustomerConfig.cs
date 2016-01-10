using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class CustomerConfig : EntityTypeConfiguration<Customer>
    {
        public CustomerConfig()
        {
            this.HasMany<CustomerShipAddress>(csa => csa.CustomerShipAddresses)
                .WithRequired(c => c.Customer)
                .WillCascadeOnDelete(true);

            this.HasMany<Basket>(csa => csa.CustomerBaskets)
                .WithRequired(c => c.Customer)
                .WillCascadeOnDelete(true);

            this.HasMany<IncompletePurchaseOrder>(csa => csa.IncompletePurchaseOrders)
                .WithRequired(c => c.Customer)
                .WillCascadeOnDelete(false);
        }
    }
}