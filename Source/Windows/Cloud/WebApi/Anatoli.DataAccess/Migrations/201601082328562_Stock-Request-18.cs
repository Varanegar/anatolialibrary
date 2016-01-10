namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockRequest18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockProductRequests", "SourceStockRequestId", c => c.Guid());
            AddColumn("dbo.StockProductRequests", "SourceStockRequestNo", c => c.String(maxLength: 50));
            DropColumn("dbo.StockProductRequests", "SrouceStockRequestId");
            DropColumn("dbo.StockProductRequests", "SrouceStockRequestNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockProductRequests", "SrouceStockRequestNo", c => c.String(maxLength: 50));
            AddColumn("dbo.StockProductRequests", "SrouceStockRequestId", c => c.Guid());
            DropColumn("dbo.StockProductRequests", "SourceStockRequestNo");
            DropColumn("dbo.StockProductRequests", "SourceStockRequestId");
        }
    }
}
