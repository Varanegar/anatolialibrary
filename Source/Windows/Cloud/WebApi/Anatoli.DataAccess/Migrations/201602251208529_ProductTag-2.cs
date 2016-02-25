namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductTag2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductTags", "ProductTagName", c => c.String(maxLength: 100));
            DropColumn("dbo.ProductTags", "TagName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductTags", "TagName", c => c.String(maxLength: 100));
            DropColumn("dbo.ProductTags", "ProductTagName");
        }
    }
}
