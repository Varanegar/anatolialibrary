namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    
    public partial class TrackingMigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "CustomerPoint", c => c.Geometry());
            AddColumn("dbo.RegionAreas", "AreaLocation", c => c.Geometry());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegionAreas", "AreaLocation");
            DropColumn("dbo.Customers", "CustomerPoint");
        }
    }
}
