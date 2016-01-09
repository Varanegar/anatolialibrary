namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockProductRule4 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.StockProductRequestRules", new[] { "ProductId" });
            AlterColumn("dbo.StockProductRequestRules", "ProductId", c => c.Guid());
            CreateIndex("dbo.StockProductRequestRules", "ProductId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.StockProductRequestRules", new[] { "ProductId" });
            AlterColumn("dbo.StockProductRequestRules", "ProductId", c => c.Guid(nullable: false));
            CreateIndex("dbo.StockProductRequestRules", "ProductId");
        }
    }
}
