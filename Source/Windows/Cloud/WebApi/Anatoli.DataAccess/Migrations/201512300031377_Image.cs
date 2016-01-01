namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Image : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TokenId = c.String(),
                        OriginalName = c.String(),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Images", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Images", "AddedBy_Id", "dbo.Principals");
            DropIndex("dbo.Images", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Images", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Images", new[] { "AddedBy_Id" });
            DropTable("dbo.Images");
        }
    }
}
