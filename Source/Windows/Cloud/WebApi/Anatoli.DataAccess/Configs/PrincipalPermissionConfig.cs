using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.DataAccess.Models.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class PrincipalPermissionConfig : EntityTypeConfiguration<PrincipalPermission>
    {
        public PrincipalPermissionConfig()
        {
            this.HasRequired<Principal>(p => p.Principal)
                .WithMany(pp => pp.PrincipalPermissions);

            this.HasRequired<Permission>(p => p.Permission)
                .WithMany(pp => pp.PrincipalPermissions);
        }
    }
}
