namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DailyActivityMigration4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyOrgCharts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        NLeft = c.Int(nullable: false),
                        NRight = c.Int(nullable: false),
                        NLevel = c.Int(nullable: false),
                        Priority = c.Int(),
                        ParentId = c.Guid(),
                        CompanyPersonnelId = c.Guid(),
                        CompanyCenterId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedBy_Id = c.String(maxLength: 128),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.CompanyCenters", t => t.CompanyCenterId, cascadeDelete: true)
                .ForeignKey("dbo.CompanyPersonnels", t => t.CompanyPersonnelId)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.CompanyOrgCharts", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.CompanyPersonnelId)
                .Index(t => t.CompanyCenterId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompanyOrgCharts", "ParentId", "dbo.CompanyOrgCharts");
            DropForeignKey("dbo.CompanyOrgCharts", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CompanyOrgCharts", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CompanyOrgCharts", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CompanyOrgCharts", "CompanyPersonnelId", "dbo.CompanyPersonnels");
            DropForeignKey("dbo.CompanyOrgCharts", "CompanyCenterId", "dbo.CompanyCenters");
            DropForeignKey("dbo.CompanyOrgCharts", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CompanyOrgCharts", "AddedBy_Id", "dbo.Users");
            DropIndex("dbo.CompanyOrgCharts", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CompanyOrgCharts", new[] { "AddedBy_Id" });
            DropIndex("dbo.CompanyOrgCharts", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CompanyOrgCharts", new[] { "DataOwnerId" });
            DropIndex("dbo.CompanyOrgCharts", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CompanyOrgCharts", new[] { "CompanyCenterId" });
            DropIndex("dbo.CompanyOrgCharts", new[] { "CompanyPersonnelId" });
            DropIndex("dbo.CompanyOrgCharts", new[] { "ParentId" });
            DropTable("dbo.CompanyOrgCharts");
        }
    }
}
