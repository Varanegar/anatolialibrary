namespace Anatoli.DataAccess.Migrations
{
    using Anatoli.DataAccess.Models;
    using Anatoli.DataAccess.Models.Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Anatoli.DataAccess.AnatoliDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Anatoli.DataAccess.AnatoliDbContext context)
        {
            context.Principals.AddOrUpdate(item => item.Id,
                new Principal { Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), Title = "petropay@varanegar.com" },
                new Principal { Id = Guid.Parse("0DAB1636-AE22-4ABE-A18D-6EC7B8E9C544"), Title = "anatoli-mobile-app@varanegar.com" },
                new Principal { Id = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16"), Title = "anatoli@varanegar.com" },
                new Principal { Id = Guid.Parse("95FCB850-2E63-4B26-8DBF-BBC86B7F5046"), Title = "anatoli-inter-com@varanegar.com" },
                new Principal { Id = Guid.Parse("33FA710A-B1E6-4765-8719-0DD1589E8F8B"), Title = "anatoli-scm@varanegar.com" }
                );

            context.Roles.AddOrUpdate(item => item.Id,
                new IdentityRole { Id = "a246c692-bcd4-441f-b4b3-fec237603417", Name = "User" },
                new IdentityRole { Id = "9d7a33b0-8f24-4b22-93f2-055b10b94a2c", Name = "AuthorizedApp" },
                new IdentityRole { Id = "edebbfe2-6eb8-4c7b-945c-480d130305df", Name = "SuperAdmin" },
                new IdentityRole { Id = "969cf96e-e231-4991-a913-87f9c1e0bb81", Name = "Admin" },
                new IdentityRole { Id = "95B93EF1-9F67-4EB5-AD3E-569C64DAF4E3", Name = "SCM" },
                new IdentityRole { Id = "AE4AF236-E229-45A8-B1C0-CBE6CB104721", Name = "InternalCommunication" }
                );
            /*
            #region User Info
            Principal privateOwner = context.Principals.FirstOrDefault(p => p.Title == "petropay@varanegar.com");

            var hasher = new PasswordHasher();
            string userId = "0dab1636-ae22-4abe-a18d-6ec7b8e9c544";
            string userEmail = "anatoli-mobile-app@varanegar.com";
            Principal principal = context.Principals.FirstOrDefault(p => p.Title == userEmail);
            context.Users.AddOrUpdate(item => item.Id,
                new User { Id = userId, PhoneNumber = "09125793221", UserName = "AnatoliMobileApp", PasswordHash = hasher.HashPassword("Anatoli@App@Vn"), Email = userEmail, Principal = principal, EmailConfirmed = true, PhoneNumberConfirmed = true, CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, LastEntry = DateTime.Now, PrivateLabelOwner = privateOwner, SecurityStamp = Guid.NewGuid().ToString() });

            userId = "95FCB850-2E63-4B26-8DBF-BBC86B7F5046";
            userEmail = "anatoli-inter-com@varanegar.com";
            principal = context.Principals.FirstOrDefault(p => p.Title == userEmail);
            context.Users.AddOrUpdate(item => item.Id,
                new User { Id = userId, PhoneNumber = "87135001", UserName = "anatoli-inter-com@varanegar.com", PasswordHash = hasher.HashPassword("anatoli@vn@87134"), Email = userEmail, Principal = principal, EmailConfirmed = true, PhoneNumberConfirmed = true, CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, LastEntry = DateTime.Now, PrivateLabelOwner = privateOwner, SecurityStamp = Guid.NewGuid().ToString() });

            userId = "33FA710A-B1E6-4765-8719-0DD1589E8F8B";
            userEmail = "anatoli-scm@varanegar.com";
            principal = context.Principals.FirstOrDefault(p => p.Title == userEmail);
            context.Users.AddOrUpdate(item => item.Id,
                new User { Id = userId, PhoneNumber = "87135002", UserName = "anatoli-scm@varanegar.com", PasswordHash = hasher.HashPassword("anatoli@vn@87134"), Email = userEmail, Principal = principal, EmailConfirmed = true, PhoneNumberConfirmed = true, CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, LastEntry = DateTime.Now, PrivateLabelOwner = privateOwner, SecurityStamp = Guid.NewGuid().ToString() });

            userId = "02D3C1AA-6149-4810-9F83-DF3928BFDF16";
            userEmail = "anatoli@varanegar.com";
            principal = context.Principals.FirstOrDefault(p => p.Title == userEmail);
            context.Users.AddOrUpdate(item => item.Id,
                new User { Id = userId, PhoneNumber = "87135000", UserName = "anatoli", PasswordHash = hasher.HashPassword("anatoli@vn@87134"), Email = userEmail, Principal = principal, EmailConfirmed = true, PhoneNumberConfirmed = true, CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, LastEntry = DateTime.Now, PrivateLabelOwner = privateOwner, SecurityStamp = Guid.NewGuid().ToString() });


            userId = "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
            userEmail = "petropay@varanegar.com";
            principal = context.Principals.FirstOrDefault(p => p.Title == userEmail);
            context.Users.AddOrUpdate(item => item.Id,
                new User { Id = userId, PhoneNumber = "02100000000", UserName = "petropay", PasswordHash = hasher.HashPassword("petropay@webapp"), Email = userEmail, Principal = principal, EmailConfirmed = true, PhoneNumberConfirmed = true, CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, LastEntry = DateTime.Now, PrivateLabelOwner = privateOwner, SecurityStamp = Guid.NewGuid().ToString() });
            #endregion
            */

            context.ProductType.AddOrUpdate(item => item.Id,
                new ProductType { Id = Guid.Parse("72E59112-6054-4140-8E33-947228616393"), ProductTypeName = "کالای زیر صفر", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new ProductType { Id = Guid.Parse("6FC2FD34-4CBC-4EB1-BD7E-1BD751E4F2A2"), ProductTypeName = "کالای یخچالی", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new ProductType { Id = Guid.Parse("8E85E5B0-9242-47A1-99A1-7B90566C36D4"), ProductTypeName = "انباری ساده", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.StockTypes.AddOrUpdate(item => item.Id,
                new StockType { Id = Guid.Parse("4C8C4883-6938-4D4A-BA35-08FB6D13203C"), StockTypeName = "انبار فروشگاه", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockType { Id = Guid.Parse("B9DC89F4-536F-45DB-AD84-2E9D3A7A6B55"), StockTypeName = "انبار تامین", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.StockProductRequestTypes.AddOrUpdate(item => item.Id,
                new StockProductRequestType { Id = Guid.Parse("1462A36B-9AB0-41AB-88F1-AAD152A7E425"), StockPorductRequestTypeName = "محاسبه دوره ای", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestType { Id = Guid.Parse("252B2E6A-AC0E-49B7-9BE7-F559A2BAC847"), StockPorductRequestTypeName = "محاسبه دستی", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestType { Id = Guid.Parse("28BC2DEE-3839-48E7-B50B-D8B8D0CCA191"), StockPorductRequestTypeName = "محاسبه میان روز اضطراری", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestType { Id = Guid.Parse("47CF63BD-C297-441F-810E-0D685333CC39"), StockPorductRequestTypeName = "محاسبه آزمایشی", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.ReorderCalcTypes.AddOrUpdate(item => item.Id,
                new ReorderCalcType { Id = Guid.Parse("7ECB9525-EA5F-487E-BBB6-971B9B22D7FF"), ReorderTypeName = "کالا به تنهایی", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new ReorderCalcType { Id = Guid.Parse("3341AFFC-6885-415E-8BEB-1EE0AA5B6405"), ReorderTypeName = "کالاهای گروه کالا", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new ReorderCalcType { Id = Guid.Parse("780C2900-ED36-4D73-A898-54CDC326B5E7"), ReorderTypeName = "کالاهای تامین کننده", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.StockProductRequestStatuses.AddOrUpdate(item => item.Id,
                new StockProductRequestStatus { Id = Guid.Parse("88B91155-5E56-4C48-BE2B-416C7EDA1713"), StockProductRequestStatusName = "منتظر تایید مسوول فروشگاه", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestStatus { Id = Guid.Parse("770803A2-3F46-48D1-98D2-2D656F6297DD"), StockProductRequestStatusName = "منتظر تایید سرپرست", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestStatus { Id = Guid.Parse("C692FBF0-B133-4F01-B154-8441D62F65CF"), StockProductRequestStatusName = "منتظر تایید تامین مرکزی", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestStatus { Id = Guid.Parse("E0012511-EC7B-498C-8EE0-ECBF6A7EC63B"), StockProductRequestStatusName = "منتظر ارسال", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestStatus { Id = Guid.Parse("9AE7E7BB-896E-4EFB-9279-D0EEFB830B3F"), StockProductRequestStatusName = "تحویل شده", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

        }
    }
}
