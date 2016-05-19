namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BrandMigration1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "ProductId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "StoreId" });
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BrandName = c.String(maxLength: 100),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            AddColumn("dbo.Products", "BrandId", c => c.Guid());
            AlterColumn("dbo.IncompletePurchaseOrderLineItems", "ProductId", c => c.Guid());
            AlterColumn("dbo.IncompletePurchaseOrders", "StoreId", c => c.Guid());
            AlterColumn("dbo.IncompletePurchaseOrders", "DeliveryTypeId", c => c.Guid());
            CreateIndex("dbo.Products", "BrandId");
            CreateIndex("dbo.IncompletePurchaseOrderLineItems", "ProductId");
            CreateIndex("dbo.IncompletePurchaseOrders", "StoreId");
            AddForeignKey("dbo.Products", "BrandId", "dbo.Brands", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Brands", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.Brands", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Brands", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.Brands", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Brands", "AddedById", "dbo.Principals");
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "StoreId" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "ProductId" });
            DropIndex("dbo.Brands", new[] { "LastModifiedById" });
            DropIndex("dbo.Brands", new[] { "AddedById" });
            DropIndex("dbo.Brands", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Brands", new[] { "DataOwnerId" });
            DropIndex("dbo.Brands", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            AlterColumn("dbo.IncompletePurchaseOrders", "DeliveryTypeId", c => c.Guid(nullable: false));
            AlterColumn("dbo.IncompletePurchaseOrders", "StoreId", c => c.Guid(nullable: false));
            AlterColumn("dbo.IncompletePurchaseOrderLineItems", "ProductId", c => c.Guid(nullable: false));
            DropColumn("dbo.Products", "BrandId");
            DropTable("dbo.Brands");
            CreateIndex("dbo.IncompletePurchaseOrders", "StoreId");
            CreateIndex("dbo.IncompletePurchaseOrderLineItems", "ProductId");
        }
    }
}
