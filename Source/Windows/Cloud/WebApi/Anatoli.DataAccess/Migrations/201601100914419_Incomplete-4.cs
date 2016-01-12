namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Incomplete4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IncompletePurchaseOrders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StoreId = c.Guid(nullable: false),
                        CityRegionId = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        DeliveryTypeId = c.Guid(nullable: false),
                        PaymentTypeId = c.Guid(),
                        OrderShipAddress = c.String(),
                        Transferee = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 20),
                        DeliveryFromTime = c.DateTime(),
                        DeliveryToTime = c.DateTime(),
                        DeliveryDate = c.DateTime(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.CityRegions", t => t.CityRegionId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId)
                .Index(t => t.CityRegionId)
                .Index(t => t.CustomerId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.IncompletePurchaseOrderLineItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Guid(nullable: false),
                        IncompletePurchaseOrderId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.IncompletePurchaseOrders", t => t.IncompletePurchaseOrderId, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: false)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.IncompletePurchaseOrderId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IncompletePurchaseOrders", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.IncompletePurchaseOrders", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrders", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "IncompletePurchaseOrderId", "dbo.IncompletePurchaseOrders");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.IncompletePurchaseOrders", "CityRegionId", "dbo.CityRegions");
            DropForeignKey("dbo.IncompletePurchaseOrders", "AddedBy_Id", "dbo.Principals");
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "AddedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "IncompletePurchaseOrderId" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "ProductId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "AddedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "CustomerId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "CityRegionId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "StoreId" });
            DropTable("dbo.IncompletePurchaseOrderLineItems");
            DropTable("dbo.IncompletePurchaseOrders");
        }
    }
}
