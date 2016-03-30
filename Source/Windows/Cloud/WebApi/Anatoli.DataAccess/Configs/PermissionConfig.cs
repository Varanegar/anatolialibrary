using Anatoli.DataAccess.Models.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class PermissionConfig : EntityTypeConfiguration<Permission>
    {
        public PermissionConfig()
        {
        }
    }
}
