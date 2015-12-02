using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.DataAccess.Models.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            this.HasRequired<Principal>(p => p.Principal);

            this.HasOptional<Role>(r => r.Role)
                .WithMany(u => u.Users);

            this.HasOptional<Group>(r => r.Group)
                .WithMany(u => u.Users);
        }
    }
}
