using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.DataAccess.Configs
{
    public class IncompleteOrderConfig : EntityTypeConfiguration<IncompleteOrder>
    {
        public IncompleteOrderConfig()
        {
            this.HasMany<IncompleteOrderLineItem>(pp => pp.IncompleteOrderLineItems)
                .WithRequired(p => p.IncompleteOrder)
                .WillCascadeOnDelete(true);
        }
    }
}