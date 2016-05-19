namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SCMPermisionMigration1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PermissionCatalogPermissions", "PermissionCatalog_Id", "dbo.PermissionCatalogs");
            DropForeignKey("dbo.PermissionCatalogPermissions", "Permission_Id", "dbo.Permissions");
            DropIndex("dbo.PermissionCatalogPermissions", new[] { "PermissionCatalog_Id" });
            DropIndex("dbo.PermissionCatalogPermissions", new[] { "Permission_Id" });
            CreateTable(
                "dbo.PrincipalStocks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PrincipalId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                        PrincipalStock_Id = c.Guid(),
                        Stock_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .ForeignKey("dbo.PrincipalStocks", t => t.PrincipalStock_Id)
                .ForeignKey("dbo.Stocks", t => t.Stock_Id)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById)
                .Index(t => t.PrincipalStock_Id)
                .Index(t => t.Stock_Id);
            
            DropTable("dbo.PermissionCatalogPermissions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PermissionCatalogPermissions",
                c => new
                    {
                        PermissionCatalog_Id = c.Guid(nullable: false),
                        Permission_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionCatalog_Id, t.Permission_Id });
            
            DropForeignKey("dbo.PrincipalStocks", "Stock_Id", "dbo.Stocks");
            DropForeignKey("dbo.PrincipalStocks", "PrincipalStock_Id", "dbo.PrincipalStocks");
            DropForeignKey("dbo.PrincipalStocks", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.PrincipalStocks", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PrincipalStocks", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PrincipalStocks", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PrincipalStocks", "AddedById", "dbo.Principals");
            DropIndex("dbo.PrincipalStocks", new[] { "Stock_Id" });
            DropIndex("dbo.PrincipalStocks", new[] { "PrincipalStock_Id" });
            DropIndex("dbo.PrincipalStocks", new[] { "LastModifiedById" });
            DropIndex("dbo.PrincipalStocks", new[] { "AddedById" });
            DropIndex("dbo.PrincipalStocks", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PrincipalStocks", new[] { "DataOwnerId" });
            DropIndex("dbo.PrincipalStocks", new[] { "ApplicationOwnerId" });
            DropTable("dbo.PrincipalStocks");
            CreateIndex("dbo.PermissionCatalogPermissions", "Permission_Id");
            CreateIndex("dbo.PermissionCatalogPermissions", "PermissionCatalog_Id");
            AddForeignKey("dbo.PermissionCatalogPermissions", "Permission_Id", "dbo.Permissions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PermissionCatalogPermissions", "PermissionCatalog_Id", "dbo.PermissionCatalogs", "Id", cascadeDelete: true);
        }
    }
}
