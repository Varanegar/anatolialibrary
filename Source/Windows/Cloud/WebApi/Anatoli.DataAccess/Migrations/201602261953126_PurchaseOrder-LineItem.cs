namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseOrderAddress2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchaseOrderLineItems", "FinalProductId", "dbo.Products");
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "FinalProductId" });
            AlterColumn("dbo.PurchaseOrderLineItems", "FinalProductId", c => c.Guid());
            CreateIndex("dbo.PurchaseOrderLineItems", "FinalProductId");
            AddForeignKey("dbo.PurchaseOrderLineItems", "FinalProductId", "dbo.Products", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseOrderLineItems", "FinalProductId", "dbo.Products");
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "FinalProductId" });
            AlterColumn("dbo.PurchaseOrderLineItems", "FinalProductId", c => c.Guid(nullable: false));
            CreateIndex("dbo.PurchaseOrderLineItems", "FinalProductId");
            AddForeignKey("dbo.PurchaseOrderLineItems", "FinalProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
