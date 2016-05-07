using Anatoli.IdentityServer.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.IdentityServer.Classes
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            //HasMany(u => u.UserDeviceTokens).WithRequired(u => u.User);

            HasMany(c => c.Channels).WithMany(u => u.Users).Map(cu =>
            {
                cu.MapLeftKey("UserId");
                cu.MapRightKey("ChannelId");
                cu.ToTable("UserChannels");
            });
        }
    }
}