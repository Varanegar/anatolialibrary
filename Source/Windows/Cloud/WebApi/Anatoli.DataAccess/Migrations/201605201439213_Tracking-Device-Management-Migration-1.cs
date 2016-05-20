namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrackingDeviceManagementMigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyDeviceLicenses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CompanyDeviceId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.CompanyDevices", t => t.CompanyDeviceId, cascadeDelete: true)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.CompanyDeviceId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.CompanyDevices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceModel = c.String(maxLength: 100),
                        MacAdress = c.String(maxLength: 100),
                        IEMI = c.String(maxLength: 100),
                        CompanyId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.CompanyId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompanyDeviceLicenses", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.CompanyDeviceLicenses", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CompanyDeviceLicenses", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CompanyDeviceLicenses", "CompanyDeviceId", "dbo.CompanyDevices");
            DropForeignKey("dbo.CompanyDevices", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.CompanyDevices", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CompanyDevices", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CompanyDevices", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.CompanyDevices", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CompanyDevices", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.CompanyDeviceLicenses", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CompanyDeviceLicenses", "AddedById", "dbo.Principals");
            DropIndex("dbo.CompanyDevices", new[] { "LastModifiedById" });
            DropIndex("dbo.CompanyDevices", new[] { "AddedById" });
            DropIndex("dbo.CompanyDevices", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CompanyDevices", new[] { "DataOwnerId" });
            DropIndex("dbo.CompanyDevices", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CompanyDevices", new[] { "CompanyId" });
            DropIndex("dbo.CompanyDeviceLicenses", new[] { "LastModifiedById" });
            DropIndex("dbo.CompanyDeviceLicenses", new[] { "AddedById" });
            DropIndex("dbo.CompanyDeviceLicenses", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CompanyDeviceLicenses", new[] { "DataOwnerId" });
            DropIndex("dbo.CompanyDeviceLicenses", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CompanyDeviceLicenses", new[] { "CompanyDeviceId" });
            DropTable("dbo.CompanyDevices");
            DropTable("dbo.CompanyDeviceLicenses");
        }
    }
}
