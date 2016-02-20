namespace Anatoli.DataAccess.Migrations
{
    using Anatoli.DataAccess.Models.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDataInsert4 : DbMigration
    {
        public override void Up()
        {
            AnatoliDbContext context = new AnatoliDbContext();

            #region User Info

            var userId = "02D3C1AA-6149-4810-9F83-DF3928BFDF16";
            var userEmail = "anatoli@varanegar.com";
            var principal = Guid.Parse(userId);
            var privateOwnerId = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");
            context.Users.AddOrUpdate(item => item.Id,
                new User
                {
                    Id = userId,
                    PhoneNumber = "87135000",
                    UserName = "anatoli",
                    PasswordHash = "AJ1iTXc0/EgQyLWeHZFh4xrX6vpu37VCmAfcTNm1bUBU+zcc2dqnTKuXyeZmhfC4+A==",
                    Email = userEmail,
                    Principal_Id = principal,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    LastEntry = DateTime.Now,
                    PrivateLabelOwner_Id = privateOwnerId,
                    SecurityStamp = "28a22f1c-83d8-4aee-8f9e-02441f25092c",
                });

            userId = "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
            userEmail = "petropay@varanegar.com";
            principal = Guid.Parse(userId);
            context.Users.AddOrUpdate(item => item.Id,
                new User
                {
                    Id = userId,
                    PhoneNumber = "02100000000",
                    UserName = "petropay",
                    PasswordHash = "AOw6dMvdSydP0geii72BK6vtgL+omhMNHlMhNMUoGgH4eF7hlmVdCF7E9v1c+uahCA==",
                    Email = userEmail,
                    Principal_Id = principal,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    LastEntry = DateTime.Now,
                    PrivateLabelOwner_Id = privateOwnerId,
                    SecurityStamp = "81434395-72c2-4def-8c09-853b08f233d9"
                });

            userId = "0dab1636-ae22-4abe-a18d-6ec7b8e9c544";
            userEmail = "anatoli-mobile-app@varanegar.com";
            principal = Guid.Parse(userId);
            context.Users.AddOrUpdate(item => item.Id,
                new User
                {
                    Id = userId,
                    PhoneNumber = "09125793221",
                    UserName = "AnatoliMobileApp",
                    PasswordHash = "AA7XiPMTyUfecJ0H6MYalhVvkX7JnNaNXt+OCy8bQYm5tkvzPfZFVFDIoLbwYWzQsA==",
                    Email = userEmail,
                    Principal_Id = principal,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    LastEntry = DateTime.Now,
                    PrivateLabelOwner_Id = privateOwnerId,
                    SecurityStamp = "4e3b2471-3700-405b-be71-53be82205fa5"
                });

            userId = "33FA710A-B1E6-4765-8719-0DD1589E8F8B";
            userEmail = "anatoli-scm@varanegar.com";
            principal = Guid.Parse(userId);
            context.Users.AddOrUpdate(item => item.Id,
                new User { Id = userId, PhoneNumber = "87135002", UserName = "anatoli-scm@varanegar.com", PasswordHash = "AKuQkFIs2ujBP4LGnMb06KWGWbXnBNeV5lMosIl0WUJs7RR/9bUrYg1qMBdg/IRaWA==", Email = userEmail, Principal_Id = principal, EmailConfirmed = true, PhoneNumberConfirmed = true, CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, LastEntry = DateTime.Now, PrivateLabelOwner_Id = privateOwnerId, SecurityStamp = "90b28ba5-b8f3-4a50-879b-45f672265727" });

            userId = "95FCB850-2E63-4B26-8DBF-BBC86B7F5046";
            userEmail = "anatoli-inter-com@varanegar.com";
            principal = Guid.Parse(userId);
            context.Users.AddOrUpdate(item => item.Id,
                new User
                {
                    Id = userId,
                    PhoneNumber = "87135001",
                    UserName = "anatoli-inter-com@varanegar.com",
                    PasswordHash = "AIuFru38JAxZIPyGo1TjRbIyO0+nhw34gCC8eHydps0LTblAQLZAnasL6CAa62wbwQ==",
                    Email = userEmail,
                    Principal_Id = principal,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    LastEntry = DateTime.Now,
                    PrivateLabelOwner_Id = privateOwnerId,
                    SecurityStamp = "1816af0b-15a9-4b18-8e48-5853f5317d0d"
                });

            
            #endregion
            context.SaveChanges();

        }
        
        public override void Down()
        {
        }
    }
}
