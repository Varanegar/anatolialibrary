namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockProductRule2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.StockProductRequestRules", new[] { "ProductTypeId" });
            AlterColumn("dbo.StockProductRequestRules", "ProductTypeId", c => c.Guid());
            CreateIndex("dbo.StockProductRequestRules", "ProductTypeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.StockProductRequestRules", new[] { "ProductTypeId" });
            AlterColumn("dbo.StockProductRequestRules", "ProductTypeId", c => c.Guid(nullable: false));
            CreateIndex("dbo.StockProductRequestRules", "ProductTypeId");
        }
    }
}
