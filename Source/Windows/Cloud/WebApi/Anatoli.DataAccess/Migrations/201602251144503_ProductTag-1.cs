namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductTag1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductTagValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FromDate = c.DateTime(),
                        FromPDate = c.String(),
                        ToDate = c.DateTime(),
                        ToPDate = c.String(),
                        ProductTagId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.ProductTags", t => t.ProductTagId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductTagId)
                .Index(t => t.ProductId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.ProductTags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TagName = c.String(maxLength: 100),
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
            
            AlterColumn("dbo.Users", "FullName", c => c.String(maxLength: 200));
            AlterColumn("dbo.Users", "ResetSMSCode", c => c.String(maxLength: 200));
            AlterColumn("dbo.Users", "ResetSMSPass", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTagValues", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductTagValues", "ProductTagId", "dbo.ProductTags");
            DropForeignKey("dbo.ProductTags", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductTags", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductTags", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductTagValues", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductTagValues", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductTagValues", "AddedBy_Id", "dbo.Principals");
            DropIndex("dbo.ProductTags", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductTags", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductTags", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ProductTagValues", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductTagValues", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductTagValues", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ProductTagValues", new[] { "ProductId" });
            DropIndex("dbo.ProductTagValues", new[] { "ProductTagId" });
            AlterColumn("dbo.Users", "ResetSMSPass", c => c.String());
            AlterColumn("dbo.Users", "ResetSMSCode", c => c.String());
            AlterColumn("dbo.Users", "FullName", c => c.String());
            DropTable("dbo.ProductTags");
            DropTable("dbo.ProductTagValues");
        }
    }
}
