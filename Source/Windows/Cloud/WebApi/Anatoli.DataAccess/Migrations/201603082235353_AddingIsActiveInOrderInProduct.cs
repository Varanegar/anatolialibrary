namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingIsActiveInOrderInProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsActiveInOrder", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsActiveInOrder");
        }
    }
}
