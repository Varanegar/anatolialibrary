namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncompletePurchaseOrder3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IncompletePurchaseOrders", "CityRegionId", "dbo.CityRegions");
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "CityRegionId" });
            AddColumn("dbo.IncompletePurchaseOrders", "PaymentTypeId", c => c.Guid(nullable: false));
            AlterColumn("dbo.IncompletePurchaseOrders", "CityRegionId", c => c.Guid(nullable: false));
            AlterColumn("dbo.IncompletePurchaseOrders", "DeliveryTypeId", c => c.Guid(nullable: false));
            CreateIndex("dbo.IncompletePurchaseOrders", "CityRegionId");
            AddForeignKey("dbo.IncompletePurchaseOrders", "CityRegionId", "dbo.CityRegions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IncompletePurchaseOrders", "CityRegionId", "dbo.CityRegions");
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "CityRegionId" });
            AlterColumn("dbo.IncompletePurchaseOrders", "DeliveryTypeId", c => c.Guid());
            AlterColumn("dbo.IncompletePurchaseOrders", "CityRegionId", c => c.Guid());
            DropColumn("dbo.IncompletePurchaseOrders", "PaymentTypeId");
            CreateIndex("dbo.IncompletePurchaseOrders", "CityRegionId");
            AddForeignKey("dbo.IncompletePurchaseOrders", "CityRegionId", "dbo.CityRegions", "Id");
        }
    }
}
