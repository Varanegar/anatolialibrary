namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReorderCalcInfo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockProductRequests", "ReorderCalcTypeId", "dbo.ReorderCalcTypes");
            DropForeignKey("dbo.StockProductRequestRules", "ReorderCalcTypeId", "dbo.ReorderCalcTypes");
            DropIndex("dbo.StockProductRequests", new[] { "ReorderCalcTypeId" });
            AddForeignKey("dbo.StockProductRequestRules", "ReorderCalcTypeId", "dbo.ReorderCalcTypes", "Id");
            DropColumn("dbo.StockProductRequests", "ReorderCalcTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockProductRequests", "ReorderCalcTypeId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.StockProductRequestRules", "ReorderCalcTypeId", "dbo.ReorderCalcTypes");
            CreateIndex("dbo.StockProductRequests", "ReorderCalcTypeId");
            AddForeignKey("dbo.StockProductRequestRules", "ReorderCalcTypeId", "dbo.ReorderCalcTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StockProductRequests", "ReorderCalcTypeId", "dbo.ReorderCalcTypes", "Id");
        }
    }
}
