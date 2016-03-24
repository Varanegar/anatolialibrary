namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBusinessModelMigration1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "ApplicationId", "dbo.Applications");
            DropIndex("dbo.Users", new[] { "ApplicationId" });
            RenameColumn(table: "dbo.Users", name: "ApplicationId", newName: "Application_Id");
            AlterColumn("dbo.Users", "Application_Id", c => c.Guid());
            CreateIndex("dbo.Users", "Application_Id");
            AddForeignKey("dbo.Users", "Application_Id", "dbo.Applications", "Id");
            DropColumn("dbo.Users", "Number_ID");
            DropColumn("dbo.Users", "IsRemoved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "Number_ID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Users", "Application_Id", "dbo.Applications");
            DropIndex("dbo.Users", new[] { "Application_Id" });
            AlterColumn("dbo.Users", "Application_Id", c => c.Guid(nullable: false));
            RenameColumn(table: "dbo.Users", name: "Application_Id", newName: "ApplicationId");
            CreateIndex("dbo.Users", "ApplicationId");
            AddForeignKey("dbo.Users", "ApplicationId", "dbo.Applications", "Id", cascadeDelete: true);
        }
    }
}
