namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PermissionMigration7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserNameStr", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "DataOwnerId", c => c.Guid(nullable: true));
            CreateIndex("dbo.Users", "DataOwnerId");
            AddForeignKey("dbo.Users", "DataOwnerId", "dbo.DataOwners", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "DataOwnerId", "dbo.DataOwners");
            DropIndex("dbo.Users", new[] { "DataOwnerId" });
            DropColumn("dbo.Users", "DataOwnerId");
            DropColumn("dbo.Users", "UserNameStr");
        }
    }
}
