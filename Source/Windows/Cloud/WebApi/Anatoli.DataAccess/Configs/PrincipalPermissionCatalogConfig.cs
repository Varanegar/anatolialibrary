
using Anatoli.DataAccess.Models.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class PrincipalPermissionCatalogConfig : EntityTypeConfiguration<PrincipalPermissionCatalog>
    {
        public PrincipalPermissionCatalogConfig()
        {
            HasRequired(p => p.Principal)
                .WithMany(pp => pp.PrincipalPermissionCatalogs)
                .WillCascadeOnDelete(false);

            HasRequired(p => p.PermissionCatalog)
                .WithMany(pp => pp.PrincipalPermissionCatalogs)
                .WillCascadeOnDelete(false);
        }
    }
}
