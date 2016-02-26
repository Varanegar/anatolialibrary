namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseOrderAddress : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PurchaseOrders", new[] { "CustomerShipAddressId" });
            AlterColumn("dbo.PurchaseOrders", "CustomerShipAddressId", c => c.Guid());
            CreateIndex("dbo.PurchaseOrders", "CustomerShipAddressId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PurchaseOrders", new[] { "CustomerShipAddressId" });
            AlterColumn("dbo.PurchaseOrders", "CustomerShipAddressId", c => c.Guid(nullable: false));
            CreateIndex("dbo.PurchaseOrders", "CustomerShipAddressId");
        }
    }
}
