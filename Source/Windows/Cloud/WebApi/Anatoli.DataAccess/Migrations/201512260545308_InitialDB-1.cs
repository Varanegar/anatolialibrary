namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductGroups", "CharGroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductGroups", "CharGroupId", c => c.Int(nullable: false));
        }
    }
}
