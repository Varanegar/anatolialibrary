namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PermissionMigration6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "AnatoliContactId", "dbo.AnatoliContacts");
            DropIndex("dbo.Users", new[] { "AnatoliContactId" });
            AlterColumn("dbo.Users", "AnatoliContactId", c => c.Guid());
            CreateIndex("dbo.Users", "AnatoliContactId");
            AddForeignKey("dbo.Users", "AnatoliContactId", "dbo.AnatoliContacts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "AnatoliContactId", "dbo.AnatoliContacts");
            DropIndex("dbo.Users", new[] { "AnatoliContactId" });
            AlterColumn("dbo.Users", "AnatoliContactId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Users", "AnatoliContactId");
            AddForeignKey("dbo.Users", "AnatoliContactId", "dbo.AnatoliContacts", "Id", cascadeDelete: true);
        }
    }
}
