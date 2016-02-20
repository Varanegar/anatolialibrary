namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStockRequest1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stocks", "OverRequest", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stocks", "OverAfterFirstAcceptance", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stocks", "OverAfterSecondAcceptance", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stocks", "OverAfterThirdAcceptance", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stocks", "OverAfterThirdAcceptance");
            DropColumn("dbo.Stocks", "OverAfterSecondAcceptance");
            DropColumn("dbo.Stocks", "OverAfterFirstAcceptance");
            DropColumn("dbo.Stocks", "OverRequest");
        }
    }
}
