namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockRequest2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockProductRequests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        RequestPDate = c.String(),
                        Accept1Date = c.DateTime(),
                        Accept1PDate = c.String(),
                        Accept2Date = c.DateTime(),
                        Accept2PDate = c.String(),
                        Accept3Date = c.DateTime(),
                        Accept3PDate = c.String(),
                        StockId = c.Guid(nullable: false),
                        ReorderCalcTypeId = c.Guid(nullable: false),
                        Accept1ById = c.Guid(nullable: false),
                        Accept2ById = c.Guid(),
                        Accept3ById = c.Guid(),
                        StockOnHandSyncId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PrivateLabelOwner_Id = c.Guid(),
                        Product_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.Accept1ById, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.Accept2ById)
                .ForeignKey("dbo.Principals", t => t.Accept3ById)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.StockOnHandSyncs", t => t.StockOnHandSyncId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId)
                .ForeignKey("dbo.ReorderCalcTypes", t => t.ReorderCalcTypeId, cascadeDelete: true)
                .Index(t => t.StockId)
                .Index(t => t.ReorderCalcTypeId)
                .Index(t => t.Accept1ById)
                .Index(t => t.Accept2ById)
                .Index(t => t.Accept3ById)
                .Index(t => t.StockOnHandSyncId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockProductRequests", "ReorderCalcTypeId", "dbo.ReorderCalcTypes");
            DropForeignKey("dbo.StockProductRequests", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockProductRequests", "StockOnHandSyncId", "dbo.StockOnHandSyncs");
            DropForeignKey("dbo.StockProductRequests", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.StockProductRequests", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequests", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequests", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequests", "Accept3ById", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequests", "Accept2ById", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequests", "Accept1ById", "dbo.Principals");
            DropIndex("dbo.StockProductRequests", new[] { "Product_Id" });
            DropIndex("dbo.StockProductRequests", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequests", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequests", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequests", new[] { "StockOnHandSyncId" });
            DropIndex("dbo.StockProductRequests", new[] { "Accept3ById" });
            DropIndex("dbo.StockProductRequests", new[] { "Accept2ById" });
            DropIndex("dbo.StockProductRequests", new[] { "Accept1ById" });
            DropIndex("dbo.StockProductRequests", new[] { "ReorderCalcTypeId" });
            DropIndex("dbo.StockProductRequests", new[] { "StockId" });
            DropTable("dbo.StockProductRequests");
        }
    }
}
