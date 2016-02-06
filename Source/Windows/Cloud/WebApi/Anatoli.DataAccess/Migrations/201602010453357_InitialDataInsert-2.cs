namespace Anatoli.DataAccess.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity.Migrations;
    
    public partial class InitialDataInsert2 : DbMigration
    {
        public override void Up()
        {
            AnatoliDbContext context = new AnatoliDbContext();
            context.UserRoles.AddOrUpdate(item => new { item.UserId, item.RoleId },
                new IdentityUserRole { RoleId = "4447853b-e19f-42ce-bb29-f5aa1943b542", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" },
                new IdentityUserRole { RoleId = "4d10bd96-7f25-477a-a544-75e54b619a1f", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" },
                new IdentityUserRole { RoleId = "507b6966-17f1-4116-a497-02242c052961", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" },
                new IdentityUserRole { RoleId = "5a61344b-b1b5-4157-8861-7bed15c0bdc2", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" },
                new IdentityUserRole { RoleId = "95B93EF1-9F67-4EB5-AD3E-569C64DAF4E3", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" },
                new IdentityUserRole { RoleId = "AE4AF236-E229-45A8-B1C0-CBE6CB104721", UserId = "02d3c1aa-6149-4810-9f83-df3928bfdf16" },
                new IdentityUserRole { RoleId = "4447853b-e19f-42ce-bb29-f5aa1943b542", UserId = "0dab1636-ae22-4abe-a18d-6ec7b8e9c544" },
                new IdentityUserRole { RoleId = "4d10bd96-7f25-477a-a544-75e54b619a1f", UserId = "0dab1636-ae22-4abe-a18d-6ec7b8e9c544" },
                new IdentityUserRole { RoleId = "4447853b-e19f-42ce-bb29-f5aa1943b542", UserId = "3eee33ce-e2fd-4a5d-a71c-103cc5046d0c" },
                new IdentityUserRole { RoleId = "4d10bd96-7f25-477a-a544-75e54b619a1f", UserId = "3eee33ce-e2fd-4a5d-a71c-103cc5046d0c" },
                new IdentityUserRole { RoleId = "4447853b-e19f-42ce-bb29-f5aa1943b542", UserId = "95fcb850-2e63-4b26-8dbf-bbc86b7f5046" },
                new IdentityUserRole { RoleId = "AE4AF236-E229-45A8-B1C0-CBE6CB104721", UserId = "95fcb850-2e63-4b26-8dbf-bbc86b7f5046" },
                new IdentityUserRole { RoleId = "4447853b-e19f-42ce-bb29-f5aa1943b542", UserId = "33fa710a-b1e6-4765-8719-0dd1589e8f8b" },
                new IdentityUserRole { RoleId = "95B93EF1-9F67-4EB5-AD3E-569C64DAF4E3", UserId = "33fa710a-b1e6-4765-8719-0dd1589e8f8b" }
            );
            context.SaveChanges();
        }
        
        public override void Down()
        {
        }
    }
}
