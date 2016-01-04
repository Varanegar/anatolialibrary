namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockRequest7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockProductRequestRules", "ProductGroupId", "dbo.ProductGroups");
            DropIndex("dbo.StockProductRequestRules", new[] { "ProductGroupId" });
            CreateTable(
                "dbo.MainProductGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GroupName = c.String(),
                        NLeft = c.Int(nullable: false),
                        NRight = c.Int(nullable: false),
                        NLevel = c.Int(nullable: false),
                        Priority = c.Int(),
                        ProductGroup2Id = c.Guid(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PrivateLabelOwner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id)
                .ForeignKey("dbo.MainProductGroups", t => t.ProductGroup2Id)
                .Index(t => t.ProductGroup2Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
            AddColumn("dbo.Products", "MainProductGroupId", c => c.Guid());
            AddColumn("dbo.StockProductRequestRules", "MainProductGroupId", c => c.Guid());
            CreateIndex("dbo.Products", "MainProductGroupId");
            CreateIndex("dbo.StockProductRequestRules", "MainProductGroupId");
            AddForeignKey("dbo.Products", "MainProductGroupId", "dbo.MainProductGroups", "Id");
            AddForeignKey("dbo.StockProductRequestRules", "MainProductGroupId", "dbo.MainProductGroups", "Id");
            DropColumn("dbo.StockProductRequestRules", "ProductGroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockProductRequestRules", "ProductGroupId", c => c.Guid());
            DropForeignKey("dbo.StockProductRequestRules", "MainProductGroupId", "dbo.MainProductGroups");
            DropForeignKey("dbo.Products", "MainProductGroupId", "dbo.MainProductGroups");
            DropForeignKey("dbo.MainProductGroups", "ProductGroup2Id", "dbo.MainProductGroups");
            DropForeignKey("dbo.MainProductGroups", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.MainProductGroups", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.MainProductGroups", "AddedBy_Id", "dbo.Principals");
            DropIndex("dbo.StockProductRequestRules", new[] { "MainProductGroupId" });
            DropIndex("dbo.MainProductGroups", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.MainProductGroups", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.MainProductGroups", new[] { "AddedBy_Id" });
            DropIndex("dbo.MainProductGroups", new[] { "ProductGroup2Id" });
            DropIndex("dbo.Products", new[] { "MainProductGroupId" });
            DropColumn("dbo.StockProductRequestRules", "MainProductGroupId");
            DropColumn("dbo.Products", "MainProductGroupId");
            DropTable("dbo.MainProductGroups");
            CreateIndex("dbo.StockProductRequestRules", "ProductGroupId");
            AddForeignKey("dbo.StockProductRequestRules", "ProductGroupId", "dbo.ProductGroups", "Id");
        }
    }
}
