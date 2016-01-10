namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpellCheck1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockProductRequestTypes", "StockProductRequestTypeName", c => c.String());
            DropColumn("dbo.StockProductRequestTypes", "StockPorductRequestTypeName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockProductRequestTypes", "StockPorductRequestTypeName", c => c.String());
            DropColumn("dbo.StockProductRequestTypes", "StockProductRequestTypeName");
        }
    }
}
