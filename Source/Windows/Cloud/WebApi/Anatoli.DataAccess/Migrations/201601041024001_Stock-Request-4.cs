namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockRequest4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductComments", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductRates", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PurchaseOrderLineItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.StockActiveOnHands", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockHistoryOnHands", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockProductRequestProducts", "ProductId", "dbo.Products");
            CreateTable(
                "dbo.StockProductRequestRules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockProductRequestRuleName = c.String(),
                        FromDate = c.DateTime(nullable: false),
                        FromPDate = c.String(),
                        FromTime = c.Time(nullable: false, precision: 7),
                        ToDate = c.DateTime(nullable: false),
                        ToPDate = c.String(),
                        ToTime = c.Time(nullable: false, precision: 7),
                        ProductId = c.Guid(nullable: false),
                        ProductGroupId = c.Guid(),
                        ProductTypeId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeId)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.ProductGroupId)
                .Index(t => t.ProductTypeId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
            CreateTable(
                "dbo.StockProductRequestProductDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RequestQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockProductRequestProductId = c.Guid(nullable: false),
                        StockProductRequestRuleId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.StockProductRequestProducts", t => t.StockProductRequestProductId, cascadeDelete: true)
                .ForeignKey("dbo.StockProductRequestRules", t => t.StockProductRequestRuleId, cascadeDelete: true)
                .Index(t => t.StockProductRequestProductId)
                .Index(t => t.StockProductRequestRuleId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
            AddForeignKey("dbo.ProductComments", "Product_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.ProductRates", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.PurchaseOrderLineItems", "Product_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.StockActiveOnHands", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.StockHistoryOnHands", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.StockProductRequestProducts", "ProductId", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockProductRequestProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockHistoryOnHands", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockActiveOnHands", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PurchaseOrderLineItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductRates", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductComments", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.StockProductRequestRules", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockProductRequestRules", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.StockProductRequestProductDetails", "StockProductRequestRuleId", "dbo.StockProductRequestRules");
            DropForeignKey("dbo.StockProductRequestProductDetails", "StockProductRequestProductId", "dbo.StockProductRequestProducts");
            DropForeignKey("dbo.StockProductRequestProductDetails", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestProductDetails", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestProductDetails", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestRules", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.StockProductRequestRules", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestRules", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestRules", "AddedBy_Id", "dbo.Principals");
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "StockProductRequestRuleId" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "StockProductRequestProductId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequestRules", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestRules", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestRules", new[] { "ProductTypeId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "ProductGroupId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "ProductId" });
            DropTable("dbo.StockProductRequestProductDetails");
            DropTable("dbo.StockProductRequestRules");
            AddForeignKey("dbo.StockProductRequestProducts", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StockHistoryOnHands", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StockActiveOnHands", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PurchaseOrderLineItems", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductRates", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductComments", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
