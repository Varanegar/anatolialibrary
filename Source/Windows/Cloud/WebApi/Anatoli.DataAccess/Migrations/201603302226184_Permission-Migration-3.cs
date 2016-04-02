namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PermissionMigration3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Principal_Id", c => c.Guid(nullable: true));
            AddColumn("dbo.Principals", "ApplicationOwnerId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Users", "Principal_Id");
            CreateIndex("dbo.Principals", "ApplicationOwnerId");
            CreateIndex("dbo.Groups", "Id");
            AddForeignKey("dbo.Principals", "ApplicationOwnerId", "dbo.ApplicationOwners", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Groups", "Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Users", "Principal_Id", "dbo.Principals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Principal_Id", "dbo.Principals");
            DropForeignKey("dbo.Groups", "Id", "dbo.Principals");
            DropForeignKey("dbo.Principals", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropIndex("dbo.Groups", new[] { "Id" });
            DropIndex("dbo.Principals", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.Users", new[] { "Principal_Id" });
            DropColumn("dbo.Principals", "ApplicationOwnerId");
            DropColumn("dbo.Users", "Principal_Id");
        }
    }
}
