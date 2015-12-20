namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FialDataModelEdit3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Baskets", "Customer_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Baskets", "Customer_Id");
            AddForeignKey("dbo.Baskets", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
            DropColumn("dbo.Baskets", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Baskets", "CustomerId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Baskets", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Baskets", new[] { "Customer_Id" });
            DropColumn("dbo.Baskets", "Customer_Id");
        }
    }
}
