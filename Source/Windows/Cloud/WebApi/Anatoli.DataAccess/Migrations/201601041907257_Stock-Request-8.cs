namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockRequest8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stocks", "Accept1ById", "dbo.Principals");
            DropIndex("dbo.Stocks", new[] { "Accept1ById" });
            AlterColumn("dbo.Stocks", "Accept1ById", c => c.Guid());
            CreateIndex("dbo.Stocks", "Accept1ById");
            AddForeignKey("dbo.Stocks", "Accept1ById", "dbo.Principals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "Accept1ById", "dbo.Principals");
            DropIndex("dbo.Stocks", new[] { "Accept1ById" });
            AlterColumn("dbo.Stocks", "Accept1ById", c => c.Guid(nullable: false));
            CreateIndex("dbo.Stocks", "Accept1ById");
            AddForeignKey("dbo.Stocks", "Accept1ById", "dbo.Principals", "Id", cascadeDelete: true);
        }
    }
}
