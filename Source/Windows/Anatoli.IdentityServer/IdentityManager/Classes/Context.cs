using System.Data.Entity;
using Anatoli.IdentityServer.Entities;
using IdentityServer3.EntityFramework;
using IdentityServer3.EntityFramework.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Anatoli.IdentityServer.Classes
{
    public class Context : IdentityDbContext<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>,
                           IClientConfigurationDbContext, IScopeConfigurationDbContext
    {
        #region Properties
        public DbSet<Client> Clients { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<UserDeviceToken> UserDeviceToken { get; set; }

        #endregion

        #region ctors
        public Context() : this("IdSvr3Config")
        {
        }
        public Context(string connString) : base(connString) { }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfig());

            modelBuilder.Configurations.Add(new UserDeviceTokenConfig());

            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);

            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}