namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockRequest3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockProductRequests", "Product_Id", "dbo.Products");
            DropIndex("dbo.StockProductRequests", new[] { "Product_Id" });
            CreateTable(
                "dbo.StockTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockTypeName = c.String(),
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
                "dbo.StockProductRequestProducts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RequestQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Accepted1Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Accepted2Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Accepted3Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeliveredQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Guid(nullable: false),
                        StockProductRequestId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.StockProductRequests", t => t.StockProductRequestId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.StockProductRequestId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
            CreateTable(
                "dbo.StockProductRequestStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockPorductRequestTypeName = c.String(),
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
                "dbo.StockProductRequestTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockPorductRequestTypeName = c.String(),
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
            
            AddColumn("dbo.StockProductRequests", "SendtoSourceStockDate", c => c.DateTime());
            AddColumn("dbo.StockProductRequests", "SendtoSourceStockDatePDate", c => c.String());
            AddColumn("dbo.StockProductRequests", "SrouceStockRequestId", c => c.Guid());
            AddColumn("dbo.StockProductRequests", "SrouceStockRequestNo", c => c.String());
            AddColumn("dbo.StockProductRequests", "TargetStockIssueDate", c => c.DateTime());
            AddColumn("dbo.StockProductRequests", "TargetStockIssueDatePDate", c => c.String());
            AddColumn("dbo.StockProductRequests", "TargetStockPaperId", c => c.Guid());
            AddColumn("dbo.StockProductRequests", "TargetStockPaperNo", c => c.String());
            AddColumn("dbo.StockProductRequests", "StockProductRequestStatusId", c => c.Guid(nullable: false));
            AddColumn("dbo.StockProductRequests", "StockTypeId", c => c.Guid(nullable: false));
            AddColumn("dbo.StockProductRequests", "SupplierId", c => c.Guid());
            AddColumn("dbo.StockProductRequests", "StockProductRequestTypeId", c => c.Guid(nullable: false));
            AddColumn("dbo.StockProductRequests", "PorductTypeId", c => c.Guid(nullable: false));
            AddColumn("dbo.Stocks", "Accept1ById", c => c.Guid(nullable: false));
            AddColumn("dbo.Stocks", "Accept2ById", c => c.Guid());
            AddColumn("dbo.Stocks", "Accept3ById", c => c.Guid());
            AddColumn("dbo.Stocks", "StockTypeId", c => c.Guid());
            CreateIndex("dbo.StockProductRequests", "StockProductRequestStatusId");
            CreateIndex("dbo.StockProductRequests", "StockTypeId");
            CreateIndex("dbo.StockProductRequests", "SupplierId");
            CreateIndex("dbo.StockProductRequests", "StockProductRequestTypeId");
            CreateIndex("dbo.StockProductRequests", "PorductTypeId");
            CreateIndex("dbo.Stocks", "Accept1ById");
            CreateIndex("dbo.Stocks", "Accept2ById");
            CreateIndex("dbo.Stocks", "Accept3ById");
            CreateIndex("dbo.Stocks", "StockTypeId");
            AddForeignKey("dbo.Stocks", "Accept1ById", "dbo.Principals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Stocks", "Accept2ById", "dbo.Principals", "Id");
            AddForeignKey("dbo.Stocks", "Accept3ById", "dbo.Principals", "Id");
            AddForeignKey("dbo.Stocks", "StockTypeId", "dbo.StockTypes", "Id");
            AddForeignKey("dbo.StockProductRequests", "StockProductRequestStatusId", "dbo.StockProductRequestStatus", "Id");
            AddForeignKey("dbo.StockProductRequests", "StockProductRequestTypeId", "dbo.StockProductRequestTypes", "Id");
            AddForeignKey("dbo.StockProductRequests", "StockTypeId", "dbo.StockTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StockProductRequests", "SupplierId", "dbo.Suppliers", "Id");
            AddForeignKey("dbo.StockProductRequests", "PorductTypeId", "dbo.ProductTypes", "Id");
            DropColumn("dbo.StockProductRequests", "Product_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockProductRequests", "Product_Id", c => c.Guid());
            DropForeignKey("dbo.StockProductRequests", "PorductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.StockProductRequests", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.StockProductRequests", "StockTypeId", "dbo.StockTypes");
            DropForeignKey("dbo.StockProductRequests", "StockProductRequestTypeId", "dbo.StockProductRequestTypes");
            DropForeignKey("dbo.StockProductRequestTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestTypes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestTypes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequests", "StockProductRequestStatusId", "dbo.StockProductRequestStatus");
            DropForeignKey("dbo.StockProductRequestStatus", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestStatus", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestStatus", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestProducts", "StockProductRequestId", "dbo.StockProductRequests");
            DropForeignKey("dbo.StockProductRequestProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockProductRequestProducts", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestProducts", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestProducts", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Stocks", "StockTypeId", "dbo.StockTypes");
            DropForeignKey("dbo.StockTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockTypes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockTypes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Stocks", "Accept3ById", "dbo.Principals");
            DropForeignKey("dbo.Stocks", "Accept2ById", "dbo.Principals");
            DropForeignKey("dbo.Stocks", "Accept1ById", "dbo.Principals");
            DropIndex("dbo.StockProductRequestTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequestTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestStatus", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequestStatus", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestStatus", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "StockProductRequestId" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "ProductId" });
            DropIndex("dbo.StockTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.Stocks", new[] { "StockTypeId" });
            DropIndex("dbo.Stocks", new[] { "Accept3ById" });
            DropIndex("dbo.Stocks", new[] { "Accept2ById" });
            DropIndex("dbo.Stocks", new[] { "Accept1ById" });
            DropIndex("dbo.StockProductRequests", new[] { "PorductTypeId" });
            DropIndex("dbo.StockProductRequests", new[] { "StockProductRequestTypeId" });
            DropIndex("dbo.StockProductRequests", new[] { "SupplierId" });
            DropIndex("dbo.StockProductRequests", new[] { "StockTypeId" });
            DropIndex("dbo.StockProductRequests", new[] { "StockProductRequestStatusId" });
            DropColumn("dbo.Stocks", "StockTypeId");
            DropColumn("dbo.Stocks", "Accept3ById");
            DropColumn("dbo.Stocks", "Accept2ById");
            DropColumn("dbo.Stocks", "Accept1ById");
            DropColumn("dbo.StockProductRequests", "PorductTypeId");
            DropColumn("dbo.StockProductRequests", "StockProductRequestTypeId");
            DropColumn("dbo.StockProductRequests", "SupplierId");
            DropColumn("dbo.StockProductRequests", "StockTypeId");
            DropColumn("dbo.StockProductRequests", "StockProductRequestStatusId");
            DropColumn("dbo.StockProductRequests", "TargetStockPaperNo");
            DropColumn("dbo.StockProductRequests", "TargetStockPaperId");
            DropColumn("dbo.StockProductRequests", "TargetStockIssueDatePDate");
            DropColumn("dbo.StockProductRequests", "TargetStockIssueDate");
            DropColumn("dbo.StockProductRequests", "SrouceStockRequestNo");
            DropColumn("dbo.StockProductRequests", "SrouceStockRequestId");
            DropColumn("dbo.StockProductRequests", "SendtoSourceStockDatePDate");
            DropColumn("dbo.StockProductRequests", "SendtoSourceStockDate");
            DropTable("dbo.StockProductRequestTypes");
            DropTable("dbo.StockProductRequestStatus");
            DropTable("dbo.StockProductRequestProducts");
            DropTable("dbo.StockTypes");
            CreateIndex("dbo.StockProductRequests", "Product_Id");
            AddForeignKey("dbo.StockProductRequests", "Product_Id", "dbo.Products", "Id");
        }
    }
}
