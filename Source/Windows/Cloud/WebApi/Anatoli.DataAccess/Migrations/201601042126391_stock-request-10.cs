namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockrequest10 : DbMigration
    {
        public override void Up()
        {

            DropColumn("dbo.StockProductRequestRules", "FromTime");
            DropColumn("dbo.StockProductRequestRules", "ToTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockProductRequestRules", "ToTime", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.StockProductRequestRules", "FromTime", c => c.Time(nullable: false, precision: 7));
        }
    }
}
