using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.DataAccess.Configs
{
    public class StockProductRequestConfig : EntityTypeConfiguration<StockProductRequest>
    {
        public StockProductRequestConfig()
        {
            this.HasRequired<Principal>(p => p.Accept1By);
            this.HasOptional<Principal>(p => p.Accept2By);
            this.HasOptional<Principal>(p => p.Accept3By);
            
            this.HasMany<StockProductRequestProduct>(pp => pp.StockProductRequestProducts)
                .WithRequired(p => p.StockProductRequest)
                .WillCascadeOnDelete(true);
        }
    }
}