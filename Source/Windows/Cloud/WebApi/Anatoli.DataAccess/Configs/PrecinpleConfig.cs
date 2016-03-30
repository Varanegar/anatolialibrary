using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Models.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class PrincipalConfig : EntityTypeConfiguration<Principal>
    {
        public PrincipalConfig()
        {
            this.HasOptional<User>(p => p.User)
                .WithRequired(s => s.Principal);

            this.HasOptional<Group>(p => p.Group)
                .WithRequired(s => s.Principal);

        }
    }
}
