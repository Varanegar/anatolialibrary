namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DailyActivityMigration3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CompanyCenters", newName: "CompanyCenters");
            RenameTable(name: "dbo.CompanyRegionLevelTypes", newName: "RegionAreaLevelTypes");
            DropForeignKey("dbo.CompanyRegions", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CompanyRegions", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CompanyRegions", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CompanyRegions", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CompanyRegions", "CompanyCenterId", "dbo.CompanyCenters");
            DropForeignKey("dbo.CompanyRegionPolygons", "CompanyRegionId", "dbo.CompanyRegions");
            DropForeignKey("dbo.CompanyRegions", "ParentId", "dbo.CompanyRegions");
            DropForeignKey("dbo.CompanyRegions", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CompanyRegions", "CompanyRegionLevelTypeId", "dbo.CompanyRegionLevelTypes");
            DropIndex("dbo.CompanyRegions", new[] { "ParentId" });
            DropIndex("dbo.CompanyRegions", new[] { "CompanyRegionLevelTypeId" });
            DropIndex("dbo.CompanyRegions", new[] { "CompanyCenterId" });
            DropIndex("dbo.CompanyRegions", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CompanyRegions", new[] { "DataOwnerId" });
            DropIndex("dbo.CompanyRegions", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CompanyRegions", new[] { "AddedBy_Id" });
            DropIndex("dbo.CompanyRegions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CompanyRegionPolygons", new[] { "CompanyRegionId" });
            DropIndex("dbo.CompanyRegionPolygons", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CompanyRegionPolygons", new[] { "DataOwnerId" });
            DropIndex("dbo.CompanyRegionPolygons", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CompanyRegionPolygons", new[] { "AddedBy_Id" });
            DropIndex("dbo.CompanyRegionPolygons", new[] { "LastModifiedBy_Id" });
            RenameColumn(table: "dbo.Stocks", name: "CompanyCenterId", newName: "CompanyCenterId");
            RenameColumn(table: "dbo.Stores", name: "CompanyCenterId", newName: "CompanyCenterId");
            RenameIndex(table: "dbo.Stocks", name: "IX_CompanyCenterId", newName: "IX_CompanyCenterId");
            RenameIndex(table: "dbo.Stores", name: "IX_CompanyCenterId", newName: "IX_CompanyCenterId");
            AddColumn("dbo.RegionAreas", "RegionAreaLevelTypeId", c => c.Guid(nullable: false));
            AddColumn("dbo.CompanyCenters", "NLeft", c => c.Int(nullable: false));
            AddColumn("dbo.CompanyCenters", "NRight", c => c.Int(nullable: false));
            AddColumn("dbo.CompanyCenters", "NLevel", c => c.Int(nullable: false));
            AddColumn("dbo.CompanyCenters", "Priority", c => c.Int());
            AddColumn("dbo.RegionAreaLevelTypes", "RegionAreaLevelTypeName", c => c.String(maxLength: 100));
            CreateIndex("dbo.RegionAreas", "RegionAreaLevelTypeId");
            AddForeignKey("dbo.RegionAreas", "RegionAreaLevelTypeId", "dbo.RegionAreaLevelTypes", "Id");
            DropColumn("dbo.RegionAreaLevelTypes", "CompanyRegionLevelTypeName");
            DropTable("dbo.CompanyRegions");
            DropTable("dbo.CompanyRegionPolygons");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CompanyRegionPolygons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CompanyRegionId = c.Guid(nullable: false),
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
                "dbo.CompanyRegions",
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
                        CompanyRegionLevelTypeId = c.Guid(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RegionAreaLevelTypes", "CompanyRegionLevelTypeName", c => c.String(maxLength: 100));
            DropForeignKey("dbo.RegionAreas", "RegionAreaLevelTypeId", "dbo.RegionAreaLevelTypes");
            DropIndex("dbo.RegionAreas", new[] { "RegionAreaLevelTypeId" });
            DropColumn("dbo.RegionAreaLevelTypes", "RegionAreaLevelTypeName");
            DropColumn("dbo.CompanyCenters", "Priority");
            DropColumn("dbo.CompanyCenters", "NLevel");
            DropColumn("dbo.CompanyCenters", "NRight");
            DropColumn("dbo.CompanyCenters", "NLeft");
            DropColumn("dbo.RegionAreas", "RegionAreaLevelTypeId");
            RenameIndex(table: "dbo.Stores", name: "IX_CompanyCenterId", newName: "IX_CompanyCenterId");
            RenameIndex(table: "dbo.Stocks", name: "IX_CompanyCenterId", newName: "IX_CompanyCenterId");
            RenameColumn(table: "dbo.Stores", name: "CompanyCenterId", newName: "CompanyCenterId");
            RenameColumn(table: "dbo.Stocks", name: "CompanyCenterId", newName: "CompanyCenterId");
            CreateIndex("dbo.CompanyRegionPolygons", "LastModifiedBy_Id");
            CreateIndex("dbo.CompanyRegionPolygons", "AddedBy_Id");
            CreateIndex("dbo.CompanyRegionPolygons", "DataOwnerCenterId");
            CreateIndex("dbo.CompanyRegionPolygons", "DataOwnerId");
            CreateIndex("dbo.CompanyRegionPolygons", "ApplicationOwnerId");
            CreateIndex("dbo.CompanyRegionPolygons", "CompanyRegionId");
            CreateIndex("dbo.CompanyRegions", "LastModifiedBy_Id");
            CreateIndex("dbo.CompanyRegions", "AddedBy_Id");
            CreateIndex("dbo.CompanyRegions", "DataOwnerCenterId");
            CreateIndex("dbo.CompanyRegions", "DataOwnerId");
            CreateIndex("dbo.CompanyRegions", "ApplicationOwnerId");
            CreateIndex("dbo.CompanyRegions", "CompanyCenterId");
            CreateIndex("dbo.CompanyRegions", "CompanyRegionLevelTypeId");
            CreateIndex("dbo.CompanyRegions", "ParentId");
            AddForeignKey("dbo.CompanyRegions", "CompanyRegionLevelTypeId", "dbo.CompanyRegionLevelTypes", "Id");
            AddForeignKey("dbo.CompanyRegions", "LastModifiedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.CompanyRegions", "ParentId", "dbo.CompanyRegions", "Id");
            AddForeignKey("dbo.CompanyRegionPolygons", "CompanyRegionId", "dbo.CompanyRegions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CompanyRegions", "CompanyCenterId", "dbo.CompanyCenters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CompanyRegions", "DataOwnerCenterId", "dbo.DataOwnerCenters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CompanyRegions", "DataOwnerId", "dbo.DataOwners", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CompanyRegions", "ApplicationOwnerId", "dbo.ApplicationOwners", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CompanyRegions", "AddedBy_Id", "dbo.Users", "Id");
            RenameTable(name: "dbo.RegionAreaLevelTypes", newName: "CompanyRegionLevelTypes");
            RenameTable(name: "dbo.CompanyCenters", newName: "CompanyCenters");
        }
    }
}
