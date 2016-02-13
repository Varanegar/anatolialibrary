namespace Anatoli.DataAccess.Migrations
{
    using Anatoli.DataAccess.Models;
    using Anatoli.DataAccess.Models.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialDataInsert : DbMigration
    {
        public override void Up()
        {
            AnatoliDbContext context = new AnatoliDbContext();
            context.Principals.AddOrUpdate(item => item.Id,
                new Principal { Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), Title = "petropay@varanegar.com" },
                new Principal { Id = Guid.Parse("0DAB1636-AE22-4ABE-A18D-6EC7B8E9C544"), Title = "anatoli-mobile-app@varanegar.com" },
                new Principal { Id = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16"), Title = "anatoli@varanegar.com" },
                new Principal { Id = Guid.Parse("95FCB850-2E63-4B26-8DBF-BBC86B7F5046"), Title = "anatoli-inter-com@varanegar.com" },
                new Principal { Id = Guid.Parse("33FA710A-B1E6-4765-8719-0DD1589E8F8B"), Title = "anatoli-scm@varanegar.com" }
                );


            context.Roles.AddOrUpdate(item => item.Id,
                new IdentityRole { Id = "4447853b-e19f-42ce-bb29-f5aa1943b542", Name = "User" },
                new IdentityRole { Id = "4d10bd96-7f25-477a-a544-75e54b619a1f", Name = "AuthorizedApp" },
                new IdentityRole { Id = "507b6966-17f1-4116-a497-02242c052961", Name = "SuperAdmin" },
                new IdentityRole { Id = "5a61344b-b1b5-4157-8861-7bed15c0bdc2", Name = "Admin" },
                new IdentityRole { Id = "95B93EF1-9F67-4EB5-AD3E-569C64DAF4E3", Name = "SCM" },
                new IdentityRole { Id = "AE4AF236-E229-45A8-B1C0-CBE6CB104721", Name = "InternalCommunication" }
                );

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
                new StockProductRequestType { Id = Guid.Parse("1462A36B-9AB0-41AB-88F1-AAD152A7E425"), StockProductRequestTypeName = "محاسبه دوره ای", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestType { Id = Guid.Parse("252B2E6A-AC0E-49B7-9BE7-F559A2BAC847"), StockProductRequestTypeName = "محاسبه دستی", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestType { Id = Guid.Parse("28BC2DEE-3839-48E7-B50B-D8B8D0CCA191"), StockProductRequestTypeName = "محاسبه میان روز اضطراری", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestType { Id = Guid.Parse("47CF63BD-C297-441F-810E-0D685333CC39"), StockProductRequestTypeName = "محاسبه آزمایشی", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.ReorderCalcTypes.AddOrUpdate(item => item.Id,
                new ReorderCalcType { Id = Guid.Parse("7ECB9525-EA5F-487E-BBB6-971B9B22D7FF"), ReorderTypeName = "کالا به تنهایی", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new ReorderCalcType { Id = Guid.Parse("3341AFFC-6885-415E-8BEB-1EE0AA5B6405"), ReorderTypeName = "کالاهای گروه کالا", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new ReorderCalcType { Id = Guid.Parse("780C2900-ED36-4D73-A898-54CDC326B5E7"), ReorderTypeName = "کالاهای تامین کننده", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.StockTypes.AddOrUpdate(item => item.Id,
                new StockType { Id = Guid.Parse("8a911bf9-dee1-449b-b407-a04a93e976c3"), StockTypeName = "انبار فروشگاه", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockType { Id = Guid.Parse("6ace8810-1261-4c47-98a0-f414c4d6f79a"), StockTypeName = "انبار اصلی تامین", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockType { Id = Guid.Parse("fe362433-401d-4b1f-b1fc-b75f0853bd44"), StockTypeName = "انبار شعبه تامین", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockType { Id = Guid.Parse("92a9b97a-0e51-4ed3-b012-f1bde2e62f9b"), StockTypeName = "انبار حوزه", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.StockProductRequestRuleTypes.AddOrUpdate(item => item.Id,
                new StockProductRequestRuleType { Id = Guid.Parse("9f1f9ce4-4d5d-4885-9458-16eb24bf1b59"), StockProductRequestRuleTypeName = "بر اساس نقطه سفارش بدون کالاهای وابسته", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestRuleType { Id = Guid.Parse("9a82849b-178f-4da1-85e7-700fee41a58a"), StockProductRequestRuleTypeName = "بر اساس نقطه سفارش با کالاهای وابسته", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestRuleType { Id = Guid.Parse("4a840ae4-2bd5-406c-9dca-8e582b41f8de"), StockProductRequestRuleTypeName = " افزودن به درخواست بدون کالاهای وابسته", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.StockProductRequestSupplyTypes.AddOrUpdate(item => item.Id,
                new StockProductRequestSupplyType { Id = Guid.Parse("454422E2-8751-4F28-A31D-0C3E8230FA81"), StockProductRequestSupplyTypeName = "تامین از تامین کننده", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestSupplyType { Id = Guid.Parse("C6968D05-CFFD-4269-8129-E0F99E63B656"), StockProductRequestSupplyTypeName = "تامین از انبار تامین مرکزی", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestSupplyType { Id = Guid.Parse("AB787E71-2120-45D7-9B69-021E60FEA0BB"), StockProductRequestSupplyTypeName = "تامین از انبار تامین مرتبط", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.StockProductRequestStatuses.AddOrUpdate(item => item.Id,
                new StockProductRequestStatus { Id = Guid.Parse("88B91155-5E56-4C48-BE2B-416C7EDA1713"), StockProductRequestStatusName = "منتظر تایید مسوول فروشگاه", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestStatus { Id = Guid.Parse("770803A2-3F46-48D1-98D2-2D656F6297DD"), StockProductRequestStatusName = "منتظر تایید سرپرست", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestStatus { Id = Guid.Parse("C692FBF0-B133-4F01-B154-8441D62F65CF"), StockProductRequestStatusName = "منتظر تایید تامین مرکزی", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestStatus { Id = Guid.Parse("E0012511-EC7B-498C-8EE0-ECBF6A7EC63B"), StockProductRequestStatusName = "منتظر ارسال", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestStatus { Id = Guid.Parse("9AE7E7BB-896E-4EFB-9279-D0EEFB830B3F"), StockProductRequestStatusName = "تحویل شده", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.BaseTypes.AddOrUpdate(item => item.Id,
                new Anatoli.DataAccess.Models.BaseType { Id = Guid.Parse("A0EA0430-6E42-4E5F-B809-E05E13D73E54"), BaseTypeDesc = "OrderSource", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseType { Id = Guid.Parse("F5FFAD55-6E39-40BD-A95D-12A34BA4D005"), BaseTypeDesc = "DeliveryType", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseType { Id = Guid.Parse("CA2BE6B3-7187-4D81-9198-B9433E726C9C"), BaseTypeDesc = "BasketType", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseType { Id = Guid.Parse("CF0016A1-E832-4EA4-9DE8-3E4C32C1EDA7"), BaseTypeDesc = "ProductType", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseType { Id = Guid.Parse("F17B8898-D39F-4955-9757-A6B31767F5C7"), BaseTypeDesc = "PayType", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseType { Id = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), BaseTypeDesc = "PurchaseOrderStatus", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.BaseValues.AddOrUpdate(item => item.Id,
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("B73A7482-E4BD-4B95-8E05-4127D6AD01D4"), BaseValueName = "کالاي فله اي", BaseTypeId = Guid.Parse("CF0016A1-E832-4EA4-9DE8-3E4C32C1EDA7"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("24832A67-BA42-4477-AD11-4AE9B36C2B1D"), BaseValueName = "چك مدت", BaseTypeId = Guid.Parse("F17B8898-D39F-4955-9757-A6B31767F5C7"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("B63FCB2D-ED98-4029-86B1-5B9D64DD5320"), BaseValueName = "رسيد", BaseTypeId = Guid.Parse("F17B8898-D39F-4955-9757-A6B31767F5C7"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("BE2919AB-5564-447A-BE49-65A81E6AF712"), BaseValueName = "دريافت از محل", BaseTypeId = Guid.Parse("F5FFAD55-6E39-40BD-A95D-12A34BA4D005"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("CE4AEE25-F8A7-404F-8DBA-80340F7339CC"), BaseValueName = "تحويل درب منزل", BaseTypeId = Guid.Parse("F5FFAD55-6E39-40BD-A95D-12A34BA4D005"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("4A0EC7BC-3AF4-4702-B230-8F908ECCE680"), BaseValueName = "كالاي تعدادي", BaseTypeId = Guid.Parse("CF0016A1-E832-4EA4-9DE8-3E4C32C1EDA7"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("6CF27F09-E162-4802-A451-9BC3304A8130"), BaseValueName = "سایر", BaseTypeId = Guid.Parse("A0EA0430-6E42-4E5F-B809-E05E13D73E54"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("AE5DE00D-3391-49FE-985B-9DA7045CDB13"), BaseValueName = "Favorite", BaseTypeId = Guid.Parse("CA2BE6B3-7187-4D81-9198-B9433E726C9C"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("41581E50-9928-4A3C-A513-A32DBB3B3D0D"), BaseValueName = "ساير", BaseTypeId = Guid.Parse("CA2BE6B3-7187-4D81-9198-B9433E726C9C"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("3A27504C-A9BA-46CE-9376-A63403BFE82A"), BaseValueName = "نقد", BaseTypeId = Guid.Parse("F17B8898-D39F-4955-9757-A6B31767F5C7"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("86136133-A701-4308-898D-D2E943EBE38E"), BaseValueName = "چك روز", BaseTypeId = Guid.Parse("F17B8898-D39F-4955-9757-A6B31767F5C7"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("0410F5BD-0C01-4E32-A4D9-D2F4DCC46003"), BaseValueName = "سايت", BaseTypeId = Guid.Parse("A0EA0430-6E42-4E5F-B809-E05E13D73E54"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("F6CE03E2-8A2A-4996-8739-DA9C21EAD787"), BaseValueName = "Checkout", BaseTypeId = Guid.Parse("CA2BE6B3-7187-4D81-9198-B9433E726C9C"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("194CA845-2E34-4A06-9A89-DCAFF956FE4D"), BaseValueName = "سفارش ناقص", BaseTypeId = Guid.Parse("CA2BE6B3-7187-4D81-9198-B9433E726C9C"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("65DEC223-059E-48BA-8281-E4FAAFF6E32D"), BaseValueName = "App", BaseTypeId = Guid.Parse("A0EA0430-6E42-4E5F-B809-E05E13D73E54"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("819E80CD-289E-416C-ADC6-FA6C6DE8D2CE"), BaseValueName = "خدمات", BaseTypeId = Guid.Parse("CF0016A1-E832-4EA4-9DE8-3E4C32C1EDA7"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("A591658A-E46B-440D-9ADB-E3E5B01B7489"), BaseValueName = "پیش نویس", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("5C0D43FC-6822-4D39-AB40-363B885BE464"), BaseValueName = "فاکتور شده", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("EA5961AB-792A-4D20-8A52-5501F01F034A"), BaseValueName = "منتظر تحویل", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("3AA22CED-A45E-4E58-B992-A4B1F838B19B"), BaseValueName = "تحویل شده ", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("D12DEEE1-DA5B-44F6-937A-7B282789908F"), BaseValueName = "تسویه شده", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.SaveChanges();
        }

        public override void Down()
        {
        }
    }
}
