namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MergeMigration1 : DbMigration
    {
        public override void Up()
        {/*
            CreateTable(
                "dbo.PermissionCatalogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        PermissionCatalougeParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PermissionCatalogs", t => t.PermissionCatalougeParentId)
                .Index(t => t.PermissionCatalougeParentId);
            
            CreateTable(
                "dbo.PrincipalPermissionCatalogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Grant = c.Int(nullable: false),
                        PermissionCatalog_Id = c.Guid(nullable: false),
                        PrincipalId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PermissionCatalogs", t => t.PermissionCatalog_Id, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.PrincipalId, cascadeDelete: true)
                .Index(t => t.PermissionCatalog_Id)
                .Index(t => t.PrincipalId);
            
            CreateTable(
                "dbo.PermissionCatalogPermissions",
                c => new
                    {
                        PermissionCatalog_Id = c.Guid(nullable: false),
                        Permission_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionCatalog_Id, t.Permission_Id })
                .ForeignKey("dbo.PermissionCatalogs", t => t.PermissionCatalog_Id, cascadeDelete: true)
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .Index(t => t.PermissionCatalog_Id)
                .Index(t => t.Permission_Id);
          * */
            
        }
        
        public override void Down()
        {
            /*
            DropForeignKey("dbo.PrincipalPermissionCatalogs", "PrincipalId", "dbo.Principals");
            DropForeignKey("dbo.PrincipalPermissionCatalogs", "PermissionCatalog_Id", "dbo.PermissionCatalogs");
            DropForeignKey("dbo.PermissionCatalogPermissions", "Permission_Id", "dbo.Permissions");
            DropForeignKey("dbo.PermissionCatalogPermissions", "PermissionCatalog_Id", "dbo.PermissionCatalogs");
            DropForeignKey("dbo.PermissionCatalogs", "PermissionCatalougeParentId", "dbo.PermissionCatalogs");
            DropIndex("dbo.PermissionCatalogPermissions", new[] { "Permission_Id" });
            DropIndex("dbo.PermissionCatalogPermissions", new[] { "PermissionCatalog_Id" });
            DropIndex("dbo.PrincipalPermissionCatalogs", new[] { "PrincipalId" });
            DropIndex("dbo.PrincipalPermissionCatalogs", new[] { "PermissionCatalog_Id" });
            DropIndex("dbo.PermissionCatalogs", new[] { "PermissionCatalougeParentId" });
            DropTable("dbo.PermissionCatalogPermissions");
            DropTable("dbo.PrincipalPermissionCatalogs");
            DropTable("dbo.PermissionCatalogs");
             * */
        }
    }
}
