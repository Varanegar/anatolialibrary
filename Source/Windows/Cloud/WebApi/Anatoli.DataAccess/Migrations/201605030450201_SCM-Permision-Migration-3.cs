namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SCMPermisionMigration3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrincipalStocks", "PrincipalStock_Id", "dbo.PrincipalStocks");
            DropForeignKey("dbo.PrincipalStocks", "Stock_Id", "dbo.Stocks");
            DropIndex("dbo.PrincipalStocks", new[] { "PrincipalStock_Id" });
            DropIndex("dbo.PrincipalStocks", new[] { "Stock_Id" });
            RenameColumn(table: "dbo.PrincipalStocks", name: "Stock_Id", newName: "StockId");
            AlterColumn("dbo.PrincipalStocks", "StockId", c => c.Guid(nullable: false));
            CreateIndex("dbo.PrincipalStocks", "StockId");
            AddForeignKey("dbo.PrincipalStocks", "StockId", "dbo.Stocks", "Id", cascadeDelete: true);
            DropColumn("dbo.PrincipalStocks", "PrincipalStock_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrincipalStocks", "PrincipalStock_Id", c => c.Guid());
            DropForeignKey("dbo.PrincipalStocks", "StockId", "dbo.Stocks");
            DropIndex("dbo.PrincipalStocks", new[] { "StockId" });
            AlterColumn("dbo.PrincipalStocks", "StockId", c => c.Guid());
            RenameColumn(table: "dbo.PrincipalStocks", name: "StockId", newName: "Stock_Id");
            CreateIndex("dbo.PrincipalStocks", "Stock_Id");
            CreateIndex("dbo.PrincipalStocks", "PrincipalStock_Id");
            AddForeignKey("dbo.PrincipalStocks", "Stock_Id", "dbo.Stocks", "Id");
            AddForeignKey("dbo.PrincipalStocks", "PrincipalStock_Id", "dbo.PrincipalStocks", "Id");
        }
    }
}
