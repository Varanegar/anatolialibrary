namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerInfo4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BasketItems", name: "Product_Id", newName: "ProductId");
            RenameIndex(table: "dbo.BasketItems", name: "IX_Product_Id", newName: "IX_ProductId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BasketItems", name: "IX_ProductId", newName: "IX_Product_Id");
            RenameColumn(table: "dbo.BasketItems", name: "ProductId", newName: "Product_Id");
        }
    }
}
