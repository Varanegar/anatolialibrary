using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class DeliveryPersonConfig : EntityTypeConfiguration<DeliveryPerson>
    {
        public DeliveryPersonConfig()
        {
            this.HasMany<StoreDeliveryPerson>(cr => cr.StoreDeliveryPersons)
                .WithRequired(svr => svr.DeliveryPerson)
                .WillCascadeOnDelete(false);
        }
    }
}