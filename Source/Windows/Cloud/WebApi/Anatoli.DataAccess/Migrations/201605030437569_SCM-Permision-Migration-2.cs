namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SCMPermisionMigration2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PermissionCatalogPermissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Permission_Id = c.Guid(),
                        PermissionCatalog_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.Permission_Id)
                .ForeignKey("dbo.PermissionCatalogs", t => t.PermissionCatalog_Id)
                .Index(t => t.Permission_Id)
                .Index(t => t.PermissionCatalog_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermissionCatalogPermissions", "PermissionCatalog_Id", "dbo.PermissionCatalogs");
            DropForeignKey("dbo.PermissionCatalogPermissions", "Permission_Id", "dbo.Permissions");
            DropIndex("dbo.PermissionCatalogPermissions", new[] { "PermissionCatalog_Id" });
            DropIndex("dbo.PermissionCatalogPermissions", new[] { "Permission_Id" });
            DropTable("dbo.PermissionCatalogPermissions");
        }
    }
}
