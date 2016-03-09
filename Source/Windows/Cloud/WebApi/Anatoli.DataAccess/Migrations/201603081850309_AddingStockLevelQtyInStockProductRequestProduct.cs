namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingStockLevelQtyInStockProductRequestProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockProductRequestProducts", "StockLevelQty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockProductRequestProducts", "StockLevelQty");
        }
    }
}
