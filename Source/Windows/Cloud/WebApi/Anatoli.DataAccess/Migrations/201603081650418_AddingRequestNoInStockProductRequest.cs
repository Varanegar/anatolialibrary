namespace Anatoli.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddingRequestNoInStockProductRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockProductRequests", "RequestNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockProductRequests", "RequestNo");
        }
    }
}
