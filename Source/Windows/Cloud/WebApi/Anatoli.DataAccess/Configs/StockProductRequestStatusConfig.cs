using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.DataAccess.Configs
{
    public class StockProductRequestStatusConfig : EntityTypeConfiguration<StockProductRequestStatus>
    {
        public StockProductRequestStatusConfig()
        {
            this.HasMany<StockProductRequest>(pp => pp.StockProductRequests)
                .WithRequired(p => p.StockProductRequestStatus)
                .WillCascadeOnDelete(false);

        }
    }
}