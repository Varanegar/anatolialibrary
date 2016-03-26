using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.DataAccess.Models.Identity;
using System.Data.Entity.ModelConfiguration;
using Anatoli.DataAccess.Models;

namespace Anatoli.DataAccess.Configs
{
    public class PrincipalPermissionConfig : EntityTypeConfiguration<PrincipalPermission>
    {
        public PrincipalPermissionConfig()
        {
            this.HasRequired<User>(p => p.User)
                .WithMany(pp => pp.PrincipalPermissions)
                .WillCascadeOnDelete(false);

            this.HasRequired<Permission>(p => p.Permission)
                .WithMany(pp => pp.PrincipalPermissions)
                .WillCascadeOnDelete(false);
        }
    }    
}
