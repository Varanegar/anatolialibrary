namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Image3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemImages", "ImageName", c => c.String());
            AddColumn("dbo.ItemImages", "ImageType", c => c.String());
            DropColumn("dbo.ItemImages", "OriginalName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemImages", "OriginalName", c => c.String());
            DropColumn("dbo.ItemImages", "ImageType");
            DropColumn("dbo.ItemImages", "ImageName");
        }
    }
}
