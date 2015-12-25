namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerInfo3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Baskets", name: "Customer_Id", newName: "CustomerId");
            RenameIndex(table: "dbo.Baskets", name: "IX_Customer_Id", newName: "IX_CustomerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Baskets", name: "IX_CustomerId", newName: "IX_Customer_Id");
            RenameColumn(table: "dbo.Baskets", name: "CustomerId", newName: "Customer_Id");
        }
    }
}
