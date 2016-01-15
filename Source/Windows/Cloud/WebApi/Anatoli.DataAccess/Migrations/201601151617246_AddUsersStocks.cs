namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersStocks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsersStocks",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        StockID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.StockID })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockID, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.StockID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersStocks", "StockID", "dbo.Stocks");
            DropForeignKey("dbo.UsersStocks", "UserId", "dbo.Users");
            DropIndex("dbo.UsersStocks", new[] { "StockID" });
            DropIndex("dbo.UsersStocks", new[] { "UserId" });
            DropTable("dbo.UsersStocks");
        }
    }
}
