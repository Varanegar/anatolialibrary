namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Incomplete5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IncompletePurchaseOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.IncompletePurchaseOrders", "CityRegionId", "dbo.CityRegions");
            DropForeignKey("dbo.IncompletePurchaseOrders", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "ProductId", "dbo.Products");
            AddForeignKey("dbo.IncompletePurchaseOrders", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.IncompletePurchaseOrders", "CityRegionId", "dbo.CityRegions", "Id");
            AddForeignKey("dbo.IncompletePurchaseOrders", "StoreId", "dbo.Stores", "Id");
            AddForeignKey("dbo.IncompletePurchaseOrderLineItems", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.IncompletePurchaseOrders", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.IncompletePurchaseOrders", "CityRegionId", "dbo.CityRegions");
            DropForeignKey("dbo.IncompletePurchaseOrders", "CustomerId", "dbo.Customers");
            AddForeignKey("dbo.IncompletePurchaseOrderLineItems", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IncompletePurchaseOrders", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IncompletePurchaseOrders", "CityRegionId", "dbo.CityRegions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IncompletePurchaseOrders", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
