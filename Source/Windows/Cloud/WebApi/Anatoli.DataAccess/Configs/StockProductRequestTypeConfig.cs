using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.DataAccess.Configs
{
    public class StockProductRequestTypeConfig : EntityTypeConfiguration<StockProductRequestType>
    {
        public StockProductRequestTypeConfig()
        {
            this.HasMany<StockProductRequest>(pp => pp.StockProductRequests)
                .WithRequired(p => p.StockPorductRequestType)
                .WillCascadeOnDelete(false);

        }
    }
}