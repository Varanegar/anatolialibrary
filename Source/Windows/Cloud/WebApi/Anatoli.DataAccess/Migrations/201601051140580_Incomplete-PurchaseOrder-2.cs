namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncompletePurchaseOrder2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "ProductId", "dbo.Products");
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "IncompletePurchaseOrder_Id" });
            RenameColumn(table: "dbo.IncompletePurchaseOrderLineItems", name: "IncompletePurchaseOrder_Id", newName: "IncompletePurchaseOrderId");
            AddColumn("dbo.IncompletePurchaseOrders", "CustomerId", c => c.Guid(nullable: false));
            AlterColumn("dbo.IncompletePurchaseOrderLineItems", "IncompletePurchaseOrderId", c => c.Guid(nullable: false));
            CreateIndex("dbo.IncompletePurchaseOrders", "CustomerId");
            CreateIndex("dbo.IncompletePurchaseOrderLineItems", "IncompletePurchaseOrderId");
            AddForeignKey("dbo.IncompletePurchaseOrders", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IncompletePurchaseOrderLineItems", "ProductId", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.IncompletePurchaseOrders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "IncompletePurchaseOrderId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "CustomerId" });
            AlterColumn("dbo.IncompletePurchaseOrderLineItems", "IncompletePurchaseOrderId", c => c.Guid());
            DropColumn("dbo.IncompletePurchaseOrders", "CustomerId");
            RenameColumn(table: "dbo.IncompletePurchaseOrderLineItems", name: "IncompletePurchaseOrderId", newName: "IncompletePurchaseOrder_Id");
            CreateIndex("dbo.IncompletePurchaseOrderLineItems", "IncompletePurchaseOrder_Id");
            AddForeignKey("dbo.IncompletePurchaseOrderLineItems", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
