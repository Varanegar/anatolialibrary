namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "NationalCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "NationalCode");
        }
    }
}
