namespace AnatoliIdentity.WebApi.Migrations
{
    using AnatoliIdentity.WebApi.Infrastructure;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AnatoliIdentity.WebApi.Infrastructure.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AnatoliIdentity.WebApi.Infrastructure.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "anatoli",
                Email = "anatoli@varanegar.com",
                EmailConfirmed = true,
                FirstName = "P2P",
                LastName = "Anatoli",
                Level = 1,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user, "anatoli");

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "Admin"});
                roleManager.Create(new IdentityRole { Name = "User"});
            }

            var adminUser = manager.FindByName("anatoli");

            manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin", "Admin" });

            var user2 = new ApplicationUser()
            {
                UserName = "petropay",
                Email = "petropay@varanegar.com",
                EmailConfirmed = true,
                FirstName = "P2P",
                LastName = "petropay",
                Level = 1,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user, "petropay");

            var userInfo = manager.FindByName("petropay");

            manager.AddToRoles(adminUser.Id, new string[] { "User"});
        }
    }
}
