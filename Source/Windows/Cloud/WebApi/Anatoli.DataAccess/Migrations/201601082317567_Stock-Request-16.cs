namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockRequest16 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockProductRequestSupplyTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockProductRequestSupplyTypeName = c.String(maxLength: 100),
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
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            AddColumn("dbo.StockProductRequests", "StockProductRequestSupplyTypeId", c => c.Guid(nullable: false));
            AddColumn("dbo.StockProducts", "StockProductRequestSupplyTypeId", c => c.Guid());
            AddColumn("dbo.StockProducts", "StockProductRequestType_Id", c => c.Guid());
            AddColumn("dbo.Stocks", "MainSCMStock2Id", c => c.Guid());
            AddColumn("dbo.Stocks", "RelatedSCMStock2Id", c => c.Guid());
            CreateIndex("dbo.StockProductRequests", "StockProductRequestSupplyTypeId");
            CreateIndex("dbo.StockProducts", "StockProductRequestSupplyTypeId");
            CreateIndex("dbo.StockProducts", "StockProductRequestType_Id");
            CreateIndex("dbo.Stocks", "MainSCMStock2Id");
            CreateIndex("dbo.Stocks", "RelatedSCMStock2Id");
            AddForeignKey("dbo.Stocks", "MainSCMStock2Id", "dbo.Stocks", "Id");
            AddForeignKey("dbo.Stocks", "RelatedSCMStock2Id", "dbo.Stocks", "Id");
            AddForeignKey("dbo.StockProductRequests", "StockProductRequestSupplyTypeId", "dbo.StockProductRequestSupplyTypes", "Id");
            AddForeignKey("dbo.StockProducts", "StockProductRequestSupplyTypeId", "dbo.StockProductRequestSupplyTypes", "Id");
            AddForeignKey("dbo.StockProducts", "StockProductRequestType_Id", "dbo.StockProductRequestTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockProducts", "StockProductRequestType_Id", "dbo.StockProductRequestTypes");
            DropForeignKey("dbo.StockProducts", "StockProductRequestSupplyTypeId", "dbo.StockProductRequestSupplyTypes");
            DropForeignKey("dbo.StockProductRequests", "StockProductRequestSupplyTypeId", "dbo.StockProductRequestSupplyTypes");
            DropForeignKey("dbo.StockProductRequestSupplyTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestSupplyTypes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestSupplyTypes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Stocks", "RelatedSCMStock2Id", "dbo.Stocks");
            DropForeignKey("dbo.Stocks", "MainSCMStock2Id", "dbo.Stocks");
            DropIndex("dbo.StockProductRequestSupplyTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestSupplyTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestSupplyTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Stocks", new[] { "RelatedSCMStock2Id" });
            DropIndex("dbo.Stocks", new[] { "MainSCMStock2Id" });
            DropIndex("dbo.StockProducts", new[] { "StockProductRequestType_Id" });
            DropIndex("dbo.StockProducts", new[] { "StockProductRequestSupplyTypeId" });
            DropIndex("dbo.StockProductRequests", new[] { "StockProductRequestSupplyTypeId" });
            DropColumn("dbo.Stocks", "RelatedSCMStock2Id");
            DropColumn("dbo.Stocks", "MainSCMStock2Id");
            DropColumn("dbo.StockProducts", "StockProductRequestType_Id");
            DropColumn("dbo.StockProducts", "StockProductRequestSupplyTypeId");
            DropColumn("dbo.StockProductRequests", "StockProductRequestSupplyTypeId");
            DropTable("dbo.StockProductRequestSupplyTypes");
        }
    }
}
