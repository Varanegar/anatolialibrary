namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddinManufacture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Manufactures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
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
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
            AddColumn("dbo.Products", "Desctription", c => c.String());
            AddColumn("dbo.Products", "Manufacture_Id", c => c.Guid());
            CreateIndex("dbo.Products", "Manufacture_Id");
            AddForeignKey("dbo.Products", "Manufacture_Id", "dbo.Manufactures", "Id");
            DropColumn("dbo.Suppliers", "SupplierUniqueId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Suppliers", "SupplierUniqueId", c => c.Guid());
            DropForeignKey("dbo.Products", "Manufacture_Id", "dbo.Manufactures");
            DropForeignKey("dbo.Manufactures", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Manufactures", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Manufactures", "AddedBy_Id", "dbo.Principals");
            DropIndex("dbo.Manufactures", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Manufactures", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Manufactures", new[] { "AddedBy_Id" });
            DropIndex("dbo.Products", new[] { "Manufacture_Id" });
            DropColumn("dbo.Products", "Manufacture_Id");
            DropColumn("dbo.Products", "Desctription");
            DropTable("dbo.Manufactures");
        }
    }
}
