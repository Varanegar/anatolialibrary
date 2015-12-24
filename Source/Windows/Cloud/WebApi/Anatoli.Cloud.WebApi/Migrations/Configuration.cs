namespace Anatoli.Cloud.WebApi.Migrations
{
    using Anatoli.Cloud.WebApi.Infrastructure;
    using Anatoli.DataAccess;
    using Anatoli.DataAccess.Models.Identity;
    using Anatoli.DataAccess.Repositories;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AnatoliDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AnatoliDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //var manager = new ApplicationUserManager(new AnatoliUserStore(context));

            //var roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context));

            //var id = Guid.NewGuid();
            //var user = new User()
            //{
            //    Id = id.ToString(),
            //    UserName = "anatoli",
            //    Email = "anatoli@varanegar.com",
            //    EmailConfirmed = true,
            //    CreatedDate = DateTime.Now,
            //    PhoneNumber = "87135000",
            //    PrivateLabelOwner = new Principal { Id = Guid.Parse("CB11335F-6D14-49C9-9798-AD61D02EDBE1") },
            //    Principal = new Principal { Id = id, Title = "anatoli@varanegar.com" }
            //};


            //manager.Create(user, "anatoli@vn@87134");

            //if (roleManager.Roles.Count() == 0)
            //{
            //    roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "AuthorizedApp" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByName("anatoli");

            //manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin", "Admin", "AuthorizedApp", "User" });

            //var user2 = new User()
            //{
            //    UserName = "petropay",
            //    Email = "petropay@varanegar.com",
            //    EmailConfirmed = true,
            //    PhoneNumber = "02100000000",
            //    CreatedDate = DateTime.Now,
            //    PrivateLabelOwner = new Principal { Id = Guid.Parse("CB11335F-6D14-49C9-9798-AD61D02EDBE1") },
            //    Principal = new Principal { Id = id, Title = "petropay@varanegar.com" }
            //};

            //manager.Create(user2, "petropay@webapp");

            //var userInfo = manager.FindByName("petropay");

            //manager.AddToRoles(adminUser.Id, new string[] { "AuthorizedApp", "User" });
        }
    }
}
