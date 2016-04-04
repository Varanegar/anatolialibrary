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

            this.HasOptional<AnatoliContact>(r => r.AnatoliContact)
                .WithMany(u => u.Users);

        }
    }
}
