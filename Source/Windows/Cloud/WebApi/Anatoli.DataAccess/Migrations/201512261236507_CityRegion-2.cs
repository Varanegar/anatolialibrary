namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityRegion2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CityRegions", new[] { "CityRegion2Id" });
            AlterColumn("dbo.CityRegions", "CityRegion2Id", c => c.Guid());
            CreateIndex("dbo.CityRegions", "CityRegion2Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CityRegions", new[] { "CityRegion2Id" });
            AlterColumn("dbo.CityRegions", "CityRegion2Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.CityRegions", "CityRegion2Id");
        }
    }
}
