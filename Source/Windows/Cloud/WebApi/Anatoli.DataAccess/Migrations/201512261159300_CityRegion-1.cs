namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityRegion1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProductGroups", name: "ProductGroup2_Id", newName: "ProductGroup2Id");
            RenameIndex(table: "dbo.ProductGroups", name: "IX_ProductGroup2_Id", newName: "IX_ProductGroup2Id");
            AddColumn("dbo.CityRegions", "CityRegion2Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.CityRegions", "CityRegion2Id");
            AddForeignKey("dbo.CityRegions", "CityRegion2Id", "dbo.CityRegions", "Id");
            DropColumn("dbo.CityRegions", "ParentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CityRegions", "ParentId", c => c.Guid());
            DropForeignKey("dbo.CityRegions", "CityRegion2Id", "dbo.CityRegions");
            DropIndex("dbo.CityRegions", new[] { "CityRegion2Id" });
            DropColumn("dbo.CityRegions", "CityRegion2Id");
            RenameIndex(table: "dbo.ProductGroups", name: "IX_ProductGroup2Id", newName: "IX_ProductGroup2_Id");
            RenameColumn(table: "dbo.ProductGroups", name: "ProductGroup2Id", newName: "ProductGroup2_Id");
        }
    }
}
