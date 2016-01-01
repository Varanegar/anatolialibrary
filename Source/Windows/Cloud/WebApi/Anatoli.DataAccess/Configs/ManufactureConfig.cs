using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class ManufactureConfig : EntityTypeConfiguration<Manufacture>
    {
        public ManufactureConfig()
        {
            this.HasMany<Product>(pg => pg.ProductManufactures)
                .WithOptional(pg => pg.Manufacture);
        }
    }
}
