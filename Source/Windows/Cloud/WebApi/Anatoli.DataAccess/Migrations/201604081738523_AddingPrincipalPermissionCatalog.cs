namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPrincipalPermissionCatalog : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrincipalPermissionCatalogs", "PrincipalId", "dbo.Principals");
            DropForeignKey("dbo.PrincipalPermissionCatalogs", "PermissionCatalog_Id", "dbo.PermissionCatalogs");
            DropIndex("dbo.PrincipalPermissionCatalogs", new[] { "PrincipalId" });
            DropIndex("dbo.PrincipalPermissionCatalogs", new[] { "PermissionCatalog_Id" });
            DropTable("dbo.PrincipalPermissionCatalogs");
        }
    }
}
