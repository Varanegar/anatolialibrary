namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProductBaseRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductBases", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductBases", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductBases", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Products", "ProductBase_Id", "dbo.ProductBases");
            DropIndex("dbo.Products", new[] { "ProductBase_Id" });
            DropIndex("dbo.ProductBases", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductBases", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductBases", new[] { "PrivateLabelOwner_Id" });
            DropColumn("dbo.Products", "ProductBase_Id");
            DropTable("dbo.ProductBases");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductBases",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductBaseUniqueId = c.Guid(nullable: false),
                        ProductBaseCode = c.String(),
                        ProductBaseName = c.String(),
                        StoreProductBaseName = c.String(),
                        PackUnitValueId = c.Int(nullable: false),
                        ProductTypeValueId = c.Int(nullable: false),
                        PackVolume = c.Decimal(precision: 18, scale: 2),
                        PackWeight = c.Decimal(precision: 18, scale: 2),
                        TaxCategoryValueId = c.Long(),
                        ProductGroupId = c.Int(),
                        MainSupplierId = c.Int(),
                        CharCategoryId = c.Int(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PrivateLabelOwner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "ProductBase_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.ProductBases", "PrivateLabelOwner_Id");
            CreateIndex("dbo.ProductBases", "LastModifiedBy_Id");
            CreateIndex("dbo.ProductBases", "AddedBy_Id");
            CreateIndex("dbo.Products", "ProductBase_Id");
            AddForeignKey("dbo.Products", "ProductBase_Id", "dbo.ProductBases", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductBases", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductBases", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductBases", "AddedBy_Id", "dbo.Principals", "Id");
        }
    }
}
