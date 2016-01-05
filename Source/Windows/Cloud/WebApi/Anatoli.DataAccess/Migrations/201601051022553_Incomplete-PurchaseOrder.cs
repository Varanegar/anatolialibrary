namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncompletePurchaseOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IncompletePurchaseOrders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StoreId = c.Guid(nullable: false),
                        CityRegionId = c.Guid(),
                        DeliveryTypeId = c.Guid(),
                        OrderShipAddress = c.String(),
                        Transferee = c.String(),
                        Phone = c.String(),
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
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.CityRegions", t => t.CityRegionId)
                .Index(t => t.StoreId)
                .Index(t => t.CityRegionId)
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
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        IncompletePurchaseOrder_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.IncompletePurchaseOrders", t => t.IncompletePurchaseOrder_Id)
                .Index(t => t.ProductId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.IncompletePurchaseOrder_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IncompletePurchaseOrders", "CityRegionId", "dbo.CityRegions");
            DropForeignKey("dbo.IncompletePurchaseOrders", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrders", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "IncompletePurchaseOrder_Id", "dbo.IncompletePurchaseOrders");
            DropForeignKey("dbo.IncompletePurchaseOrders", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrders", "AddedBy_Id", "dbo.Principals");
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "IncompletePurchaseOrder_Id" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "AddedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "ProductId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "AddedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "CityRegionId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "StoreId" });
            DropTable("dbo.IncompletePurchaseOrderLineItems");
            DropTable("dbo.IncompletePurchaseOrders");
        }
    }
}
