namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DailyActivityMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerAreas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RegionAreaId = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.RegionAreas", t => t.RegionAreaId, cascadeDelete: true)
                .Index(t => t.RegionAreaId)
                .Index(t => t.CustomerId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.RegionAreas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AreaName = c.String(maxLength: 200),
                        NLeft = c.Int(nullable: false),
                        NRight = c.Int(nullable: false),
                        NLevel = c.Int(nullable: false),
                        Priority = c.Int(),
                        RegionAreaParentId = c.Guid(),
                        IsLeaf = c.Boolean(nullable: false),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.RegionAreas", t => t.RegionAreaParentId)
                .Index(t => t.RegionAreaParentId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.PersonnelDailyActivityDayAreas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitorId = c.Guid(nullable: false),
                        RegionAreaId = c.Guid(),
                        ActivityDate = c.DateTime(nullable: false),
                        ActivityPDate = c.String(maxLength: 10),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.RegionAreas", t => t.RegionAreaId)
                .ForeignKey("dbo.Visitors", t => t.VisitorId, cascadeDelete: true)
                .Index(t => t.VisitorId)
                .Index(t => t.RegionAreaId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Visitors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LastName = c.String(maxLength: 100),
                        FirstName = c.String(maxLength: 100),
                        Code = c.Int(nullable: false),
                        CompanyId = c.Guid(nullable: false),
                        AnatoliAccountId = c.Guid(),
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
                .ForeignKey("dbo.AnatoliAccounts", t => t.AnatoliAccountId)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.CompanyId)
                .Index(t => t.AnatoliAccountId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.PersonnelDailyActivityEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitorId = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        PersonnelDailyActivityVisitTypeId = c.Guid(nullable: false),
                        PersonnelDailyActivityEventTypeId = c.Guid(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        ShortDescription = c.String(maxLength: 1000),
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
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.PersonnelDailyActivityEventTypes", t => t.PersonnelDailyActivityEventTypeId, cascadeDelete: true)
                .ForeignKey("dbo.PersonnelDailyActivityVisitTypes", t => t.PersonnelDailyActivityVisitTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Visitors", t => t.VisitorId, cascadeDelete: true)
                .Index(t => t.VisitorId)
                .Index(t => t.CustomerId)
                .Index(t => t.PersonnelDailyActivityVisitTypeId)
                .Index(t => t.PersonnelDailyActivityEventTypeId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.PersonnelDailyActivityEventTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.PersonnelDailyActivityVisitTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.PersonnelDailyActivityPoints",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitorId = c.Guid(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        ActivityDate = c.DateTime(nullable: false),
                        ActivityPDate = c.String(maxLength: 10),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Visitors", t => t.VisitorId, cascadeDelete: true)
                .Index(t => t.VisitorId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.RegionAreaPoints",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Priority = c.Int(nullable: false),
                        RegionAreaId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.RegionAreas", t => t.RegionAreaId, cascadeDelete: true)
                .Index(t => t.RegionAreaId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.PersonnelDailyActivityCommentTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.PersonnelDailyActivityEventComments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PersonnelDailyActivityEventId = c.Guid(nullable: false),
                        PersonnelDailyActivityCommentTypeId = c.Guid(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        Description = c.String(),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.PersonnelDailyActivityCommentTypes", t => t.PersonnelDailyActivityCommentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.PersonnelDailyActivityEvents", t => t.PersonnelDailyActivityEventId, cascadeDelete: true)
                .Index(t => t.PersonnelDailyActivityEventId)
                .Index(t => t.PersonnelDailyActivityCommentTypeId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.VisitorRoutes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            AddColumn("dbo.Customers", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Customers", "Latitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisitorRoutes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.VisitorRoutes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.VisitorRoutes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.VisitorRoutes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.VisitorRoutes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PersonnelDailyActivityEventComments", "PersonnelDailyActivityEventId", "dbo.PersonnelDailyActivityEvents");
            DropForeignKey("dbo.PersonnelDailyActivityEventComments", "PersonnelDailyActivityCommentTypeId", "dbo.PersonnelDailyActivityCommentTypes");
            DropForeignKey("dbo.PersonnelDailyActivityEventComments", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PersonnelDailyActivityEventComments", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PersonnelDailyActivityEventComments", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PersonnelDailyActivityEventComments", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PersonnelDailyActivityEventComments", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PersonnelDailyActivityCommentTypes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PersonnelDailyActivityCommentTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PersonnelDailyActivityCommentTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PersonnelDailyActivityCommentTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PersonnelDailyActivityCommentTypes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CustomerAreas", "RegionAreaId", "dbo.RegionAreas");
            DropForeignKey("dbo.RegionAreaPoints", "RegionAreaId", "dbo.RegionAreas");
            DropForeignKey("dbo.RegionAreaPoints", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.RegionAreaPoints", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.RegionAreaPoints", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.RegionAreaPoints", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.RegionAreaPoints", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.RegionAreas", "RegionAreaParentId", "dbo.RegionAreas");
            DropForeignKey("dbo.PersonnelDailyActivityDayAreas", "VisitorId", "dbo.Visitors");
            DropForeignKey("dbo.PersonnelDailyActivityPoints", "VisitorId", "dbo.Visitors");
            DropForeignKey("dbo.PersonnelDailyActivityPoints", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PersonnelDailyActivityPoints", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PersonnelDailyActivityPoints", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PersonnelDailyActivityPoints", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PersonnelDailyActivityPoints", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PersonnelDailyActivityEvents", "VisitorId", "dbo.Visitors");
            DropForeignKey("dbo.PersonnelDailyActivityEvents", "PersonnelDailyActivityVisitTypeId", "dbo.PersonnelDailyActivityVisitTypes");
            DropForeignKey("dbo.PersonnelDailyActivityVisitTypes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PersonnelDailyActivityVisitTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PersonnelDailyActivityVisitTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PersonnelDailyActivityVisitTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PersonnelDailyActivityVisitTypes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PersonnelDailyActivityEvents", "PersonnelDailyActivityEventTypeId", "dbo.PersonnelDailyActivityEventTypes");
            DropForeignKey("dbo.PersonnelDailyActivityEventTypes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PersonnelDailyActivityEventTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PersonnelDailyActivityEventTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PersonnelDailyActivityEventTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PersonnelDailyActivityEventTypes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PersonnelDailyActivityEvents", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PersonnelDailyActivityEvents", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PersonnelDailyActivityEvents", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PersonnelDailyActivityEvents", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.PersonnelDailyActivityEvents", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PersonnelDailyActivityEvents", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Visitors", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Visitors", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Visitors", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.Visitors", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Visitors", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Visitors", "AnatoliAccountId", "dbo.AnatoliAccounts");
            DropForeignKey("dbo.Visitors", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PersonnelDailyActivityDayAreas", "RegionAreaId", "dbo.RegionAreas");
            DropForeignKey("dbo.PersonnelDailyActivityDayAreas", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PersonnelDailyActivityDayAreas", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PersonnelDailyActivityDayAreas", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PersonnelDailyActivityDayAreas", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PersonnelDailyActivityDayAreas", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.RegionAreas", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.RegionAreas", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.RegionAreas", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.RegionAreas", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.RegionAreas", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CustomerAreas", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CustomerAreas", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CustomerAreas", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CustomerAreas", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerAreas", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CustomerAreas", "AddedBy_Id", "dbo.Users");
            DropIndex("dbo.VisitorRoutes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.VisitorRoutes", new[] { "AddedBy_Id" });
            DropIndex("dbo.VisitorRoutes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.VisitorRoutes", new[] { "DataOwnerId" });
            DropIndex("dbo.VisitorRoutes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PersonnelDailyActivityEventComments", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PersonnelDailyActivityEventComments", new[] { "AddedBy_Id" });
            DropIndex("dbo.PersonnelDailyActivityEventComments", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PersonnelDailyActivityEventComments", new[] { "DataOwnerId" });
            DropIndex("dbo.PersonnelDailyActivityEventComments", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PersonnelDailyActivityEventComments", new[] { "PersonnelDailyActivityCommentTypeId" });
            DropIndex("dbo.PersonnelDailyActivityEventComments", new[] { "PersonnelDailyActivityEventId" });
            DropIndex("dbo.PersonnelDailyActivityCommentTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PersonnelDailyActivityCommentTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.PersonnelDailyActivityCommentTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PersonnelDailyActivityCommentTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.PersonnelDailyActivityCommentTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.RegionAreaPoints", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.RegionAreaPoints", new[] { "AddedBy_Id" });
            DropIndex("dbo.RegionAreaPoints", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.RegionAreaPoints", new[] { "DataOwnerId" });
            DropIndex("dbo.RegionAreaPoints", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.RegionAreaPoints", new[] { "RegionAreaId" });
            DropIndex("dbo.PersonnelDailyActivityPoints", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PersonnelDailyActivityPoints", new[] { "AddedBy_Id" });
            DropIndex("dbo.PersonnelDailyActivityPoints", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PersonnelDailyActivityPoints", new[] { "DataOwnerId" });
            DropIndex("dbo.PersonnelDailyActivityPoints", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PersonnelDailyActivityPoints", new[] { "VisitorId" });
            DropIndex("dbo.PersonnelDailyActivityVisitTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PersonnelDailyActivityVisitTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.PersonnelDailyActivityVisitTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PersonnelDailyActivityVisitTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.PersonnelDailyActivityVisitTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PersonnelDailyActivityEventTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PersonnelDailyActivityEventTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.PersonnelDailyActivityEventTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PersonnelDailyActivityEventTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.PersonnelDailyActivityEventTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PersonnelDailyActivityEvents", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PersonnelDailyActivityEvents", new[] { "AddedBy_Id" });
            DropIndex("dbo.PersonnelDailyActivityEvents", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PersonnelDailyActivityEvents", new[] { "DataOwnerId" });
            DropIndex("dbo.PersonnelDailyActivityEvents", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PersonnelDailyActivityEvents", new[] { "PersonnelDailyActivityEventTypeId" });
            DropIndex("dbo.PersonnelDailyActivityEvents", new[] { "PersonnelDailyActivityVisitTypeId" });
            DropIndex("dbo.PersonnelDailyActivityEvents", new[] { "CustomerId" });
            DropIndex("dbo.PersonnelDailyActivityEvents", new[] { "VisitorId" });
            DropIndex("dbo.Visitors", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Visitors", new[] { "AddedBy_Id" });
            DropIndex("dbo.Visitors", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Visitors", new[] { "DataOwnerId" });
            DropIndex("dbo.Visitors", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.Visitors", new[] { "AnatoliAccountId" });
            DropIndex("dbo.Visitors", new[] { "CompanyId" });
            DropIndex("dbo.PersonnelDailyActivityDayAreas", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PersonnelDailyActivityDayAreas", new[] { "AddedBy_Id" });
            DropIndex("dbo.PersonnelDailyActivityDayAreas", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PersonnelDailyActivityDayAreas", new[] { "DataOwnerId" });
            DropIndex("dbo.PersonnelDailyActivityDayAreas", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PersonnelDailyActivityDayAreas", new[] { "RegionAreaId" });
            DropIndex("dbo.PersonnelDailyActivityDayAreas", new[] { "VisitorId" });
            DropIndex("dbo.RegionAreas", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.RegionAreas", new[] { "AddedBy_Id" });
            DropIndex("dbo.RegionAreas", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.RegionAreas", new[] { "DataOwnerId" });
            DropIndex("dbo.RegionAreas", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.RegionAreas", new[] { "RegionAreaParentId" });
            DropIndex("dbo.CustomerAreas", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CustomerAreas", new[] { "AddedBy_Id" });
            DropIndex("dbo.CustomerAreas", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CustomerAreas", new[] { "DataOwnerId" });
            DropIndex("dbo.CustomerAreas", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CustomerAreas", new[] { "CustomerId" });
            DropIndex("dbo.CustomerAreas", new[] { "RegionAreaId" });
            DropColumn("dbo.Customers", "Latitude");
            DropColumn("dbo.Customers", "Longitude");
            DropTable("dbo.VisitorRoutes");
            DropTable("dbo.PersonnelDailyActivityEventComments");
            DropTable("dbo.PersonnelDailyActivityCommentTypes");
            DropTable("dbo.RegionAreaPoints");
            DropTable("dbo.PersonnelDailyActivityPoints");
            DropTable("dbo.PersonnelDailyActivityVisitTypes");
            DropTable("dbo.PersonnelDailyActivityEventTypes");
            DropTable("dbo.PersonnelDailyActivityEvents");
            DropTable("dbo.Visitors");
            DropTable("dbo.PersonnelDailyActivityDayAreas");
            DropTable("dbo.RegionAreas");
            DropTable("dbo.CustomerAreas");
        }
    }
}
