namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreActiveOnHandMigration2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.StoreActiveOnhands", "IX_FirstAndSecond");
            DropIndex("dbo.StoreActivePriceLists", "IX_FirstAndSecond");

        }
        
        public override void Down()
        {
            CreateIndex("dbo.StoreActivePriceLists", new[] { "StoreId", "ProductId" }, unique: true, name: "IX_FirstAndSecond");
            CreateIndex("dbo.StoreActiveOnhands", new[] { "StoreId", "ProductId" }, unique: true, name: "IX_FirstAndSecond");
        }
    }
}
