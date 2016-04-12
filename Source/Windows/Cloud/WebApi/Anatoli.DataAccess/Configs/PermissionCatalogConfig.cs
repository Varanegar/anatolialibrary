using Anatoli.DataAccess.Models.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class PermissionCatalogConfig : EntityTypeConfiguration<PermissionCatalog>
    {
        public PermissionCatalogConfig()
        {
            HasOptional(o => o.PermissionCatalougeParent);

            HasMany(p => p.Permissions).WithMany(pp => pp.PermissionCatalogs)
                                       .Map(cs =>
                                       {
                                           cs.MapLeftKey("PermissionCatalogId");
                                           cs.MapRightKey("PermissionID");
                                           cs.ToTable("PermissionCatalogPermissions");
                                       });
        }
    }
}
