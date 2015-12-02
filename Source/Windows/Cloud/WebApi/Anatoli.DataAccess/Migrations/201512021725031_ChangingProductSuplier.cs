namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingProductSuplier : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductSuppliers", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductSuppliers", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductSuppliers", "PrivateLabelOwner_Id", "dbo.Principals");
            DropIndex("dbo.ProductSuppliers", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductSuppliers", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductSuppliers", new[] { "PrivateLabelOwner_Id" });
            CreateTable(
                "dbo.ProductSupliers",
                c => new
                    {
                        ProductId = c.Guid(nullable: false),
                        SuplierID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.SuplierID })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SuplierID, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.SuplierID);
            
            DropTable("dbo.ProductSuppliers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductSuppliers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Comment = c.String(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PrivateLabelOwner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ProductSupliers", "SuplierID", "dbo.Suppliers");
            DropForeignKey("dbo.ProductSupliers", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductSupliers", new[] { "SuplierID" });
            DropIndex("dbo.ProductSupliers", new[] { "ProductId" });
            DropTable("dbo.ProductSupliers");
            CreateIndex("dbo.ProductSuppliers", "PrivateLabelOwner_Id");
            CreateIndex("dbo.ProductSuppliers", "LastModifiedBy_Id");
            CreateIndex("dbo.ProductSuppliers", "AddedBy_Id");
            AddForeignKey("dbo.ProductSuppliers", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductSuppliers", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductSuppliers", "AddedBy_Id", "dbo.Principals", "Id");
        }
    }
}
