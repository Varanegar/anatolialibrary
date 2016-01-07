namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockrequest11 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.StockProducts", name: "ReorderCalcTypeId", newName: "ReorderCalcType_Id");
            RenameIndex(table: "dbo.StockProducts", name: "IX_ReorderCalcTypeId", newName: "IX_ReorderCalcType_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.StockProducts", name: "IX_ReorderCalcType_Id", newName: "IX_ReorderCalcTypeId");
            RenameColumn(table: "dbo.StockProducts", name: "ReorderCalcType_Id", newName: "ReorderCalcTypeId");
        }
    }
}
