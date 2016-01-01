namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductRate : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProductRates", name: "Product_Id", newName: "ProductId");
            RenameIndex(table: "dbo.ProductRates", name: "IX_Product_Id", newName: "IX_ProductId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProductRates", name: "IX_ProductId", newName: "IX_Product_Id");
            RenameColumn(table: "dbo.ProductRates", name: "ProductId", newName: "Product_Id");
        }
    }
}
