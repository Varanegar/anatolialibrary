namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BaseValues", "BaseType_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.BaseValues", "BaseType_Id");
            AddForeignKey("dbo.BaseValues", "BaseType_Id", "dbo.BaseTypes", "Id", cascadeDelete: true);
            DropColumn("dbo.BaseValues", "BaseTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BaseValues", "BaseTypeId", c => c.Long(nullable: false));
            DropForeignKey("dbo.BaseValues", "BaseType_Id", "dbo.BaseTypes");
            DropIndex("dbo.BaseValues", new[] { "BaseType_Id" });
            DropColumn("dbo.BaseValues", "BaseType_Id");
        }
    }
}
