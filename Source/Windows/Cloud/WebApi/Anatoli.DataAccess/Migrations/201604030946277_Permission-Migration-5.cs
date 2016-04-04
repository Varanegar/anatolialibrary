namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PermissionMigration5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Principal_Id", "dbo.Principals");
            RenameColumn(table: "dbo.Users", name: "Principal_Id", newName: "PrincipalId");
            RenameIndex(table: "dbo.Users", name: "IX_Principal_Id", newName: "IX_PrincipalId");
            AddForeignKey("dbo.Users", "PrincipalId", "dbo.Principals", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "PrincipalId", "dbo.Principals");
            RenameIndex(table: "dbo.Users", name: "IX_PrincipalId", newName: "IX_Principal_Id");
            RenameColumn(table: "dbo.Users", name: "PrincipalId", newName: "Principal_Id");
            AddForeignKey("dbo.Users", "Principal_Id", "dbo.Principals", "Id");
        }
    }
}
