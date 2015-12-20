namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FialDataModelEdit2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasketItems", "Product_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrderLineItems", "FinalProduct_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrderLineItems", "Product_Id", c => c.Guid());
            AddColumn("dbo.Products", "MainSupplier_Id", c => c.Guid());
            AlterColumn("dbo.Customers", "Mobile", c => c.String());
            CreateIndex("dbo.BasketItems", "Product_Id");
            CreateIndex("dbo.PurchaseOrderLineItems", "FinalProduct_Id");
            CreateIndex("dbo.PurchaseOrderLineItems", "Product_Id");
            CreateIndex("dbo.Products", "MainSupplier_Id");
            AddForeignKey("dbo.Products", "MainSupplier_Id", "dbo.Suppliers", "Id");
            AddForeignKey("dbo.PurchaseOrderLineItems", "FinalProduct_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.PurchaseOrderLineItems", "Product_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.BasketItems", "Product_Id", "dbo.Products", "Id");
            DropColumn("dbo.BasketItems", "ProductId");
            DropColumn("dbo.PurchaseOrderLineItems", "ProductId");
            DropColumn("dbo.PurchaseOrderLineItems", "FinalProductId");
            DropColumn("dbo.PurchaseOrderLineItems", "ProductBaseId");
            DropColumn("dbo.Products", "MainSupplierId");
            DropColumn("dbo.Customers", "CustomerMainAppId");
            DropColumn("dbo.Customers", "Username");
            DropColumn("dbo.Customers", "Password");
            DropColumn("dbo.Customers", "ActionSourceValueId");
            DropColumn("dbo.Customers", "DeviceIMEI");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "DeviceIMEI", c => c.String());
            AddColumn("dbo.Customers", "ActionSourceValueId", c => c.Long());
            AddColumn("dbo.Customers", "Password", c => c.String());
            AddColumn("dbo.Customers", "Username", c => c.String());
            AddColumn("dbo.Customers", "CustomerMainAppId", c => c.Int());
            AddColumn("dbo.Products", "MainSupplierId", c => c.Int());
            AddColumn("dbo.PurchaseOrderLineItems", "ProductBaseId", c => c.Int());
            AddColumn("dbo.PurchaseOrderLineItems", "FinalProductId", c => c.Int(nullable: false));
            AddColumn("dbo.PurchaseOrderLineItems", "ProductId", c => c.Int());
            AddColumn("dbo.BasketItems", "ProductId", c => c.Int());
            DropForeignKey("dbo.BasketItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.PurchaseOrderLineItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.PurchaseOrderLineItems", "FinalProduct_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "MainSupplier_Id", "dbo.Suppliers");
            DropIndex("dbo.Products", new[] { "MainSupplier_Id" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "Product_Id" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "FinalProduct_Id" });
            DropIndex("dbo.BasketItems", new[] { "Product_Id" });
            AlterColumn("dbo.Customers", "Mobile", c => c.Long());
            DropColumn("dbo.Products", "MainSupplier_Id");
            DropColumn("dbo.PurchaseOrderLineItems", "Product_Id");
            DropColumn("dbo.PurchaseOrderLineItems", "FinalProduct_Id");
            DropColumn("dbo.BasketItems", "Product_Id");
        }
    }
}
