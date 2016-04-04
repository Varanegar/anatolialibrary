namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductTagMigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductTags", "IsDefault", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductTags", "IsDefault");
        }
    }
}
