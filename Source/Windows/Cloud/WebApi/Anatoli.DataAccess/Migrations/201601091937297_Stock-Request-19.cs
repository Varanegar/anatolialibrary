namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockRequest19 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "QtyPerPack", c => c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue: 1));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "QtyPerPack", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
