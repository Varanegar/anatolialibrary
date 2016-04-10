using Anatoli.DataAccess.Models.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class PrincipalPermissionConfig : EntityTypeConfiguration<PrincipalPermission>
    {
        public PrincipalPermissionConfig()
        {
            HasRequired(p => p.Principal)
                .WithMany(pp => pp.PrincipalPermissions)
                .WillCascadeOnDelete(false);

            HasRequired(p => p.Permission)
                .WithMany(pp => pp.PrincipalPermissions)
                .WillCascadeOnDelete(false);
        }
    }   
}
