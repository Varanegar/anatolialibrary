namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockRequest6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "QtyPerPack", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.FiscalYears", "FromDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.FiscalYears", "ToDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Products", "PackUnitValueId");
            DropColumn("dbo.Products", "ProductTypeValueId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductTypeValueId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "PackUnitValueId", c => c.Int(nullable: false));
            DropColumn("dbo.FiscalYears", "ToDate");
            DropColumn("dbo.FiscalYears", "FromDate");
            DropColumn("dbo.Products", "QtyPerPack");
        }
    }
}
