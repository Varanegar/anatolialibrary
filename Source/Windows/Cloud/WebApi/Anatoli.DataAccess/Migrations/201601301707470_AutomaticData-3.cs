namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutomaticData3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "PrivateLabelOwner_Id", "dbo.Principals");
            DropIndex("dbo.Users", new[] { "PrivateLabelOwner_Id" });
            AlterColumn("dbo.Users", "PrivateLabelOwner_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Users", "PrivateLabelOwner_Id");
            AddForeignKey("dbo.Users", "PrivateLabelOwner_Id", "dbo.Principals", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "PrivateLabelOwner_Id", "dbo.Principals");
            DropIndex("dbo.Users", new[] { "PrivateLabelOwner_Id" });
            AlterColumn("dbo.Users", "PrivateLabelOwner_Id", c => c.Guid());
            CreateIndex("dbo.Users", "PrivateLabelOwner_Id");
            AddForeignKey("dbo.Users", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
        }
    }
}
