namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerShipAdressMainStreetMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerShipAddresses", "MainStreet", c => c.String(maxLength: 500));
            AlterColumn("dbo.CustomerShipAddresses", "OtherStreet", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerShipAddresses", "OtherStreet", c => c.String(maxLength: 50));
            AlterColumn("dbo.CustomerShipAddresses", "MainStreet", c => c.String(maxLength: 50));
        }
    }
}
