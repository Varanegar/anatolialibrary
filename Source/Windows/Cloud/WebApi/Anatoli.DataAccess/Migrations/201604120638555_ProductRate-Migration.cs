namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductRateMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductRate", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ProductRate");
        }
    }
}
