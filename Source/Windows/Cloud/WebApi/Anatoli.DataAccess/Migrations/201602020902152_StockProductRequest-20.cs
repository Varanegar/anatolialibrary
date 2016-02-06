namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockProductRequest20 : DbMigration
    {
        public override void Up()
        {
            Sql("delete StockProductRequestRules");

            CreateTable(
                "dbo.StockProductRequestRuleCalcTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockProductRequestRuleCalcTypeName = c.String(maxLength: 100),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            AddColumn("dbo.StockProductRequestRules", "StockProductRequestRuleCalcTypeId", c => c.Guid(nullable: false));
            CreateIndex("dbo.StockProductRequestRules", "StockProductRequestRuleCalcTypeId");
            AddForeignKey("dbo.StockProductRequestRules", "StockProductRequestRuleCalcTypeId", "dbo.StockProductRequestRuleCalcTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockProductRequestRules", "StockProductRequestRuleCalcTypeId", "dbo.StockProductRequestRuleCalcTypes");
            DropForeignKey("dbo.StockProductRequestRuleCalcTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestRuleCalcTypes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestRuleCalcTypes", "AddedBy_Id", "dbo.Principals");
            DropIndex("dbo.StockProductRequestRuleCalcTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestRuleCalcTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestRuleCalcTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequestRules", new[] { "StockProductRequestRuleCalcTypeId" });
            DropColumn("dbo.StockProductRequestRules", "StockProductRequestRuleCalcTypeId");
            DropTable("dbo.StockProductRequestRuleCalcTypes");
        }
    }
}
