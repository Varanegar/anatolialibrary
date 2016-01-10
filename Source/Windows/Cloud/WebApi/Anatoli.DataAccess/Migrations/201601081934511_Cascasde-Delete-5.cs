namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascasdeDelete5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "IncompletePurchaseOrderId", "dbo.IncompletePurchaseOrders");
            AddForeignKey("dbo.IncompletePurchaseOrderLineItems", "IncompletePurchaseOrderId", "dbo.IncompletePurchaseOrders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "IncompletePurchaseOrderId", "dbo.IncompletePurchaseOrders");
            AddForeignKey("dbo.IncompletePurchaseOrderLineItems", "IncompletePurchaseOrderId", "dbo.IncompletePurchaseOrders", "Id");
        }
    }
}
