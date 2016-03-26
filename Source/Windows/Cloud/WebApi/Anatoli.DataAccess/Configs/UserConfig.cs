using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Models.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            this.HasRequired<ApplicationOwner>(p => p.ApplicationOwner)
                .WithMany(u => u.Users);

            this.HasRequired<AnatoliContact>(r => r.AnatoliContact)
                .WithMany(u => u.Users);

            this.HasMany<Stock>(p => p.Stocks)
                .WithMany(s => s.Users);

            this.HasMany<Stock>(s => s.Stocks)
               .WithMany(c => c.Users)
               .Map(cs =>
               {
                   cs.MapLeftKey("UserId");
                   cs.MapRightKey("StockID");
                   cs.ToTable("UsersStocks");
               });
        }
    }
}
