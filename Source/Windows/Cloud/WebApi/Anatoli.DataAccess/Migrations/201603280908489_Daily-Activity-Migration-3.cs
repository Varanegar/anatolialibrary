namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DailyActivityMigration3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DistCompanyCenters", newName: "CompanyCenters");
            RenameTable(name: "dbo.DistCompanyRegionLevelTypes", newName: "RegionAreaLevelTypes");
            DropForeignKey("dbo.DistCompanyRegions", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.DistCompanyRegions", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.DistCompanyRegions", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.DistCompanyRegions", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.DistCompanyRegions", "DistCompanyCenterId", "dbo.DistCompanyCenters");
            DropForeignKey("dbo.DistCompanyRegionPolygons", "DistCompanyRegionId", "dbo.DistCompanyRegions");
            DropForeignKey("dbo.DistCompanyRegions", "ParentId", "dbo.DistCompanyRegions");
            DropForeignKey("dbo.DistCompanyRegions", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.DistCompanyRegions", "DistCompanyRegionLevelTypeId", "dbo.DistCompanyRegionLevelTypes");
            DropIndex("dbo.DistCompanyRegions", new[] { "ParentId" });
            DropIndex("dbo.DistCompanyRegions", new[] { "DistCompanyRegionLevelTypeId" });
            DropIndex("dbo.DistCompanyRegions", new[] { "DistCompanyCenterId" });
            DropIndex("dbo.DistCompanyRegions", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.DistCompanyRegions", new[] { "DataOwnerId" });
            DropIndex("dbo.DistCompanyRegions", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.DistCompanyRegions", new[] { "AddedBy_Id" });
            DropIndex("dbo.DistCompanyRegions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.DistCompanyRegionPolygons", new[] { "DistCompanyRegionId" });
            DropIndex("dbo.DistCompanyRegionPolygons", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.DistCompanyRegionPolygons", new[] { "DataOwnerId" });
            DropIndex("dbo.DistCompanyRegionPolygons", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.DistCompanyRegionPolygons", new[] { "AddedBy_Id" });
            DropIndex("dbo.DistCompanyRegionPolygons", new[] { "LastModifiedBy_Id" });
            RenameColumn(table: "dbo.Stocks", name: "DistCompanyCenterId", newName: "CompanyCenterId");
            RenameColumn(table: "dbo.Stores", name: "DistCompanyCenterId", newName: "CompanyCenterId");
            RenameIndex(table: "dbo.Stocks", name: "IX_DistCompanyCenterId", newName: "IX_CompanyCenterId");
            RenameIndex(table: "dbo.Stores", name: "IX_DistCompanyCenterId", newName: "IX_CompanyCenterId");
            AddColumn("dbo.RegionAreas", "RegionAreaLevelTypeId", c => c.Guid(nullable: false));
            AddColumn("dbo.CompanyCenters", "NLeft", c => c.Int(nullable: false));
            AddColumn("dbo.CompanyCenters", "NRight", c => c.Int(nullable: false));
            AddColumn("dbo.CompanyCenters", "NLevel", c => c.Int(nullable: false));
            AddColumn("dbo.CompanyCenters", "Priority", c => c.Int());
            AddColumn("dbo.RegionAreaLevelTypes", "RegionAreaLevelTypeName", c => c.String(maxLength: 100));
            CreateIndex("dbo.RegionAreas", "RegionAreaLevelTypeId");
            AddForeignKey("dbo.RegionAreas", "RegionAreaLevelTypeId", "dbo.RegionAreaLevelTypes", "Id");
            DropColumn("dbo.RegionAreaLevelTypes", "DistCompanyRegionLevelTypeName");
            DropTable("dbo.DistCompanyRegions");
            DropTable("dbo.DistCompanyRegionPolygons");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DistCompanyRegionPolygons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DistCompanyRegionId = c.Guid(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DistCompanyRegions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GroupCode = c.String(),
                        GroupName = c.String(),
                        NLeft = c.Int(nullable: false),
                        NRight = c.Int(nullable: false),
                        NLevel = c.Int(nullable: false),
                        isLeaf = c.Boolean(nullable: false),
                        Priority = c.Int(),
                        ParentId = c.Guid(),
                        DistCompanyRegionLevelTypeId = c.Guid(nullable: false),
                        DistCompanyCenterId = c.Guid(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RegionAreaLevelTypes", "DistCompanyRegionLevelTypeName", c => c.String(maxLength: 100));
            DropForeignKey("dbo.RegionAreas", "RegionAreaLevelTypeId", "dbo.RegionAreaLevelTypes");
            DropIndex("dbo.RegionAreas", new[] { "RegionAreaLevelTypeId" });
            DropColumn("dbo.RegionAreaLevelTypes", "RegionAreaLevelTypeName");
            DropColumn("dbo.CompanyCenters", "Priority");
            DropColumn("dbo.CompanyCenters", "NLevel");
            DropColumn("dbo.CompanyCenters", "NRight");
            DropColumn("dbo.CompanyCenters", "NLeft");
            DropColumn("dbo.RegionAreas", "RegionAreaLevelTypeId");
            RenameIndex(table: "dbo.Stores", name: "IX_CompanyCenterId", newName: "IX_DistCompanyCenterId");
            RenameIndex(table: "dbo.Stocks", name: "IX_CompanyCenterId", newName: "IX_DistCompanyCenterId");
            RenameColumn(table: "dbo.Stores", name: "CompanyCenterId", newName: "DistCompanyCenterId");
            RenameColumn(table: "dbo.Stocks", name: "CompanyCenterId", newName: "DistCompanyCenterId");
            CreateIndex("dbo.DistCompanyRegionPolygons", "LastModifiedBy_Id");
            CreateIndex("dbo.DistCompanyRegionPolygons", "AddedBy_Id");
            CreateIndex("dbo.DistCompanyRegionPolygons", "DataOwnerCenterId");
            CreateIndex("dbo.DistCompanyRegionPolygons", "DataOwnerId");
            CreateIndex("dbo.DistCompanyRegionPolygons", "ApplicationOwnerId");
            CreateIndex("dbo.DistCompanyRegionPolygons", "DistCompanyRegionId");
            CreateIndex("dbo.DistCompanyRegions", "LastModifiedBy_Id");
            CreateIndex("dbo.DistCompanyRegions", "AddedBy_Id");
            CreateIndex("dbo.DistCompanyRegions", "DataOwnerCenterId");
            CreateIndex("dbo.DistCompanyRegions", "DataOwnerId");
            CreateIndex("dbo.DistCompanyRegions", "ApplicationOwnerId");
            CreateIndex("dbo.DistCompanyRegions", "DistCompanyCenterId");
            CreateIndex("dbo.DistCompanyRegions", "DistCompanyRegionLevelTypeId");
            CreateIndex("dbo.DistCompanyRegions", "ParentId");
            AddForeignKey("dbo.DistCompanyRegions", "DistCompanyRegionLevelTypeId", "dbo.DistCompanyRegionLevelTypes", "Id");
            AddForeignKey("dbo.DistCompanyRegions", "LastModifiedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.DistCompanyRegions", "ParentId", "dbo.DistCompanyRegions", "Id");
            AddForeignKey("dbo.DistCompanyRegionPolygons", "DistCompanyRegionId", "dbo.DistCompanyRegions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DistCompanyRegions", "DistCompanyCenterId", "dbo.DistCompanyCenters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DistCompanyRegions", "DataOwnerCenterId", "dbo.DataOwnerCenters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DistCompanyRegions", "DataOwnerId", "dbo.DataOwners", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DistCompanyRegions", "ApplicationOwnerId", "dbo.ApplicationOwners", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DistCompanyRegions", "AddedBy_Id", "dbo.Users", "Id");
            RenameTable(name: "dbo.RegionAreaLevelTypes", newName: "DistCompanyRegionLevelTypes");
            RenameTable(name: "dbo.CompanyCenters", newName: "DistCompanyCenters");
        }
    }
}
