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

    public sealed class Configuration : DbMigrationsConfiguration<Anatoli.DataAccess.AnatoliDbContext>
    {
        private readonly bool _pendingMigrations;
        public Configuration()
        {
            var migrator = new DbMigrator(this);
            var _pendingMigrations = migrator.GetPendingMigrations().Any();
        }

        protected override void Seed(Anatoli.DataAccess.AnatoliDbContext context)
        {
            //if (!_pendingMigrations) return;
            context.Applications.AddOrUpdate(item => item.Id,
                new Application { Id = Guid.Parse("8A074FD5-9311-4F8E-AF47-0572DE1A7B6A"), Name = "Anatoli Market place" },
                new Application { Id = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), Name = "VN Cloud" }
                );

            context.AnatoliContactTypes.AddOrUpdate(item => item.Id,
                new AnatoliContactType { Id = Guid.Parse("0B8F7429-B33C-4209-ABED-1192E7B36657"), Name = "حقیقی" },
                new AnatoliContactType { Id = Guid.Parse("8E1DB1BC-52FD-456B-95FB-F2BBCA07C87D"), Name = "حقوقی" },
                new AnatoliContactType { Id = Guid.Parse("ED1220EF-5BD2-4BFE-B5C8-7D3713143A6A"), Name = "فروشگاه" }
            );


            context.AnatoliContacts.AddOrUpdate(
                new AnatoliContact { Id = Guid.Parse("D186DFBC-611F-42ED-B3D8-ACF92A8DE3C9"), AnatoliContactTypeId = Guid.Parse("8E1DB1BC-52FD-456B-95FB-F2BBCA07C87D"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, IsRemoved = false, ContactName = "نیک توشه زیست", Phone = "+98-21-", Email = "info@eiggstores.com", Website = "http://www.eiggstores.com/" },
                //new AnatoliContact { Id = Guid.Parse("A60F9E34-5A69-4B0A-A555-CA1F54343D74"), AnatoliContactTypeId = Guid.Parse("8E1DB1BC-52FD-456B-95FB-F2BBCA07C87D"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, IsRemoved = false, ContactName = "شرکت چی توز", Phone = "+98-21-", Email = "info@", Website = "" }
                    new AnatoliContact { Id = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16"), AnatoliContactTypeId = Guid.Parse("8E1DB1BC-52FD-456B-95FB-F2BBCA07C87D"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, IsRemoved = false, ContactName = "داده کاوان پیشرو ایده ورانگر", Phone = "+98-21-87134", Email = "info@varanegar.com", Website = "http://www.varanegar.com/" }
                );

            context.ApplicationOwners.AddOrUpdate(item => item.Id,
                new ApplicationOwner { Id = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), AnatoliContactId = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16"), Title = "نرم افزارهای ابری داده کاوان پیشرو ایده ورانگر", WebHookUsername = "anatoli-inter-com@varanegar.com" },
                new ApplicationOwner { Id = Guid.Parse("E0468F3E-CA81-4867-8768-52F18887BEF8"), ApplicationId = Guid.Parse("8A074FD5-9311-4F8E-AF47-0572DE1A7B6A"), AnatoliContactId = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16"), Title = "Parastoo Market Place", WebHookUsername = "anatoli-inter-com@varanegar.com" }
                //new ApplicationOwner { Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), AnatoliContactId = Guid.Parse("D186DFBC-611F-42ED-B3D8-ACF92A8DE3C9"), Title = "نرم افزار فروشگاهی نیک توشه زیست", WebHookUsername = "anatoli-inter-com@varanegar.com" },
                //new ApplicationOwner { Id = Guid.Parse("897DAD91-EB44-407A-9C63-9132132E1F99"), ApplicationId = Guid.Parse("61B8646F-77D4-49D1-8949-909EA771DEED"), AnatoliContactId = Guid.Parse("D186DFBC-611F-42ED-B3D8-ACF92A8DE3C9"), Title = "نرم افزار تامین داخلی نیک توشه زیست", WebHookUsername = "anatoli-inter-com@varanegar.com" }
            );

            context.DataOwners.AddOrUpdate(item => item.Id,
                new DataOwner { Id = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), AnatoliContactId = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16"), Title = "نرم افزارهای ابری داده کاوان پیشرو ایده ورانگر", WebHookUsername = "anatoli-inter-com@varanegar.com" },
                new DataOwner { Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), AnatoliContactId = Guid.Parse("D186DFBC-611F-42ED-B3D8-ACF92A8DE3C9"), Title = "نرم افزار فروش اینترنتی نیک توشه زیست", WebHookUsername = "info@eiggstores.com" }
            );

            context.DataOwnerCenters.AddOrUpdate(item => item.Id,
                new DataOwnerCenter { Id = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), AnatoliContactId = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16"), Title = "نرم افزارهای ابری داده کاوان پیشرو ایده ورانگر", WebHookUsername = "anatoli-inter-com@varanegar.com" },
                new DataOwnerCenter { Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), DataOwnerId = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), AnatoliContactId = Guid.Parse("D186DFBC-611F-42ED-B3D8-ACF92A8DE3C9"), Title = "نرم افزار فروش اینترنتی نیک توشه زیست", WebHookUsername = "info@eiggstores.com" }
            );

            context.Roles.AddOrUpdate(item => item.Id,
                new Role { Id = "4447853b-e19f-42ce-bb29-f5aa1943b542", ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), Name = "User" },
                new Role { Id = "4d10bd96-7f25-477a-a544-75e54b619a1f", ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), Name = "AuthorizedApp" },
                new Role { Id = "C0614C05-855F-45C6-A93C-EB3B8A8B2D94", ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), Name = "DataSync" },
                new Role { Id = "507b6966-17f1-4116-a497-02242c052961", ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), Name = "SuperAdmin" },
                new Role { Id = "5a61344b-b1b5-4157-8861-7bed15c0bdc2", ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), Name = "Admin" },
                new Role { Id = "AE4AF236-E229-45A8-B1C0-CBE6CB104721", ApplicationId = Guid.Parse("081AF21C-06E4-44DD-88B4-0A68710131DC"), Name = "InternalCommunication" }
            );

            //context.ProductType.AddOrUpdate(item => item.Id,
            //    new ProductType { Id = Guid.Parse("72E59112-6054-4140-8E33-947228616393"), ProductTypeName = "کالای زیر صفر", IsRemoved = false, ApplicationOwnerId = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
            //    new ProductType { Id = Guid.Parse("6FC2FD34-4CBC-4EB1-BD7E-1BD751E4F2A2"), ProductTypeName = "کالای یخچالی", IsRemoved = false, ApplicationOwnerId = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
            //    new ProductType { Id = Guid.Parse("8E85E5B0-9242-47A1-99A1-7B90566C36D4"), ProductTypeName = "انباری ساده", IsRemoved = false, ApplicationOwnerId = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
            //    );

            context.StockProductRequestTypes.AddOrUpdate(item => item.Id,
                new StockProductRequestType { Id = Guid.Parse("1462A36B-9AB0-41AB-88F1-AAD152A7E425"), StockProductRequestTypeName = "محاسبه دوره ای", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestType { Id = Guid.Parse("252B2E6A-AC0E-49B7-9BE7-F559A2BAC847"), StockProductRequestTypeName = "محاسبه دستی", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestType { Id = Guid.Parse("28BC2DEE-3839-48E7-B50B-D8B8D0CCA191"), StockProductRequestTypeName = "محاسبه میان روز اضطراری", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestType { Id = Guid.Parse("47CF63BD-C297-441F-810E-0D685333CC39"), StockProductRequestTypeName = "محاسبه آزمایشی", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.ReorderCalcTypes.AddOrUpdate(item => item.Id,
                new ReorderCalcType { Id = Guid.Parse("7ECB9525-EA5F-487E-BBB6-971B9B22D7FF"), ReorderTypeName = "کالا به تنهایی", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new ReorderCalcType { Id = Guid.Parse("3341AFFC-6885-415E-8BEB-1EE0AA5B6405"), ReorderTypeName = "کالاهای گروه کالا", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new ReorderCalcType { Id = Guid.Parse("780C2900-ED36-4D73-A898-54CDC326B5E7"), ReorderTypeName = "کالاهای تامین کننده", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.StockTypes.AddOrUpdate(item => item.Id,
                new StockType { Id = Guid.Parse("8a911bf9-dee1-449b-b407-a04a93e976c3"), StockTypeName = "انبار فروشگاه", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockType { Id = Guid.Parse("6ace8810-1261-4c47-98a0-f414c4d6f79a"), StockTypeName = "انبار اصلی تامین", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockType { Id = Guid.Parse("fe362433-401d-4b1f-b1fc-b75f0853bd44"), StockTypeName = "انبار شعبه تامین", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockType { Id = Guid.Parse("92a9b97a-0e51-4ed3-b012-f1bde2e62f9b"), StockTypeName = "انبار حوزه", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.StockProductRequestRuleTypes.AddOrUpdate(item => item.Id,
                new StockProductRequestRuleType { Id = Guid.Parse("9f1f9ce4-4d5d-4885-9458-16eb24bf1b59"), StockProductRequestRuleTypeName = "بر اساس نقطه سفارش بدون کالاهای وابسته", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestRuleType { Id = Guid.Parse("9a82849b-178f-4da1-85e7-700fee41a58a"), StockProductRequestRuleTypeName = "بر اساس نقطه سفارش با کالاهای وابسته", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestRuleType { Id = Guid.Parse("4a840ae4-2bd5-406c-9dca-8e582b41f8de"), StockProductRequestRuleTypeName = " افزودن به درخواست بدون کالاهای وابسته", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.StockProductRequestSupplyTypes.AddOrUpdate(item => item.Id,
                new StockProductRequestSupplyType { Id = Guid.Parse("454422E2-8751-4F28-A31D-0C3E8230FA81"), StockProductRequestSupplyTypeName = "تامین از تامین کننده", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestSupplyType { Id = Guid.Parse("C6968D05-CFFD-4269-8129-E0F99E63B656"), StockProductRequestSupplyTypeName = "تامین از انبار تامین مرکزی", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestSupplyType { Id = Guid.Parse("AB787E71-2120-45D7-9B69-021E60FEA0BB"), StockProductRequestSupplyTypeName = "تامین از انبار تامین مرتبط", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.StockProductRequestStatuses.AddOrUpdate(item => item.Id,
                new StockProductRequestStatus { Id = Guid.Parse("88B91155-5E56-4C48-BE2B-416C7EDA1713"), StockProductRequestStatusName = "منتظر تایید مسوول فروشگاه", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestStatus { Id = Guid.Parse("770803A2-3F46-48D1-98D2-2D656F6297DD"), StockProductRequestStatusName = "منتظر تایید سرپرست", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestStatus { Id = Guid.Parse("C692FBF0-B133-4F01-B154-8441D62F65CF"), StockProductRequestStatusName = "منتظر تایید تامین مرکزی", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestStatus { Id = Guid.Parse("E0012511-EC7B-498C-8EE0-ECBF6A7EC63B"), StockProductRequestStatusName = "منتظر ارسال", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new StockProductRequestStatus { Id = Guid.Parse("9AE7E7BB-896E-4EFB-9279-D0EEFB830B3F"), StockProductRequestStatusName = "تحویل شده", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.BaseTypes.AddOrUpdate(item => item.Id,
                new Anatoli.DataAccess.Models.BaseType { Id = Guid.Parse("A0EA0430-6E42-4E5F-B809-E05E13D73E54"), BaseTypeDesc = "OrderSource", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseType { Id = Guid.Parse("F5FFAD55-6E39-40BD-A95D-12A34BA4D005"), BaseTypeDesc = "DeliveryType", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseType { Id = Guid.Parse("CA2BE6B3-7187-4D81-9198-B9433E726C9C"), BaseTypeDesc = "BasketType", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseType { Id = Guid.Parse("CF0016A1-E832-4EA4-9DE8-3E4C32C1EDA7"), BaseTypeDesc = "ProductType", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseType { Id = Guid.Parse("F17B8898-D39F-4955-9757-A6B31767F5C7"), BaseTypeDesc = "PayType", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseType { Id = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), BaseTypeDesc = "PurchaseOrderStatus", IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.BaseValues.AddOrUpdate(item => item.Id,
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("B73A7482-E4BD-4B95-8E05-4127D6AD01D4"), BaseValueName = "کالاي فله اي", BaseTypeId = Guid.Parse("CF0016A1-E832-4EA4-9DE8-3E4C32C1EDA7"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("24832A67-BA42-4477-AD11-4AE9B36C2B1D"), BaseValueName = "چك مدت", BaseTypeId = Guid.Parse("F17B8898-D39F-4955-9757-A6B31767F5C7"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("B63FCB2D-ED98-4029-86B1-5B9D64DD5320"), BaseValueName = "رسيد", BaseTypeId = Guid.Parse("F17B8898-D39F-4955-9757-A6B31767F5C7"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("BE2919AB-5564-447A-BE49-65A81E6AF712"), BaseValueName = "دريافت از محل", BaseTypeId = Guid.Parse("F5FFAD55-6E39-40BD-A95D-12A34BA4D005"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("CE4AEE25-F8A7-404F-8DBA-80340F7339CC"), BaseValueName = "تحويل درب منزل", BaseTypeId = Guid.Parse("F5FFAD55-6E39-40BD-A95D-12A34BA4D005"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("4A0EC7BC-3AF4-4702-B230-8F908ECCE680"), BaseValueName = "كالاي تعدادي", BaseTypeId = Guid.Parse("CF0016A1-E832-4EA4-9DE8-3E4C32C1EDA7"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("6CF27F09-E162-4802-A451-9BC3304A8130"), BaseValueName = "سایر", BaseTypeId = Guid.Parse("A0EA0430-6E42-4E5F-B809-E05E13D73E54"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("AE5DE00D-3391-49FE-985B-9DA7045CDB13"), BaseValueName = "Favorite", BaseTypeId = Guid.Parse("CA2BE6B3-7187-4D81-9198-B9433E726C9C"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("41581E50-9928-4A3C-A513-A32DBB3B3D0D"), BaseValueName = "ساير", BaseTypeId = Guid.Parse("CA2BE6B3-7187-4D81-9198-B9433E726C9C"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("3A27504C-A9BA-46CE-9376-A63403BFE82A"), BaseValueName = "نقد", BaseTypeId = Guid.Parse("F17B8898-D39F-4955-9757-A6B31767F5C7"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("86136133-A701-4308-898D-D2E943EBE38E"), BaseValueName = "چك روز", BaseTypeId = Guid.Parse("F17B8898-D39F-4955-9757-A6B31767F5C7"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("0410F5BD-0C01-4E32-A4D9-D2F4DCC46003"), BaseValueName = "سايت", BaseTypeId = Guid.Parse("A0EA0430-6E42-4E5F-B809-E05E13D73E54"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("F6CE03E2-8A2A-4996-8739-DA9C21EAD787"), BaseValueName = "Checkout", BaseTypeId = Guid.Parse("CA2BE6B3-7187-4D81-9198-B9433E726C9C"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("194CA845-2E34-4A06-9A89-DCAFF956FE4D"), BaseValueName = "سفارش ناقص", BaseTypeId = Guid.Parse("CA2BE6B3-7187-4D81-9198-B9433E726C9C"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("65DEC223-059E-48BA-8281-E4FAAFF6E32D"), BaseValueName = "App", BaseTypeId = Guid.Parse("A0EA0430-6E42-4E5F-B809-E05E13D73E54"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("819E80CD-289E-416C-ADC6-FA6C6DE8D2CE"), BaseValueName = "خدمات", BaseTypeId = Guid.Parse("CF0016A1-E832-4EA4-9DE8-3E4C32C1EDA7"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("A591658A-E46B-440D-9ADB-E3E5B01B7489"), BaseValueName = "پیش نویس", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("5C0D43FC-6822-4D39-AB40-363B885BE464"), BaseValueName = "فاکتور شده", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("EA5961AB-792A-4D20-8A52-5501F01F034A"), BaseValueName = "منتظر تحویل", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("3AA22CED-A45E-4E58-B992-A4B1F838B19B"), BaseValueName = "تحویل شده ", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, },
                new Anatoli.DataAccess.Models.BaseValue { Id = Guid.Parse("D12DEEE1-DA5B-44F6-937A-7B282789908F"), BaseValueName = "تسویه شده", BaseTypeId = Guid.Parse("80EAB271-D2BD-4119-92EF-A6F2DD867A6B"), IsRemoved = false, ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );

            context.StockProductRequestRuleCalcTypes.AddOrUpdate(item => item.Id,
                new StockProductRequestRuleCalcType
                {
                    Id = Guid.Parse("D4AA1C1A-536C-4596-AF30-73E1A771417B"),
                    StockProductRequestRuleCalcTypeName = "قانون عادی",
                    IsRemoved = false,
                    ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
                    DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
                    DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
                    CreatedDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                }
                );

            context.StockProductRequestRules.AddOrUpdate(item => item.Id,
                new StockProductRequestRule
                {
                    Id = Guid.Parse("cc9b0f95-c067-4ee4-bff9-e1fb19853f52"),
                    StockProductRequestRuleCalcTypeId = Guid.Parse("D4AA1C1A-536C-4596-AF30-73E1A771417B"),
                    StockProductRequestRuleTypeId = Guid.Parse("9f1f9ce4-4d5d-4885-9458-16eb24bf1b59"),
                    ReorderCalcTypeId = Guid.Parse("7ECB9525-EA5F-487E-BBB6-971B9B22D7FF"),
                    StockProductRequestRuleName = "بر اساس نقطه سفارش",
                    FromDate = DateTime.Parse("2011/03/21"),
                    FromPDate = "1390/01/01",
                    ToDate = DateTime.Parse("2111/03/21"),
                    ToPDate = "1490/01/01",
                    IsRemoved = false,
                    ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
                    DataOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
                    DataOwnerCenterId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
                    CreatedDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                }
                );

            #region Add Users
            var userId = "02D3C1AA-6149-4810-9F83-DF3928BFDF16";
            if (!context.Users.Any(item => item.Id == userId))
            {
                context.Users.AddOrUpdate(item => item.Id,
                    new User
                    {
                        Id = userId,
                        PhoneNumber = "87135000",
                        UserName = "anatoli",
                        PasswordHash = "AJ1iTXc0/EgQyLWeHZFh4xrX6vpu37VCmAfcTNm1bUBU+zcc2dqnTKuXyeZmhfC4+A==",
                        Email = "anatoli@varanegar.com",
                        EmailConfirmed = true,
                        AnatoliContactId = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16"),
                        PhoneNumberConfirmed = true,
                        CreatedDate = DateTime.Now,
                        LastUpdate = DateTime.Now,
                        LastEntry = DateTime.Now,
                        ApplicationOwnerId = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
                        SecurityStamp = "28a22f1c-83d8-4aee-8f9e-02441f25092c",
                    });


            }

            if (context.Users.Any(item => item.Id == userId))
            {
                context.UserRoles.AddOrUpdate(item => new { item.RoleId, item.UserId },
                    new IdentityUserRole { RoleId = "507b6966-17f1-4116-a497-02242c052961", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" },
                    new IdentityUserRole { RoleId = "5a61344b-b1b5-4157-8861-7bed15c0bdc2", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" },
                    new IdentityUserRole { RoleId = "4d10bd96-7f25-477a-a544-75e54b619a1f", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" },
                    new IdentityUserRole { RoleId = "C0614C05-855F-45C6-A93C-EB3B8A8B2D94", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" },
                    new IdentityUserRole { RoleId = "AE4AF236-E229-45A8-B1C0-CBE6CB104721", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" }
                );
            }
            #endregion
        }
    }
}
