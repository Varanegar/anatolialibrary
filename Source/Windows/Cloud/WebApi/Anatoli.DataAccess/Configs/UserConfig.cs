using Anatoli.DataAccess.Models.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            HasRequired(p => p.ApplicationOwner).WithMany(u => u.Users);

            HasOptional(r => r.AnatoliContact).WithMany(u => u.Users);
        }
    }
}
