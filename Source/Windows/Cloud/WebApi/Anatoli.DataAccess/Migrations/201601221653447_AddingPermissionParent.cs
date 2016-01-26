namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPermissionParent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permissions", "PersianTitle", c => c.String());
            AddColumn("dbo.Permissions", "Parent_Id", c => c.Guid());
            CreateIndex("dbo.Permissions", "Parent_Id");
            AddForeignKey("dbo.Permissions", "Parent_Id", "dbo.Permissions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permissions", "Parent_Id", "dbo.Permissions");
            DropIndex("dbo.Permissions", new[] { "Parent_Id" });
            DropColumn("dbo.Permissions", "Parent_Id");
            DropColumn("dbo.Permissions", "PersianTitle");
        }
    }
}
