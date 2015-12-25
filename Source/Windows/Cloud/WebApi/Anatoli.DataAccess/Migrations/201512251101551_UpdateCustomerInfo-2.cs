namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerInfo2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BasketItems", name: "Basket_Id", newName: "BasketId");
            RenameIndex(table: "dbo.BasketItems", name: "IX_Basket_Id", newName: "IX_BasketId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BasketItems", name: "IX_BasketId", newName: "IX_Basket_Id");
            RenameColumn(table: "dbo.BasketItems", name: "BasketId", newName: "Basket_Id");
        }
    }
}
