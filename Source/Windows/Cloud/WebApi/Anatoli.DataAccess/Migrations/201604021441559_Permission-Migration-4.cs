namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PermissionMigration4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationModuleResources", "ParentId", "dbo.ApplicationModuleResources");
            DropForeignKey("dbo.PermissionActions", "ParentId", "dbo.PermissionActions");
            DropIndex("dbo.ApplicationModuleResources", new[] { "ParentId" });
            DropIndex("dbo.PermissionActions", new[] { "ParentId" });
            DropColumn("dbo.ApplicationModuleResources", "ParentId");
            DropColumn("dbo.ApplicationModuleResources", "NodeId");
            DropColumn("dbo.ApplicationModuleResources", "NLeft");
            DropColumn("dbo.ApplicationModuleResources", "NRight");
            DropColumn("dbo.ApplicationModuleResources", "NLevel");
            DropColumn("dbo.ApplicationModuleResources", "Priority");
            DropColumn("dbo.PermissionActions", "ParentId");
            DropColumn("dbo.PermissionActions", "NodeId");
            DropColumn("dbo.PermissionActions", "NLeft");
            DropColumn("dbo.PermissionActions", "NRight");
            DropColumn("dbo.PermissionActions", "NLevel");
            DropColumn("dbo.PermissionActions", "Priority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PermissionActions", "Priority", c => c.Int());
            AddColumn("dbo.PermissionActions", "NLevel", c => c.Int(nullable: false));
            AddColumn("dbo.PermissionActions", "NRight", c => c.Int(nullable: false));
            AddColumn("dbo.PermissionActions", "NLeft", c => c.Int(nullable: false));
            AddColumn("dbo.PermissionActions", "NodeId", c => c.Int(nullable: false));
            AddColumn("dbo.PermissionActions", "ParentId", c => c.Guid());
            AddColumn("dbo.ApplicationModuleResources", "Priority", c => c.Int());
            AddColumn("dbo.ApplicationModuleResources", "NLevel", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationModuleResources", "NRight", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationModuleResources", "NLeft", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationModuleResources", "NodeId", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationModuleResources", "ParentId", c => c.Guid());
            CreateIndex("dbo.PermissionActions", "ParentId");
            CreateIndex("dbo.ApplicationModuleResources", "ParentId");
            AddForeignKey("dbo.PermissionActions", "ParentId", "dbo.PermissionActions", "Id");
            AddForeignKey("dbo.ApplicationModuleResources", "ParentId", "dbo.ApplicationModuleResources", "Id");
        }
    }
}
