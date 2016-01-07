namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncompletePurchaseOrder4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IncompletePurchaseOrders", "PaymentTypeId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IncompletePurchaseOrders", "PaymentTypeId", c => c.Guid(nullable: false));
        }
    }
}
