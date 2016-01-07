namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockrequest14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockProductRequestStatus", "StockProductRequestStatusName", c => c.String());
            DropColumn("dbo.StockProductRequestStatus", "StockPorductRequestTypeName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockProductRequestStatus", "StockPorductRequestTypeName", c => c.String());
            DropColumn("dbo.StockProductRequestStatus", "StockProductRequestStatusName");
        }
    }
}
