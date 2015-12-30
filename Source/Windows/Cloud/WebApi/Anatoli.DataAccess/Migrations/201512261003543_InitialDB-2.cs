namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.StoreCalendars", name: "Store_Id", newName: "StoreId");
            RenameIndex(table: "dbo.StoreCalendars", name: "IX_Store_Id", newName: "IX_StoreId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.StoreCalendars", name: "IX_StoreId", newName: "IX_Store_Id");
            RenameColumn(table: "dbo.StoreCalendars", name: "StoreId", newName: "Store_Id");
        }
    }
}
