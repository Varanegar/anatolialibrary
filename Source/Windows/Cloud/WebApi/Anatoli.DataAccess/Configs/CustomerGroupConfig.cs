using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class CustomerGroupConfig : EntityTypeConfiguration<CustomerGroup>
    {
        public CustomerGroupConfig()
        {
            this.HasMany<Customer>(csa => csa.Customers)
                .WithOptional(c => c.CustomerGroup)
                .WillCascadeOnDelete(false);
        }
    }
}