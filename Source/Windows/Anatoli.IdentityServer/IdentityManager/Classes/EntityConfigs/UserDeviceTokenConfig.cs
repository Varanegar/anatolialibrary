using Anatoli.IdentityServer.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.IdentityServer.Classes
{
    public class UserDeviceTokenConfig : EntityTypeConfiguration<UserDeviceToken>
    {
        public UserDeviceTokenConfig()
        {
            HasRequired(u => u.Client);

            HasRequired(u => u.User).WithMany(dt => dt.UserDeviceTokens);
        }
    }
}