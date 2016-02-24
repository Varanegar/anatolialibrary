namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StorePhone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stores", "Phone", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stores", "Phone");
        }
    }
}
