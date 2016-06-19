namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoyaltyMigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GroupName = c.String(maxLength: 200),
                        NLeft = c.Int(nullable: false),
                        NRight = c.Int(nullable: false),
                        NLevel = c.Int(nullable: false),
                        Priority = c.Int(),
                        CustomerGroup2Id = c.Guid(),
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
                .ForeignKey("dbo.CustomerGroups", t => t.CustomerGroup2Id)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.CustomerGroup2Id)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.CustomerLoyaltyTierHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        LoyaltyTierId = c.Guid(nullable: false),
                        FromTime = c.DateTime(nullable: false),
                        ToTime = c.DateTime(nullable: false),
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
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .ForeignKey("dbo.LoyaltyTiers", t => t.LoyaltyTierId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.LoyaltyTierId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LoyaltyTiers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TierName = c.String(maxLength: 100),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LoyaltyCards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CardNo = c.String(),
                        ExpireDate = c.DateTime(nullable: false),
                        GenerateDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        AssignDate = c.Boolean(nullable: false),
                        CancelReason = c.Guid(),
                        LoyaltyCardSetId = c.Guid(nullable: false),
                        LoyaltyCardBatchId = c.Guid(),
                        CustomerId = c.Guid(),
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
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .ForeignKey("dbo.LoyaltyCardBatches", t => t.LoyaltyCardBatchId)
                .ForeignKey("dbo.LoyaltyCardSets", t => t.LoyaltyCardSetId, cascadeDelete: true)
                .Index(t => t.LoyaltyCardSetId)
                .Index(t => t.LoyaltyCardBatchId)
                .Index(t => t.CustomerId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LoyaltyCardBatches",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BatchGenerateDate = c.DateTime(nullable: false),
                        BatchGeneratePDate = c.String(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedById = c.Guid(),
                        LastModifiedById = c.Guid(),
                        LoyaltyCardSet_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedById)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .ForeignKey("dbo.LoyaltyCardSets", t => t.LoyaltyCardSet_Id)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById)
                .Index(t => t.LoyaltyCardSet_Id);
            
            CreateTable(
                "dbo.LoyaltyCardSets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoyaltyCardSetName = c.String(maxLength: 100),
                        Seed = c.Int(nullable: false),
                        CurrentNo = c.Long(nullable: false),
                        Initialtext = c.String(),
                        GenerateNoType = c.Guid(nullable: false),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.CurrencyTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CurrencyTypeName = c.String(maxLength: 100),
                        IsDefault = c.Boolean(nullable: false),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LoyaltyActionTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoyaltyActionTypeName = c.String(maxLength: 100),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LoyaltyRuleActions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ActionValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoyaltyRuleId = c.Guid(nullable: false),
                        LoyaltyActionTypeId = c.Guid(nullable: false),
                        LoyaltyValueTypeId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .ForeignKey("dbo.LoyaltyActionTypes", t => t.LoyaltyActionTypeId, cascadeDelete: true)
                .ForeignKey("dbo.LoyaltyRules", t => t.LoyaltyRuleId, cascadeDelete: true)
                .ForeignKey("dbo.LoyaltyValueTypes", t => t.LoyaltyValueTypeId, cascadeDelete: true)
                .Index(t => t.LoyaltyRuleId)
                .Index(t => t.LoyaltyActionTypeId)
                .Index(t => t.LoyaltyValueTypeId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LoyaltyRules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoyaltyRuleName = c.String(maxLength: 100),
                        Description = c.String(maxLength: 2000),
                        CalcPriority = c.Int(nullable: false),
                        LoyaltyRuleTypeId = c.Guid(nullable: false),
                        LoyaltyTriggerTypeId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .ForeignKey("dbo.LoyaltyRuleTypes", t => t.LoyaltyRuleTypeId, cascadeDelete: true)
                .ForeignKey("dbo.LoyaltyTriggerTypes", t => t.LoyaltyTriggerTypeId, cascadeDelete: true)
                .Index(t => t.LoyaltyRuleTypeId)
                .Index(t => t.LoyaltyTriggerTypeId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LoyaltyProgramRules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoyaltyRuleId = c.Guid(nullable: false),
                        LoyaltyProgramId = c.Guid(nullable: false),
                        isActive = c.Boolean(nullable: false),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .ForeignKey("dbo.LoyaltyPrograms", t => t.LoyaltyProgramId, cascadeDelete: true)
                .ForeignKey("dbo.LoyaltyRules", t => t.LoyaltyRuleId, cascadeDelete: true)
                .Index(t => t.LoyaltyRuleId)
                .Index(t => t.LoyaltyProgramId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LoyaltyPrograms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoyaltyProgramName = c.String(maxLength: 100),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LoyaltyRuleTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoyaltyRuleTypeName = c.String(maxLength: 100),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LoyaltyTriggerTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoyaltyTriggerTypeName = c.String(maxLength: 100),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LoyaltyValueTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoyaltyValueTypeName = c.String(maxLength: 100),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LoyaltyRuleConditions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoyaltyRuleId = c.Guid(nullable: false),
                        MinValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoyaltyRuleConditionTypeId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .ForeignKey("dbo.LoyaltyRules", t => t.LoyaltyRuleId, cascadeDelete: true)
                .ForeignKey("dbo.LoyaltyRuleConditionTypes", t => t.LoyaltyRuleConditionTypeId, cascadeDelete: true)
                .Index(t => t.LoyaltyRuleId)
                .Index(t => t.LoyaltyRuleConditionTypeId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LoyaltyRuleConditionTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoyaltyRuleConditionTypeName = c.String(maxLength: 100),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Principals", t => t.LastModifiedById)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedById)
                .Index(t => t.LastModifiedById);
            
            AddColumn("dbo.Customers", "CustomerGroupId", c => c.Guid());
            AddColumn("dbo.Customers", "LoyaltyTierId", c => c.Guid());
            AddColumn("dbo.CompanyCenters", "SupportPOS", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Customers", "CustomerGroupId");
            CreateIndex("dbo.Customers", "LoyaltyTierId");
            AddForeignKey("dbo.Customers", "CustomerGroupId", "dbo.CustomerGroups", "Id");
            AddForeignKey("dbo.Customers", "LoyaltyTierId", "dbo.LoyaltyTiers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoyaltyRuleConditions", "LoyaltyRuleConditionTypeId", "dbo.LoyaltyRuleConditionTypes");
            DropForeignKey("dbo.LoyaltyRuleConditionTypes", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyRuleConditionTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.LoyaltyRuleConditionTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.LoyaltyRuleConditionTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.LoyaltyRuleConditionTypes", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyRuleConditions", "LoyaltyRuleId", "dbo.LoyaltyRules");
            DropForeignKey("dbo.LoyaltyRuleConditions", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyRuleConditions", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.LoyaltyRuleConditions", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.LoyaltyRuleConditions", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.LoyaltyRuleConditions", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyRuleActions", "LoyaltyValueTypeId", "dbo.LoyaltyValueTypes");
            DropForeignKey("dbo.LoyaltyValueTypes", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyValueTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.LoyaltyValueTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.LoyaltyValueTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.LoyaltyValueTypes", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyRuleActions", "LoyaltyRuleId", "dbo.LoyaltyRules");
            DropForeignKey("dbo.LoyaltyRules", "LoyaltyTriggerTypeId", "dbo.LoyaltyTriggerTypes");
            DropForeignKey("dbo.LoyaltyTriggerTypes", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyTriggerTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.LoyaltyTriggerTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.LoyaltyTriggerTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.LoyaltyTriggerTypes", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyRules", "LoyaltyRuleTypeId", "dbo.LoyaltyRuleTypes");
            DropForeignKey("dbo.LoyaltyRuleTypes", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyRuleTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.LoyaltyRuleTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.LoyaltyRuleTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.LoyaltyRuleTypes", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyProgramRules", "LoyaltyRuleId", "dbo.LoyaltyRules");
            DropForeignKey("dbo.LoyaltyProgramRules", "LoyaltyProgramId", "dbo.LoyaltyPrograms");
            DropForeignKey("dbo.LoyaltyPrograms", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyPrograms", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.LoyaltyPrograms", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.LoyaltyPrograms", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.LoyaltyPrograms", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyProgramRules", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyProgramRules", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.LoyaltyProgramRules", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.LoyaltyProgramRules", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.LoyaltyProgramRules", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyRules", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyRules", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.LoyaltyRules", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.LoyaltyRules", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.LoyaltyRules", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyRuleActions", "LoyaltyActionTypeId", "dbo.LoyaltyActionTypes");
            DropForeignKey("dbo.LoyaltyRuleActions", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyRuleActions", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.LoyaltyRuleActions", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.LoyaltyRuleActions", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.LoyaltyRuleActions", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyActionTypes", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyActionTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.LoyaltyActionTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.LoyaltyActionTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.LoyaltyActionTypes", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.CurrencyTypes", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.CurrencyTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CurrencyTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CurrencyTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CurrencyTypes", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.Customers", "LoyaltyTierId", "dbo.LoyaltyTiers");
            DropForeignKey("dbo.LoyaltyCards", "LoyaltyCardSetId", "dbo.LoyaltyCardSets");
            DropForeignKey("dbo.LoyaltyCardBatches", "LoyaltyCardSet_Id", "dbo.LoyaltyCardSets");
            DropForeignKey("dbo.LoyaltyCardSets", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyCardSets", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.LoyaltyCardSets", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.LoyaltyCardSets", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.LoyaltyCardSets", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyCards", "LoyaltyCardBatchId", "dbo.LoyaltyCardBatches");
            DropForeignKey("dbo.LoyaltyCardBatches", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyCardBatches", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.LoyaltyCardBatches", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.LoyaltyCardBatches", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.LoyaltyCardBatches", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyCards", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyCards", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.LoyaltyCards", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.LoyaltyCards", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.LoyaltyCards", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.LoyaltyCards", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.CustomerLoyaltyTierHistories", "LoyaltyTierId", "dbo.LoyaltyTiers");
            DropForeignKey("dbo.LoyaltyTiers", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.LoyaltyTiers", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.LoyaltyTiers", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.LoyaltyTiers", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.LoyaltyTiers", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.CustomerLoyaltyTierHistories", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.CustomerLoyaltyTierHistories", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CustomerLoyaltyTierHistories", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CustomerLoyaltyTierHistories", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerLoyaltyTierHistories", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CustomerLoyaltyTierHistories", "AddedById", "dbo.Principals");
            DropForeignKey("dbo.CustomerGroups", "LastModifiedById", "dbo.Principals");
            DropForeignKey("dbo.CustomerGroups", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CustomerGroups", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.Customers", "CustomerGroupId", "dbo.CustomerGroups");
            DropForeignKey("dbo.CustomerGroups", "CustomerGroup2Id", "dbo.CustomerGroups");
            DropForeignKey("dbo.CustomerGroups", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CustomerGroups", "AddedById", "dbo.Principals");
            DropIndex("dbo.LoyaltyRuleConditionTypes", new[] { "LastModifiedById" });
            DropIndex("dbo.LoyaltyRuleConditionTypes", new[] { "AddedById" });
            DropIndex("dbo.LoyaltyRuleConditionTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.LoyaltyRuleConditionTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.LoyaltyRuleConditionTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.LoyaltyRuleConditions", new[] { "LastModifiedById" });
            DropIndex("dbo.LoyaltyRuleConditions", new[] { "AddedById" });
            DropIndex("dbo.LoyaltyRuleConditions", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.LoyaltyRuleConditions", new[] { "DataOwnerId" });
            DropIndex("dbo.LoyaltyRuleConditions", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.LoyaltyRuleConditions", new[] { "LoyaltyRuleConditionTypeId" });
            DropIndex("dbo.LoyaltyRuleConditions", new[] { "LoyaltyRuleId" });
            DropIndex("dbo.LoyaltyValueTypes", new[] { "LastModifiedById" });
            DropIndex("dbo.LoyaltyValueTypes", new[] { "AddedById" });
            DropIndex("dbo.LoyaltyValueTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.LoyaltyValueTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.LoyaltyValueTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.LoyaltyTriggerTypes", new[] { "LastModifiedById" });
            DropIndex("dbo.LoyaltyTriggerTypes", new[] { "AddedById" });
            DropIndex("dbo.LoyaltyTriggerTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.LoyaltyTriggerTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.LoyaltyTriggerTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.LoyaltyRuleTypes", new[] { "LastModifiedById" });
            DropIndex("dbo.LoyaltyRuleTypes", new[] { "AddedById" });
            DropIndex("dbo.LoyaltyRuleTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.LoyaltyRuleTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.LoyaltyRuleTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.LoyaltyPrograms", new[] { "LastModifiedById" });
            DropIndex("dbo.LoyaltyPrograms", new[] { "AddedById" });
            DropIndex("dbo.LoyaltyPrograms", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.LoyaltyPrograms", new[] { "DataOwnerId" });
            DropIndex("dbo.LoyaltyPrograms", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.LoyaltyProgramRules", new[] { "LastModifiedById" });
            DropIndex("dbo.LoyaltyProgramRules", new[] { "AddedById" });
            DropIndex("dbo.LoyaltyProgramRules", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.LoyaltyProgramRules", new[] { "DataOwnerId" });
            DropIndex("dbo.LoyaltyProgramRules", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.LoyaltyProgramRules", new[] { "LoyaltyProgramId" });
            DropIndex("dbo.LoyaltyProgramRules", new[] { "LoyaltyRuleId" });
            DropIndex("dbo.LoyaltyRules", new[] { "LastModifiedById" });
            DropIndex("dbo.LoyaltyRules", new[] { "AddedById" });
            DropIndex("dbo.LoyaltyRules", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.LoyaltyRules", new[] { "DataOwnerId" });
            DropIndex("dbo.LoyaltyRules", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.LoyaltyRules", new[] { "LoyaltyTriggerTypeId" });
            DropIndex("dbo.LoyaltyRules", new[] { "LoyaltyRuleTypeId" });
            DropIndex("dbo.LoyaltyRuleActions", new[] { "LastModifiedById" });
            DropIndex("dbo.LoyaltyRuleActions", new[] { "AddedById" });
            DropIndex("dbo.LoyaltyRuleActions", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.LoyaltyRuleActions", new[] { "DataOwnerId" });
            DropIndex("dbo.LoyaltyRuleActions", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.LoyaltyRuleActions", new[] { "LoyaltyValueTypeId" });
            DropIndex("dbo.LoyaltyRuleActions", new[] { "LoyaltyActionTypeId" });
            DropIndex("dbo.LoyaltyRuleActions", new[] { "LoyaltyRuleId" });
            DropIndex("dbo.LoyaltyActionTypes", new[] { "LastModifiedById" });
            DropIndex("dbo.LoyaltyActionTypes", new[] { "AddedById" });
            DropIndex("dbo.LoyaltyActionTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.LoyaltyActionTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.LoyaltyActionTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CurrencyTypes", new[] { "LastModifiedById" });
            DropIndex("dbo.CurrencyTypes", new[] { "AddedById" });
            DropIndex("dbo.CurrencyTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CurrencyTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.CurrencyTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.LoyaltyCardSets", new[] { "LastModifiedById" });
            DropIndex("dbo.LoyaltyCardSets", new[] { "AddedById" });
            DropIndex("dbo.LoyaltyCardSets", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.LoyaltyCardSets", new[] { "DataOwnerId" });
            DropIndex("dbo.LoyaltyCardSets", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.LoyaltyCardBatches", new[] { "LoyaltyCardSet_Id" });
            DropIndex("dbo.LoyaltyCardBatches", new[] { "LastModifiedById" });
            DropIndex("dbo.LoyaltyCardBatches", new[] { "AddedById" });
            DropIndex("dbo.LoyaltyCardBatches", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.LoyaltyCardBatches", new[] { "DataOwnerId" });
            DropIndex("dbo.LoyaltyCardBatches", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.LoyaltyCards", new[] { "LastModifiedById" });
            DropIndex("dbo.LoyaltyCards", new[] { "AddedById" });
            DropIndex("dbo.LoyaltyCards", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.LoyaltyCards", new[] { "DataOwnerId" });
            DropIndex("dbo.LoyaltyCards", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.LoyaltyCards", new[] { "CustomerId" });
            DropIndex("dbo.LoyaltyCards", new[] { "LoyaltyCardBatchId" });
            DropIndex("dbo.LoyaltyCards", new[] { "LoyaltyCardSetId" });
            DropIndex("dbo.LoyaltyTiers", new[] { "LastModifiedById" });
            DropIndex("dbo.LoyaltyTiers", new[] { "AddedById" });
            DropIndex("dbo.LoyaltyTiers", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.LoyaltyTiers", new[] { "DataOwnerId" });
            DropIndex("dbo.LoyaltyTiers", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CustomerLoyaltyTierHistories", new[] { "LastModifiedById" });
            DropIndex("dbo.CustomerLoyaltyTierHistories", new[] { "AddedById" });
            DropIndex("dbo.CustomerLoyaltyTierHistories", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CustomerLoyaltyTierHistories", new[] { "DataOwnerId" });
            DropIndex("dbo.CustomerLoyaltyTierHistories", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CustomerLoyaltyTierHistories", new[] { "LoyaltyTierId" });
            DropIndex("dbo.CustomerLoyaltyTierHistories", new[] { "CustomerId" });
            DropIndex("dbo.CustomerGroups", new[] { "LastModifiedById" });
            DropIndex("dbo.CustomerGroups", new[] { "AddedById" });
            DropIndex("dbo.CustomerGroups", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CustomerGroups", new[] { "DataOwnerId" });
            DropIndex("dbo.CustomerGroups", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CustomerGroups", new[] { "CustomerGroup2Id" });
            DropIndex("dbo.Customers", new[] { "LoyaltyTierId" });
            DropIndex("dbo.Customers", new[] { "CustomerGroupId" });
            DropColumn("dbo.CompanyCenters", "SupportPOS");
            DropColumn("dbo.Customers", "LoyaltyTierId");
            DropColumn("dbo.Customers", "CustomerGroupId");
            DropTable("dbo.LoyaltyRuleConditionTypes");
            DropTable("dbo.LoyaltyRuleConditions");
            DropTable("dbo.LoyaltyValueTypes");
            DropTable("dbo.LoyaltyTriggerTypes");
            DropTable("dbo.LoyaltyRuleTypes");
            DropTable("dbo.LoyaltyPrograms");
            DropTable("dbo.LoyaltyProgramRules");
            DropTable("dbo.LoyaltyRules");
            DropTable("dbo.LoyaltyRuleActions");
            DropTable("dbo.LoyaltyActionTypes");
            DropTable("dbo.CurrencyTypes");
            DropTable("dbo.LoyaltyCardSets");
            DropTable("dbo.LoyaltyCardBatches");
            DropTable("dbo.LoyaltyCards");
            DropTable("dbo.LoyaltyTiers");
            DropTable("dbo.CustomerLoyaltyTierHistories");
            DropTable("dbo.CustomerGroups");
        }
    }
}
