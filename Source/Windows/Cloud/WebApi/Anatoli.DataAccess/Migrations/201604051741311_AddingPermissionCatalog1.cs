namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPermissionCatalog1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PermissionCatalogs", new[] { "PermissionCatalougeParentId" });
            AlterColumn("dbo.PermissionCatalogs", "PermissionCatalougeParentId", c => c.Guid());
            CreateIndex("dbo.PermissionCatalogs", "PermissionCatalougeParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PermissionCatalogs", new[] { "PermissionCatalougeParentId" });
            AlterColumn("dbo.PermissionCatalogs", "PermissionCatalougeParentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.PermissionCatalogs", "PermissionCatalougeParentId");
        }
    }
}
