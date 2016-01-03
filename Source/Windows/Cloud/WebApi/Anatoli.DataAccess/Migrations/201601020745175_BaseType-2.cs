namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseType2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BaseValues", name: "BaseType_Id", newName: "BaseTypeId");
            RenameIndex(table: "dbo.BaseValues", name: "IX_BaseType_Id", newName: "IX_BaseTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BaseValues", name: "IX_BaseTypeId", newName: "IX_BaseType_Id");
            RenameColumn(table: "dbo.BaseValues", name: "BaseTypeId", newName: "BaseType_Id");
        }
    }
}
