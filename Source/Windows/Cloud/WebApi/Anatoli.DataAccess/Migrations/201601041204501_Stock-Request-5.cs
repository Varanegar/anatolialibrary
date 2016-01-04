namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockRequest5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FiscalYears",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FromPdate = c.String(),
                        ToPdate = c.String(),
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
            
            AddColumn("dbo.StockProducts", "IsEnable", c => c.Boolean(nullable: false));
            AddColumn("dbo.StockProducts", "FiscalYearId", c => c.Guid(nullable: false));
            AddColumn("dbo.Stocks", "Address", c => c.String());
            CreateIndex("dbo.StockProducts", "FiscalYearId");
            AddForeignKey("dbo.StockProducts", "FiscalYearId", "dbo.FiscalYears", "Id");
            DropColumn("dbo.StockProducts", "OrderType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockProducts", "OrderType", c => c.Guid(nullable: false));
            DropForeignKey("dbo.StockProducts", "FiscalYearId", "dbo.FiscalYears");
            DropForeignKey("dbo.FiscalYears", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.FiscalYears", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.FiscalYears", "AddedBy_Id", "dbo.Principals");
            DropIndex("dbo.FiscalYears", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.FiscalYears", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.FiscalYears", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProducts", new[] { "FiscalYearId" });
            DropColumn("dbo.Stocks", "Address");
            DropColumn("dbo.StockProducts", "FiscalYearId");
            DropColumn("dbo.StockProducts", "IsEnable");
            DropTable("dbo.FiscalYears");
        }
    }
}
