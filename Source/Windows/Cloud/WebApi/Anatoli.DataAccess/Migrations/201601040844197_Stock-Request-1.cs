namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockRequest1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductTypeName = c.String(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PrivateLabelOwner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
            CreateTable(
                "dbo.StockProducts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MinQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReorderLevel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderType = c.Guid(nullable: false),
                        StockId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        ReorderCalcTypeId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PrivateLabelOwner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id)
                .ForeignKey("dbo.ReorderCalcTypes", t => t.ReorderCalcTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.StockId)
                .Index(t => t.ProductId)
                .Index(t => t.ReorderCalcTypeId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
            CreateTable(
                "dbo.ReorderCalcTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ReorderTypeName = c.String(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PrivateLabelOwner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockCode = c.Int(nullable: false),
                        StockName = c.String(),
                        StoreId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PrivateLabelOwner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
            CreateTable(
                "dbo.StockOnHandSyncs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SyncDate = c.DateTime(nullable: false),
                        SyncPDate = c.String(),
                        StockId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PrivateLabelOwner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id)
                .ForeignKey("dbo.Stocks", t => t.StockId)
                .Index(t => t.StockId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
            CreateTable(
                "dbo.StockActiveOnHands",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        StockOnHandSyncId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PrivateLabelOwner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .ForeignKey("dbo.StockOnHandSyncs", t => t.StockOnHandSyncId, cascadeDelete: true)
                .Index(t => t.StockId)
                .Index(t => t.ProductId)
                .Index(t => t.StockOnHandSyncId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
            CreateTable(
                "dbo.StockHistoryOnHands",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        StockOnHandSyncId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PrivateLabelOwner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .ForeignKey("dbo.StockOnHandSyncs", t => t.StockOnHandSyncId, cascadeDelete: true)
                .Index(t => t.StockId)
                .Index(t => t.ProductId)
                .Index(t => t.StockOnHandSyncId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
            AddColumn("dbo.Products", "ProductTypeId", c => c.Guid());
            CreateIndex("dbo.Products", "ProductTypeId");
            AddForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.StockProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockProducts", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockOnHandSyncs", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockHistoryOnHands", "StockOnHandSyncId", "dbo.StockOnHandSyncs");
            DropForeignKey("dbo.StockHistoryOnHands", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockHistoryOnHands", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockHistoryOnHands", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockHistoryOnHands", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockHistoryOnHands", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockActiveOnHands", "StockOnHandSyncId", "dbo.StockOnHandSyncs");
            DropForeignKey("dbo.StockActiveOnHands", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockActiveOnHands", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockActiveOnHands", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockActiveOnHands", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockActiveOnHands", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockOnHandSyncs", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockOnHandSyncs", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockOnHandSyncs", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Stocks", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Stocks", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Stocks", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProducts", "ReorderCalcTypeId", "dbo.ReorderCalcTypes");
            DropForeignKey("dbo.ReorderCalcTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ReorderCalcTypes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ReorderCalcTypes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProducts", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProducts", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProducts", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.ProductTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductTypes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductTypes", "AddedBy_Id", "dbo.Principals");
            DropIndex("dbo.StockHistoryOnHands", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "StockOnHandSyncId" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "ProductId" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "StockId" });
            DropIndex("dbo.StockActiveOnHands", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockActiveOnHands", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockActiveOnHands", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockActiveOnHands", new[] { "StockOnHandSyncId" });
            DropIndex("dbo.StockActiveOnHands", new[] { "ProductId" });
            DropIndex("dbo.StockActiveOnHands", new[] { "StockId" });
            DropIndex("dbo.StockOnHandSyncs", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockOnHandSyncs", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockOnHandSyncs", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockOnHandSyncs", new[] { "StockId" });
            DropIndex("dbo.Stocks", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Stocks", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Stocks", new[] { "AddedBy_Id" });
            DropIndex("dbo.Stocks", new[] { "StoreId" });
            DropIndex("dbo.ReorderCalcTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ReorderCalcTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ReorderCalcTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProducts", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProducts", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProducts", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProducts", new[] { "ReorderCalcTypeId" });
            DropIndex("dbo.StockProducts", new[] { "ProductId" });
            DropIndex("dbo.StockProducts", new[] { "StockId" });
            DropIndex("dbo.ProductTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ProductTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.Products", new[] { "ProductTypeId" });
            DropColumn("dbo.Products", "ProductTypeId");
            DropTable("dbo.StockHistoryOnHands");
            DropTable("dbo.StockActiveOnHands");
            DropTable("dbo.StockOnHandSyncs");
            DropTable("dbo.Stocks");
            DropTable("dbo.ReorderCalcTypes");
            DropTable("dbo.StockProducts");
            DropTable("dbo.ProductTypes");
        }
    }
}
