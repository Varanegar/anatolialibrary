namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockRequest9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockProducts", "ReorderCalcTypeId", "dbo.ReorderCalcTypes");
            DropIndex("dbo.StockProducts", new[] { "ReorderCalcTypeId" });
            AlterColumn("dbo.StockProducts", "ReorderCalcTypeId", c => c.Guid());
            CreateIndex("dbo.StockProducts", "ReorderCalcTypeId");
            AddForeignKey("dbo.StockProducts", "ReorderCalcTypeId", "dbo.ReorderCalcTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockProducts", "ReorderCalcTypeId", "dbo.ReorderCalcTypes");
            DropIndex("dbo.StockProducts", new[] { "ReorderCalcTypeId" });
            AlterColumn("dbo.StockProducts", "ReorderCalcTypeId", c => c.Guid(nullable: false));
            CreateIndex("dbo.StockProducts", "ReorderCalcTypeId");
            AddForeignKey("dbo.StockProducts", "ReorderCalcTypeId", "dbo.ReorderCalcTypes", "Id", cascadeDelete: true);
        }
    }
}
