namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockRequest17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockProductRequests", "SupplyByStockId", c => c.Guid());
            CreateIndex("dbo.StockProductRequests", "SupplyByStockId");
            AddForeignKey("dbo.StockProductRequests", "SupplyByStockId", "dbo.Stocks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockProductRequests", "SupplyByStockId", "dbo.Stocks");
            DropIndex("dbo.StockProductRequests", new[] { "SupplyByStockId" });
            DropColumn("dbo.StockProductRequests", "SupplyByStockId");
        }
    }
}
