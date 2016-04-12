namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPermissionCatalog12 : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Users", "AnatoliContactId", "dbo.AnatoliContacts");
            //DropForeignKey("dbo.Users", "Principal_Id", "dbo.Principals");
            //DropIndex("dbo.Users", new[] { "AnatoliContactId" });
            //RenameColumn(table: "dbo.Users", name: "Principal_Id", newName: "PrincipalId");
            //RenameIndex(table: "dbo.Users", name: "IX_Principal_Id", newName: "IX_PrincipalId");
            //AddColumn("dbo.Users", "UserNameStr", c => c.String(maxLength: 50));
            //AddColumn("dbo.Users", "DataOwnerId", c => c.Guid(nullable: false));
            //AddColumn("dbo.ProductTags", "IsDefault", c => c.Boolean(nullable: false));
            //AlterColumn("dbo.Users", "AnatoliContactId", c => c.Guid());
            //CreateIndex("dbo.Users", "AnatoliContactId");
            //CreateIndex("dbo.Users", "DataOwnerId");
            //AddForeignKey("dbo.Users", "DataOwnerId", "dbo.DataOwners", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.Users", "AnatoliContactId", "dbo.AnatoliContacts", "Id");
            //AddForeignKey("dbo.Users", "PrincipalId", "dbo.Principals", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Users", "PrincipalId", "dbo.Principals");
            //DropForeignKey("dbo.Users", "AnatoliContactId", "dbo.AnatoliContacts");
            //DropForeignKey("dbo.Users", "DataOwnerId", "dbo.DataOwners");
            //DropIndex("dbo.Users", new[] { "DataOwnerId" });
            //DropIndex("dbo.Users", new[] { "AnatoliContactId" });
            //AlterColumn("dbo.Users", "AnatoliContactId", c => c.Guid(nullable: false));
            //DropColumn("dbo.ProductTags", "IsDefault");
            //DropColumn("dbo.Users", "DataOwnerId");
            //DropColumn("dbo.Users", "UserNameStr");
            //RenameIndex(table: "dbo.Users", name: "IX_PrincipalId", newName: "IX_Principal_Id");
            //RenameColumn(table: "dbo.Users", name: "PrincipalId", newName: "Principal_Id");
            //CreateIndex("dbo.Users", "AnatoliContactId");
            //AddForeignKey("dbo.Users", "Principal_Id", "dbo.Principals", "Id");
            //AddForeignKey("dbo.Users", "AnatoliContactId", "dbo.AnatoliContacts", "Id", cascadeDelete: true);
        }
    }
}
