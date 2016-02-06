namespace Anatoli.DataAccess.Migrations
{
    using Anatoli.DataAccess.Models;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDataInsert3 : DbMigration
    {
        public override void Up()
        {
            AnatoliDbContext context = new AnatoliDbContext();

            context.StockProductRequestRuleCalcTypes.AddOrUpdate(item => item.Id,
                new StockProductRequestRuleCalcType { Id = Guid.Parse("D4AA1C1A-536C-4596-AF30-73E1A771417B"), StockProductRequestRuleCalcTypeName = "قانون عادی", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );
            context.StockProductRequestRules.AddOrUpdate(item => item.Id,
                new StockProductRequestRule { Id = Guid.Parse("cc9b0f95-c067-4ee4-bff9-e1fb19853f52"), StockProductRequestRuleCalcTypeId = Guid.Parse("D4AA1C1A-536C-4596-AF30-73E1A771417B"), StockProductRequestRuleTypeId = Guid.Parse("9f1f9ce4-4d5d-4885-9458-16eb24bf1b59"), ReorderCalcTypeId = Guid.Parse("7ECB9525-EA5F-487E-BBB6-971B9B22D7FF"), StockProductRequestRuleName = "بر اساس نقطه سفارش", FromDate = DateTime.Parse("2011/03/21"), FromPDate = "1390/01/01", ToDate = DateTime.Parse("2111/03/21"), ToPDate = "1490/01/01", IsRemoved = false, PrivateLabelOwner_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"), CreatedDate = DateTime.Now, LastUpdate = DateTime.Now, }
                );
            context.SaveChanges();


        }
        
        public override void Down()
        {
        }
    }
}
