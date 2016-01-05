namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockrequest12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Suppliers", "OrderAllProduct", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Suppliers", "OrderAllProduct");
        }
    }
}
