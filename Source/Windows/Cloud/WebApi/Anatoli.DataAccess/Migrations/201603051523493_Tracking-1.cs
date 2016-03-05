namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tracking1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyCode = c.Int(nullable: false),
                        CompanyName = c.String(maxLength: 100),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: false)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.CustomerNotVerifieds",
                c => new
                    {
                        RegionInfoId = c.Guid(),
                        RegionLevel1Id = c.Guid(),
                        RegionLevel2Id = c.Guid(),
                        RegionLevel3Id = c.Guid(),
                        RegionLevel4Id = c.Guid(),
                        Id = c.Guid(nullable: false),
                        CustomerCode = c.Long(),
                        CustomerName = c.String(maxLength: 200),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        BirthDay = c.DateTime(),
                        Phone = c.String(maxLength: 20),
                        Mobile = c.String(maxLength: 20),
                        Email = c.String(maxLength: 500),
                        MainStreet = c.String(maxLength: 500),
                        OtherStreet = c.String(maxLength: 500),
                        PostalCode = c.String(maxLength: 20),
                        NationalCode = c.String(maxLength: 20),
                        CompanyId = c.Guid(nullable: false),
                        CustomerId = c.Guid(),
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
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: false)
                .ForeignKey("dbo.CityRegions", t => t.RegionLevel4Id)
                .ForeignKey("dbo.CityRegions", t => t.RegionInfoId)
                .ForeignKey("dbo.CityRegions", t => t.RegionLevel1Id)
                .ForeignKey("dbo.CityRegions", t => t.RegionLevel2Id)
                .ForeignKey("dbo.CityRegions", t => t.RegionLevel3Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .Index(t => t.RegionInfoId)
                .Index(t => t.RegionLevel1Id)
                .Index(t => t.RegionLevel2Id)
                .Index(t => t.RegionLevel3Id)
                .Index(t => t.RegionLevel4Id)
                .Index(t => t.CompanyId)
                .Index(t => t.CustomerId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.DistCompanyCenters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CenterCode = c.Int(nullable: false),
                        CenterName = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 200),
                        Lat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Lng = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CenterTypeId = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        SupportAppOrder = c.Boolean(nullable: false),
                        SupportWebOrder = c.Boolean(nullable: false),
                        SupportCallCenterOrder = c.Boolean(nullable: false),
                        CompanyId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.DistCompanyCenters", t => t.ParentId)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: false)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .Index(t => t.ParentId)
                .Index(t => t.CompanyId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.DistCompanyRegionLevelTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DistCompanyRegionLevelTypeName = c.String(maxLength: 100),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: false)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.DistCompanyCenters", t => t.DistCompanyCenterId, cascadeDelete: true)
                .ForeignKey("dbo.DistCompanyRegions", t => t.ParentId)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: false)
                .ForeignKey("dbo.DistCompanyRegionLevelTypes", t => t.DistCompanyRegionLevelTypeId)
                .Index(t => t.ParentId)
                .Index(t => t.DistCompanyRegionLevelTypeId)
                .Index(t => t.DistCompanyCenterId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: false)
                .ForeignKey("dbo.DistCompanyRegions", t => t.DistCompanyRegionId, cascadeDelete: true)
                .Index(t => t.DistCompanyRegionId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            AddColumn("dbo.Stores", "CompanyId", c => c.Guid());
            AddColumn("dbo.IncompletePurchaseOrders", "CustomerShipAddressId", c => c.Guid());
            AddColumn("dbo.Stocks", "CompanyId", c => c.Guid());
            CreateIndex("dbo.Stores", "CompanyId");
            CreateIndex("dbo.IncompletePurchaseOrders", "CustomerShipAddressId");
            CreateIndex("dbo.Stocks", "CompanyId");
            AddForeignKey("dbo.Stocks", "CompanyId", "dbo.Companies", "Id");
            AddForeignKey("dbo.Stores", "CompanyId", "dbo.Companies", "Id");
            AddForeignKey("dbo.IncompletePurchaseOrders", "CustomerShipAddressId", "dbo.CustomerShipAddresses", "Id");
            DropColumn("dbo.Stores", "GradeValueId");
            DropColumn("dbo.Stores", "StoreTemplateId");
            DropColumn("dbo.Stores", "StoreStatusTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stores", "StoreStatusTypeId", c => c.Byte());
            AddColumn("dbo.Stores", "StoreTemplateId", c => c.Int());
            AddColumn("dbo.Stores", "GradeValueId", c => c.Int());
            DropForeignKey("dbo.DistCompanyRegionLevelTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.DistCompanyRegionLevelTypes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.DistCompanyRegions", "DistCompanyRegionLevelTypeId", "dbo.DistCompanyRegionLevelTypes");
            DropForeignKey("dbo.DistCompanyRegions", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.DistCompanyRegions", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.DistCompanyRegions", "ParentId", "dbo.DistCompanyRegions");
            DropForeignKey("dbo.DistCompanyRegionPolygons", "DistCompanyRegionId", "dbo.DistCompanyRegions");
            DropForeignKey("dbo.DistCompanyRegionPolygons", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.DistCompanyRegionPolygons", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.DistCompanyRegionPolygons", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.DistCompanyRegions", "DistCompanyCenterId", "dbo.DistCompanyCenters");
            DropForeignKey("dbo.DistCompanyRegions", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.DistCompanyRegionLevelTypes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrders", "CustomerShipAddressId", "dbo.CustomerShipAddresses");
            DropForeignKey("dbo.Stores", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Stocks", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Companies", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Companies", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.DistCompanyCenters", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.DistCompanyCenters", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.DistCompanyCenters", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.DistCompanyCenters", "ParentId", "dbo.DistCompanyCenters");
            DropForeignKey("dbo.DistCompanyCenters", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CustomerNotVerifieds", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.CustomerNotVerifieds", "RegionLevel3Id", "dbo.CityRegions");
            DropForeignKey("dbo.CustomerNotVerifieds", "RegionLevel2Id", "dbo.CityRegions");
            DropForeignKey("dbo.CustomerNotVerifieds", "RegionLevel1Id", "dbo.CityRegions");
            DropForeignKey("dbo.CustomerNotVerifieds", "RegionInfoId", "dbo.CityRegions");
            DropForeignKey("dbo.CustomerNotVerifieds", "RegionLevel4Id", "dbo.CityRegions");
            DropForeignKey("dbo.CustomerNotVerifieds", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CustomerNotVerifieds", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CustomerNotVerifieds", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerNotVerifieds", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Companies", "AddedBy_Id", "dbo.Principals");
            DropIndex("dbo.DistCompanyRegionPolygons", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.DistCompanyRegionPolygons", new[] { "AddedBy_Id" });
            DropIndex("dbo.DistCompanyRegionPolygons", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.DistCompanyRegionPolygons", new[] { "DistCompanyRegionId" });
            DropIndex("dbo.DistCompanyRegions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.DistCompanyRegions", new[] { "AddedBy_Id" });
            DropIndex("dbo.DistCompanyRegions", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.DistCompanyRegions", new[] { "DistCompanyCenterId" });
            DropIndex("dbo.DistCompanyRegions", new[] { "DistCompanyRegionLevelTypeId" });
            DropIndex("dbo.DistCompanyRegions", new[] { "ParentId" });
            DropIndex("dbo.DistCompanyRegionLevelTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.DistCompanyRegionLevelTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.DistCompanyRegionLevelTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.DistCompanyCenters", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.DistCompanyCenters", new[] { "AddedBy_Id" });
            DropIndex("dbo.DistCompanyCenters", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.DistCompanyCenters", new[] { "CompanyId" });
            DropIndex("dbo.DistCompanyCenters", new[] { "ParentId" });
            DropIndex("dbo.Stocks", new[] { "CompanyId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "CustomerShipAddressId" });
            DropIndex("dbo.CustomerNotVerifieds", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CustomerNotVerifieds", new[] { "AddedBy_Id" });
            DropIndex("dbo.CustomerNotVerifieds", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CustomerNotVerifieds", new[] { "CustomerId" });
            DropIndex("dbo.CustomerNotVerifieds", new[] { "CompanyId" });
            DropIndex("dbo.CustomerNotVerifieds", new[] { "RegionLevel4Id" });
            DropIndex("dbo.CustomerNotVerifieds", new[] { "RegionLevel3Id" });
            DropIndex("dbo.CustomerNotVerifieds", new[] { "RegionLevel2Id" });
            DropIndex("dbo.CustomerNotVerifieds", new[] { "RegionLevel1Id" });
            DropIndex("dbo.CustomerNotVerifieds", new[] { "RegionInfoId" });
            DropIndex("dbo.Companies", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Companies", new[] { "AddedBy_Id" });
            DropIndex("dbo.Companies", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Stores", new[] { "CompanyId" });
            DropColumn("dbo.Stocks", "CompanyId");
            DropColumn("dbo.IncompletePurchaseOrders", "CustomerShipAddressId");
            DropColumn("dbo.Stores", "CompanyId");
            DropTable("dbo.DistCompanyRegionPolygons");
            DropTable("dbo.DistCompanyRegions");
            DropTable("dbo.DistCompanyRegionLevelTypes");
            DropTable("dbo.DistCompanyCenters");
            DropTable("dbo.CustomerNotVerifieds");
            DropTable("dbo.Companies");
        }
    }
}
