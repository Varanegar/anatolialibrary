using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class BasketConfig : EntityTypeConfiguration<Basket>
    {
        public BasketConfig()
        {
            this.HasMany<BasketItem>(bi => bi.BasketItems)
                .WithRequired(c => c.Basket)
                .WillCascadeOnDelete(true);

            this.HasMany<BasketNote>(bn => bn.BasketNotes)
                .WithRequired(c => c.Basket);
        }
    }
}