namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Image2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Images", newName: "ItemImages");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ItemImages", newName: "Images");
        }
    }
}
