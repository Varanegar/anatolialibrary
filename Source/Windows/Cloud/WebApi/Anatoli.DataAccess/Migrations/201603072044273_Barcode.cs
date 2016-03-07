namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Barcode : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockProductRequests", "Accept1ById", "dbo.Principals");
            DropIndex("dbo.StockProductRequests", new[] { "Accept1ById" });
            AddColumn("dbo.Products", "Barcode", c => c.String(maxLength: 20));
            AlterColumn("dbo.Products", "ProductCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.StockProductRequests", "Accept1ById", c => c.Guid());
            CreateIndex("dbo.StockProductRequests", "Accept1ById");
            AddForeignKey("dbo.StockProductRequests", "Accept1ById", "dbo.Principals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockProductRequests", "Accept1ById", "dbo.Principals");
            DropIndex("dbo.StockProductRequests", new[] { "Accept1ById" });
            AlterColumn("dbo.StockProductRequests", "Accept1ById", c => c.Guid(nullable: false));
            AlterColumn("dbo.Products", "ProductCode", c => c.String());
            DropColumn("dbo.Products", "Barcode");
            CreateIndex("dbo.StockProductRequests", "Accept1ById");
            AddForeignKey("dbo.StockProductRequests", "Accept1ById", "dbo.Principals", "Id", cascadeDelete: true);
        }
    }
}
