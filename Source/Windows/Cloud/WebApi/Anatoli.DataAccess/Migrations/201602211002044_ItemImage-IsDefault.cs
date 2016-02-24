namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemImageIsDefault : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemImages", "IsDefault", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemImages", "IsDefault");
        }
    }
}
