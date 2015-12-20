namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FialDataModelEdit1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "MainAppProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "MainAppProductId", c => c.Int(nullable: false));
        }
    }
}
