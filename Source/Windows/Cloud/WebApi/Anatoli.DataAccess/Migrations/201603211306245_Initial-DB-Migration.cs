namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDBMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnatoliAccounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        AnatoliPlaceId = c.Guid(),
                        AnatoliContactId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliContacts", t => t.AnatoliContactId)
                .ForeignKey("dbo.AnatoliPlaces", t => t.AnatoliPlaceId)
                .Index(t => t.AnatoliPlaceId)
                .Index(t => t.AnatoliContactId);
            
            CreateTable(
                "dbo.AnatoliContacts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ContactName = c.String(maxLength: 200),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        BirthDay = c.DateTime(),
                        Phone = c.String(maxLength: 20),
                        Mobile = c.String(maxLength: 20),
                        Email = c.String(maxLength: 100),
                        Website = c.String(maxLength: 100),
                        NationalCode = c.String(maxLength: 20),
                        AnatoliContactTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliContactTypes", t => t.AnatoliContactTypeId, cascadeDelete: true)
                .Index(t => t.AnatoliContactTypeId);
            
            CreateTable(
                "dbo.AnatoliContactTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 20),
                        Description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                        FullName = c.String(maxLength: 200),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        LastEntry = c.DateTime(nullable: false),
                        LastEntryIp = c.String(),
                        ResetSMSCode = c.String(maxLength: 200),
                        ResetSMSPass = c.String(maxLength: 200),
                        ResetSMSRequestTime = c.DateTime(),
                        AnatoliContactId = c.Guid(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        Group_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliContacts", t => t.AnatoliContactId, cascadeDelete: true)
                .ForeignKey("dbo.Applications", t => t.ApplicationId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.ApplicationId)
                .Index(t => t.AnatoliContactId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationOwners",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        WebHookURI = c.String(maxLength: 200),
                        WebHookUsername = c.String(maxLength: 200),
                        WebHookPassword = c.String(maxLength: 200),
                        AnatoliContactId = c.Guid(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliContacts", t => t.AnatoliContactId, cascadeDelete: true)
                .ForeignKey("dbo.Applications", t => t.ApplicationId, cascadeDelete: true)
                .Index(t => t.AnatoliContactId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        ApplicationId = c.Guid(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.ApplicationId, cascadeDelete: true)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.UserId)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.PrincipalPermissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Grant = c.Boolean(nullable: false),
                        Permission_Id = c.Guid(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
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
                .ForeignKey("dbo.Permissions", t => t.Permission_Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.Permission_Id)
                .Index(t => t.UserId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.DataOwners",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        WebHookURI = c.String(maxLength: 200),
                        WebHookUsername = c.String(maxLength: 200),
                        WebHookPassword = c.String(maxLength: 200),
                        AnatoliContactId = c.Guid(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliContacts", t => t.AnatoliContactId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .Index(t => t.AnatoliContactId)
                .Index(t => t.ApplicationOwnerId);
            
            CreateTable(
                "dbo.DataOwnerCenters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        WebHookURI = c.String(maxLength: 200),
                        WebHookUsername = c.String(maxLength: 200),
                        WebHookPassword = c.String(maxLength: 200),
                        AnatoliContactId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliContacts", t => t.AnatoliContactId, cascadeDelete: true)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .Index(t => t.AnatoliContactId)
                .Index(t => t.DataOwnerId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PersianTitle = c.String(),
                        Resource = c.String(),
                        Action = c.String(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedBy_Id = c.String(maxLength: 128),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        Parent_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Permissions", t => t.Parent_Id)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockCode = c.Int(nullable: false),
                        StockName = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                        StoreId = c.Guid(),
                        CompanyCenterId = c.Guid(),
                        Accept1ById = c.String(maxLength: 128),
                        Accept2ById = c.String(maxLength: 128),
                        Accept3ById = c.String(maxLength: 128),
                        StockTypeId = c.Guid(),
                        MainSCMStock2Id = c.Guid(),
                        RelatedSCMStock2Id = c.Guid(),
                        OverRequest = c.Boolean(nullable: false),
                        OverAfterFirstAcceptance = c.Boolean(nullable: false),
                        OverAfterSecondAcceptance = c.Boolean(nullable: false),
                        OverAfterThirdAcceptance = c.Boolean(nullable: false),
                        CompanyId = c.Guid(),
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
                .ForeignKey("dbo.Users", t => t.Accept1ById)
                .ForeignKey("dbo.Users", t => t.Accept2ById)
                .ForeignKey("dbo.Users", t => t.Accept3ById)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.CompanyCenters", t => t.CompanyCenterId)
                .ForeignKey("dbo.Stores", t => t.StoreId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Stocks", t => t.MainSCMStock2Id)
                .ForeignKey("dbo.Stocks", t => t.RelatedSCMStock2Id)
                .ForeignKey("dbo.StockTypes", t => t.StockTypeId)
                .Index(t => t.StoreId)
                .Index(t => t.CompanyCenterId)
                .Index(t => t.Accept1ById)
                .Index(t => t.Accept2ById)
                .Index(t => t.Accept3ById)
                .Index(t => t.StockTypeId)
                .Index(t => t.MainSCMStock2Id)
                .Index(t => t.RelatedSCMStock2Id)
                .Index(t => t.CompanyId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyCode = c.Int(nullable: false),
                        CompanyName = c.String(maxLength: 100),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.AnatoliAccountId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Customers",
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
                        DefauleStoreId = c.Guid(),
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
                .ForeignKey("dbo.CityRegions", t => t.RegionLevel4Id)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Stores", t => t.DefauleStoreId)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
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
                .Index(t => t.DefauleStoreId)
                .Index(t => t.CompanyId)
                .Index(t => t.AnatoliAccountId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BasketTypeValueGuid = c.Guid(nullable: false),
                        BasketName = c.String(maxLength: 200),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        Qty = c.Int(),
                        Comment = c.String(maxLength: 500),
                        BasketId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Baskets", t => t.BasketId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.BasketId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductCode = c.String(maxLength: 20),
                        Barcode = c.String(maxLength: 20),
                        ProductName = c.String(maxLength: 200),
                        StoreProductName = c.String(maxLength: 200),
                        PackVolume = c.Decimal(precision: 18, scale: 2),
                        PackWeight = c.Decimal(precision: 18, scale: 2),
                        QtyPerPack = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxCategoryValueId = c.Long(),
                        Desctription = c.String(maxLength: 500),
                        ProductGroupId = c.Guid(),
                        MainProductGroupId = c.Guid(),
                        ManufactureId = c.Guid(),
                        MainSupplierId = c.Guid(),
                        ProductTypeId = c.Guid(),
                        IsActiveInOrder = c.Boolean(nullable: false),
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
                .ForeignKey("dbo.MainProductGroups", t => t.MainProductGroupId)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeId)
                .ForeignKey("dbo.Suppliers", t => t.MainSupplierId)
                .ForeignKey("dbo.Manufactures", t => t.ManufactureId)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId)
                .Index(t => t.ProductGroupId)
                .Index(t => t.MainProductGroupId)
                .Index(t => t.ManufactureId)
                .Index(t => t.MainSupplierId)
                .Index(t => t.ProductTypeId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.CharValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CharValueText = c.String(maxLength: 200),
                        CharValueFromAmount = c.Decimal(precision: 18, scale: 2),
                        CharValueToAmount = c.Decimal(precision: 18, scale: 2),
                        CharTypeId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.CharTypes", t => t.CharTypeId)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.CharTypeId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.CharTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CharTypeDesc = c.String(),
                        DefaultCharValueGuid = c.Guid(nullable: false),
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
                "dbo.CharGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CharGroupCode = c.Int(nullable: false),
                        CharGroupName = c.String(maxLength: 200),
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
                "dbo.IncompletePurchaseOrderLineItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Guid(nullable: false),
                        IncompletePurchaseOrderId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.IncompletePurchaseOrders", t => t.IncompletePurchaseOrderId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.IncompletePurchaseOrderId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.IncompletePurchaseOrders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StoreId = c.Guid(nullable: false),
                        CityRegionId = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        DeliveryTypeId = c.Guid(nullable: false),
                        PaymentTypeId = c.Guid(),
                        OrderShipAddress = c.String(),
                        CustomerShipAddressId = c.Guid(),
                        Transferee = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 20),
                        DeliveryFromTime = c.DateTime(),
                        DeliveryToTime = c.DateTime(),
                        DeliveryDate = c.DateTime(),
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
                .ForeignKey("dbo.CityRegions", t => t.CityRegionId)
                .ForeignKey("dbo.Stores", t => t.StoreId)
                .ForeignKey("dbo.CustomerShipAddresses", t => t.CustomerShipAddressId)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.StoreId)
                .Index(t => t.CityRegionId)
                .Index(t => t.CustomerId)
                .Index(t => t.CustomerShipAddressId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.CityRegions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GroupName = c.String(),
                        NLeft = c.Int(nullable: false),
                        NRight = c.Int(nullable: false),
                        NLevel = c.Int(nullable: false),
                        Priority = c.Int(),
                        CityRegion2Id = c.Guid(),
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
                .ForeignKey("dbo.CityRegions", t => t.CityRegion2Id)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.CityRegion2Id)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StoreCode = c.Int(nullable: false),
                        StoreName = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 200),
                        Lat = c.Long(nullable: false),
                        Lng = c.Long(nullable: false),
                        HasDelivery = c.Byte(nullable: false),
                        HasCourier = c.Byte(nullable: false),
                        SupportAppOrder = c.Byte(nullable: false),
                        SupportWebOrder = c.Byte(nullable: false),
                        SupportCallCenterOrder = c.Byte(nullable: false),
                        CompanyCenterId = c.Guid(),
                        CompanyId = c.Guid(),
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
                .ForeignKey("dbo.CompanyCenters", t => t.CompanyCenterId)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .Index(t => t.CompanyCenterId)
                .Index(t => t.CompanyId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.CompanyCenters",
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
                .ForeignKey("dbo.CompanyCenters", t => t.ParentId)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .Index(t => t.ParentId)
                .Index(t => t.CompanyId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StoreActions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StoreActionValueId = c.Long(nullable: false),
                        ActionDate = c.DateTime(nullable: false),
                        ActionPDate = c.String(maxLength: 10),
                        ActionTime = c.Time(precision: 7),
                        ActionDesc = c.String(maxLength: 100),
                        ActionDataId = c.Int(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedBy_Id = c.String(maxLength: 128),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        Store_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Stores", t => t.Store_Id, cascadeDelete: true)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.StoreActiveOnhands",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StoreId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId)
                .Index(t => t.ProductId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StoreActivePriceLists",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StoreId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.StoreId)
                .Index(t => t.ProductId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StoreCalendars",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        PDate = c.String(maxLength: 10),
                        FromTime = c.Time(nullable: false, precision: 7),
                        ToTime = c.Time(nullable: false, precision: 7),
                        CalendarTypeValueId = c.Guid(),
                        Description = c.String(maxLength: 200),
                        StoreId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StoreCalendarHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplyBy = c.Guid(),
                        ApplyDate = c.DateTime(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedBy_Id = c.String(maxLength: 128),
                        CalendarTemplate_Id = c.Guid(),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        StoreCalendar_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.CalendarTemplates", t => t.CalendarTemplate_Id)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.StoreCalendars", t => t.StoreCalendar_Id, cascadeDelete: true)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.CalendarTemplate_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.StoreCalendar_Id);
            
            CreateTable(
                "dbo.CalendarTemplates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CalendarTemplateName = c.String(maxLength: 200),
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
                "dbo.CalendarTemplateHolidays",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(),
                        PDate = c.String(maxLength: 10),
                        FromTime = c.Time(precision: 7),
                        ToTime = c.Time(precision: 7),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedBy_Id = c.String(maxLength: 128),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        CalendarTemplate_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.CalendarTemplates", t => t.CalendarTemplate_Id, cascadeDelete: true)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.CalendarTemplate_Id);
            
            CreateTable(
                "dbo.CalendarTemplateOpenTimes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FromTime = c.Time(nullable: false, precision: 7),
                        ToTime = c.Time(nullable: false, precision: 7),
                        WeekDay = c.Int(),
                        CalendarTypeValueId = c.Guid(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedBy_Id = c.String(maxLength: 128),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        CalendarTemplate_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.CalendarTemplates", t => t.CalendarTemplate_Id, cascadeDelete: true)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.CalendarTemplate_Id);
            
            CreateTable(
                "dbo.StoreDeliveryPersons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.Byte(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedBy_Id = c.String(maxLength: 128),
                        DeliveryPerson_Id = c.Guid(nullable: false),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        Store_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.DeliveryPersons", t => t.DeliveryPerson_Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Stores", t => t.Store_Id, cascadeDelete: true)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.DeliveryPerson_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.DeliveryPersons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LastName = c.String(maxLength: 100),
                        FirstName = c.String(maxLength: 100),
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
                "dbo.CustomerShipAddresses",
                c => new
                    {
                        RegionInfoId = c.Guid(),
                        RegionLevel1Id = c.Guid(),
                        RegionLevel2Id = c.Guid(),
                        RegionLevel3Id = c.Guid(),
                        RegionLevel4Id = c.Guid(),
                        Id = c.Guid(nullable: false),
                        AddressName = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 20),
                        Mobile = c.String(maxLength: 20),
                        Email = c.String(maxLength: 50),
                        MainStreet = c.String(maxLength: 50),
                        OtherStreet = c.String(maxLength: 50),
                        PostalCode = c.String(maxLength: 20),
                        Transferee = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        DefauleStore_Id = c.Guid(),
                        Lat = c.Decimal(precision: 18, scale: 2),
                        Lng = c.Decimal(precision: 18, scale: 2),
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
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Stores", t => t.DefauleStore_Id)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.CityRegions", t => t.RegionInfoId)
                .ForeignKey("dbo.CityRegions", t => t.RegionLevel1Id)
                .ForeignKey("dbo.CityRegions", t => t.RegionLevel2Id)
                .ForeignKey("dbo.CityRegions", t => t.RegionLevel3Id)
                .ForeignKey("dbo.CityRegions", t => t.RegionLevel4Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.RegionInfoId)
                .Index(t => t.RegionLevel1Id)
                .Index(t => t.RegionLevel2Id)
                .Index(t => t.RegionLevel3Id)
                .Index(t => t.RegionLevel4Id)
                .Index(t => t.DefauleStore_Id)
                .Index(t => t.CustomerId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ActionSourceId = c.Guid(nullable: false),
                        DeviceIMEI = c.String(),
                        OrderDate = c.DateTime(),
                        OrderPDate = c.String(),
                        OrderTime = c.Time(precision: 7),
                        PaymentTypeId = c.Guid(nullable: false),
                        DiscountCodeId = c.String(),
                        AppOrderNo = c.Long(nullable: false),
                        DiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountAmount2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChargeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherAdd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherSub = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comment = c.String(maxLength: 500),
                        DeliveryTypeId = c.Guid(nullable: false),
                        DeliveryDate = c.DateTime(),
                        DeliveryPDate = c.String(maxLength: 10),
                        DeliveryFromTime = c.Time(precision: 7),
                        DeliveryToTime = c.Time(precision: 7),
                        PurchaseOrderStatusId = c.Guid(nullable: false),
                        DiscountFinalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount2FinalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChargeFinalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxFinalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingFinalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalFinalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherFinalAdd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherFinalSub = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalNetAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsCancelled = c.Boolean(nullable: false),
                        CancelReasonId = c.Guid(),
                        CancelDesc = c.String(maxLength: 100),
                        BackOfficeId = c.Int(),
                        StoreId = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        CustomerShipAddressId = c.Guid(),
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
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .ForeignKey("dbo.CustomerShipAddresses", t => t.CustomerShipAddressId)
                .Index(t => t.StoreId)
                .Index(t => t.CustomerId)
                .Index(t => t.CustomerShipAddressId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.PurchaseOrderClearances",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeliveryPersonId = c.Int(nullable: false),
                        ClearanceStatusTypeId = c.Int(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedBy_Id = c.String(maxLength: 128),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        PurchaseOrder_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrder_Id, cascadeDelete: true)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PurchaseOrder_Id);
            
            CreateTable(
                "dbo.PurchaseOrderStatusHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StatusValueId = c.Guid(nullable: false),
                        StatusDate = c.DateTime(nullable: false),
                        StatusPDate = c.String(maxLength: 10),
                        Comment = c.String(maxLength: 100),
                        PurchaseOrderId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrderId, cascadeDelete: true)
                .Index(t => t.PurchaseOrderId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.PurchaseOrderLineItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChargeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsPrize = c.Boolean(nullable: false),
                        Comment = c.String(maxLength: 500),
                        AllowReplace = c.Boolean(nullable: false),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalUnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalTaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalChargeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalNetAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalIsPrize = c.Boolean(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        FinalProductId = c.Guid(),
                        PurchaseOrderId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.FinalProductId)
                .Index(t => t.ProductId)
                .Index(t => t.FinalProductId)
                .Index(t => t.PurchaseOrderId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.PurchaseOrderPayments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PurchaseOrderGiftCardId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentTypeValueId = c.Long(nullable: false),
                        GiftCardId = c.Int(),
                        BankAccountId = c.String(),
                        PaymentTrackingNo = c.String(),
                        PayTypeValueId = c.Long(nullable: false),
                        InAppPayment = c.Byte(nullable: false),
                        PayDate = c.DateTime(nullable: false),
                        PayPDate = c.String(maxLength: 10),
                        PayTime = c.Time(nullable: false, precision: 7),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedBy_Id = c.String(maxLength: 128),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        PurchaseOrder_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrder_Id, cascadeDelete: true)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PurchaseOrder_Id);
            
            CreateTable(
                "dbo.MainProductGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GroupName = c.String(maxLength: 200),
                        NLeft = c.Int(nullable: false),
                        NRight = c.Int(nullable: false),
                        NLevel = c.Int(nullable: false),
                        Priority = c.Int(),
                        ProductGroup2Id = c.Guid(),
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
                .ForeignKey("dbo.MainProductGroups", t => t.ProductGroup2Id)
                .Index(t => t.ProductGroup2Id)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StockProductRequestRules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockProductRequestRuleName = c.String(maxLength: 200),
                        FromDate = c.DateTime(nullable: false),
                        FromPDate = c.String(maxLength: 10),
                        ToDate = c.DateTime(nullable: false),
                        ToPDate = c.String(maxLength: 10),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Guid(),
                        MainProductGroupId = c.Guid(),
                        ProductTypeId = c.Guid(),
                        SupplierId = c.Guid(),
                        ReorderCalcTypeId = c.Guid(nullable: false),
                        StockProductRequestRuleCalcTypeId = c.Guid(nullable: false),
                        StockProductRequestRuleTypeId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeId)
                .ForeignKey("dbo.ReorderCalcTypes", t => t.ReorderCalcTypeId)
                .ForeignKey("dbo.StockProductRequestRuleCalcTypes", t => t.StockProductRequestRuleCalcTypeId, cascadeDelete: true)
                .ForeignKey("dbo.StockProductRequestRuleTypes", t => t.StockProductRequestRuleTypeId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .ForeignKey("dbo.MainProductGroups", t => t.MainProductGroupId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.MainProductGroupId)
                .Index(t => t.ProductTypeId)
                .Index(t => t.SupplierId)
                .Index(t => t.ReorderCalcTypeId)
                .Index(t => t.StockProductRequestRuleCalcTypeId)
                .Index(t => t.StockProductRequestRuleTypeId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductTypeName = c.String(maxLength: 100),
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
                "dbo.StockProductRequests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        RequestPDate = c.String(maxLength: 10),
                        Accept1Date = c.DateTime(),
                        Accept1PDate = c.String(maxLength: 10),
                        Accept2Date = c.DateTime(),
                        Accept2PDate = c.String(maxLength: 10),
                        Accept3Date = c.DateTime(),
                        Accept3PDate = c.String(maxLength: 10),
                        SendToSourceStockDate = c.DateTime(),
                        SendToSourceStockDatePDate = c.String(maxLength: 10),
                        SourceStockRequestId = c.Guid(),
                        SourceStockRequestNo = c.String(maxLength: 50),
                        TargetStockIssueDate = c.DateTime(),
                        TargetStockIssueDatePDate = c.String(maxLength: 10),
                        TargetStockPaperId = c.Guid(),
                        TargetStockPaperNo = c.String(maxLength: 50),
                        StockProductRequestStatusId = c.Guid(nullable: false),
                        StockId = c.Guid(nullable: false),
                        SupplyByStockId = c.Guid(),
                        StockTypeId = c.Guid(nullable: false),
                        SupplierId = c.Guid(),
                        StockProductRequestTypeId = c.Guid(nullable: false),
                        PorductTypeId = c.Guid(nullable: false),
                        Accept1ById = c.Guid(),
                        Accept2ById = c.Guid(),
                        Accept3ById = c.Guid(),
                        StockOnHandSyncId = c.Guid(nullable: false),
                        StockProductRequestSupplyTypeId = c.Guid(nullable: false),
                        RequestNo = c.String(),
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
                .ForeignKey("dbo.ApplicationOwners", t => t.Accept1ById)
                .ForeignKey("dbo.ApplicationOwners", t => t.Accept2ById)
                .ForeignKey("dbo.ApplicationOwners", t => t.Accept3ById)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.StockOnHandSyncs", t => t.StockOnHandSyncId)
                .ForeignKey("dbo.StockProductRequestStatus", t => t.StockProductRequestStatusId)
                .ForeignKey("dbo.StockProductRequestSupplyTypes", t => t.StockProductRequestSupplyTypeId)
                .ForeignKey("dbo.StockProductRequestTypes", t => t.StockProductRequestTypeId)
                .ForeignKey("dbo.StockTypes", t => t.StockTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .ForeignKey("dbo.ProductTypes", t => t.PorductTypeId)
                .ForeignKey("dbo.Stocks", t => t.StockId)
                .ForeignKey("dbo.Stocks", t => t.SupplyByStockId)
                .Index(t => t.StockProductRequestStatusId)
                .Index(t => t.StockId)
                .Index(t => t.SupplyByStockId)
                .Index(t => t.StockTypeId)
                .Index(t => t.SupplierId)
                .Index(t => t.StockProductRequestTypeId)
                .Index(t => t.PorductTypeId)
                .Index(t => t.Accept1ById)
                .Index(t => t.Accept2ById)
                .Index(t => t.Accept3ById)
                .Index(t => t.StockOnHandSyncId)
                .Index(t => t.StockProductRequestSupplyTypeId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StockOnHandSyncs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SyncDate = c.DateTime(nullable: false),
                        SyncPDate = c.String(maxLength: 10),
                        StockId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Stocks", t => t.StockId)
                .Index(t => t.StockId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StockActiveOnHands",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        StockOnHandSyncId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .ForeignKey("dbo.StockOnHandSyncs", t => t.StockOnHandSyncId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.StockId)
                .Index(t => t.ProductId)
                .Index(t => t.StockOnHandSyncId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StockHistoryOnHands",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        StockOnHandSyncId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .ForeignKey("dbo.StockOnHandSyncs", t => t.StockOnHandSyncId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.StockId)
                .Index(t => t.ProductId)
                .Index(t => t.StockOnHandSyncId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StockProductRequestProducts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RequestQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Accepted1Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Accepted2Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Accepted3Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeliveredQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Guid(nullable: false),
                        StockProductRequestId = c.Guid(nullable: false),
                        StockLevelQty = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                .ForeignKey("dbo.StockProductRequests", t => t.StockProductRequestId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.StockProductRequestId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StockProductRequestProductDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RequestQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockProductRequestProductId = c.Guid(nullable: false),
                        StockProductRequestRuleId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.StockProductRequestProducts", t => t.StockProductRequestProductId, cascadeDelete: true)
                .ForeignKey("dbo.StockProductRequestRules", t => t.StockProductRequestRuleId)
                .Index(t => t.StockProductRequestProductId)
                .Index(t => t.StockProductRequestRuleId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StockProductRequestStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockProductRequestStatusName = c.String(maxLength: 100),
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
                "dbo.StockProductRequestSupplyTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockProductRequestSupplyTypeName = c.String(maxLength: 100),
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
                "dbo.StockProducts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MinQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReorderLevel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsEnable = c.Boolean(nullable: false),
                        StockId = c.Guid(nullable: false),
                        FiscalYearId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        ReorderCalcTypeId = c.Guid(),
                        StockProductRequestSupplyTypeId = c.Guid(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedBy_Id = c.String(maxLength: 128),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        StockProductRequestType_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.FiscalYears", t => t.FiscalYearId)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.ReorderCalcTypes", t => t.ReorderCalcTypeId)
                .ForeignKey("dbo.StockProductRequestSupplyTypes", t => t.StockProductRequestSupplyTypeId)
                .ForeignKey("dbo.StockProductRequestTypes", t => t.StockProductRequestType_Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Stocks", t => t.StockId)
                .Index(t => t.StockId)
                .Index(t => t.FiscalYearId)
                .Index(t => t.ProductId)
                .Index(t => t.ReorderCalcTypeId)
                .Index(t => t.StockProductRequestSupplyTypeId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.StockProductRequestType_Id);
            
            CreateTable(
                "dbo.FiscalYears",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FromPdate = c.String(maxLength: 10),
                        ToPdate = c.String(maxLength: 10),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
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
                "dbo.ReorderCalcTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ReorderTypeName = c.String(maxLength: 100),
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
                "dbo.StockProductRequestTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockProductRequestTypeName = c.String(maxLength: 100),
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
                "dbo.StockTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockTypeName = c.String(maxLength: 100),
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
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SupplierName = c.String(),
                        OrderAllProduct = c.Boolean(nullable: false),
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
                "dbo.StockProductRequestRuleCalcTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockProductRequestRuleCalcTypeName = c.String(maxLength: 100),
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
                "dbo.StockProductRequestRuleTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockProductRequestRuleTypeName = c.String(maxLength: 100),
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
                "dbo.Manufactures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ManufactureName = c.String(maxLength: 100),
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
                "dbo.ProductComments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CommentDate = c.DateTime(nullable: false),
                        CommentTime = c.Time(nullable: false, precision: 7),
                        Value = c.Int(nullable: false),
                        CommentBy = c.Guid(),
                        CommentByName = c.String(maxLength: 100),
                        CommentByEmailAddress = c.String(maxLength: 50),
                        IsApproved = c.Byte(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedBy_Id = c.String(maxLength: 128),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GroupName = c.String(maxLength: 200),
                        NLeft = c.Int(nullable: false),
                        NRight = c.Int(nullable: false),
                        NLevel = c.Int(nullable: false),
                        Priority = c.Int(),
                        ProductGroup2Id = c.Guid(),
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
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroup2Id)
                .Index(t => t.ProductGroup2Id)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.ProductPictures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PictureTypeValueGuid = c.Guid(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        ProductPictureName = c.String(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedBy_Id = c.String(maxLength: 128),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductRates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RateDate = c.DateTime(nullable: false),
                        RateTime = c.Time(nullable: false, precision: 7),
                        Value = c.Int(nullable: false),
                        RateBy = c.Guid(),
                        RateByName = c.String(),
                        ProductId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.ProductTagValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FromDate = c.DateTime(),
                        FromPDate = c.String(),
                        ToDate = c.DateTime(),
                        ToPDate = c.String(),
                        ProductTagId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.ProductTags", t => t.ProductTagId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductTagId)
                .Index(t => t.ProductId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.ProductTags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductTagName = c.String(maxLength: 100),
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
                "dbo.BasketNotes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Comment = c.String(maxLength: 500),
                        FullText = c.String(maxLength: 500),
                        DueDate = c.DateTime(),
                        DuePDate = c.String(maxLength: 10),
                        DueTime = c.Time(precision: 7),
                        IsCompleted = c.Byte(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedBy_Id = c.String(maxLength: 128),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        Basket_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Baskets", t => t.Basket_Id, cascadeDelete: true)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Basket_Id);
            
            CreateTable(
                "dbo.AnatoliPlaces",
                c => new
                    {
                        RegionInfoId = c.Guid(),
                        RegionLevel1Id = c.Guid(),
                        RegionLevel2Id = c.Guid(),
                        RegionLevel3Id = c.Guid(),
                        RegionLevel4Id = c.Guid(),
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        Phone = c.String(),
                        Mobile = c.String(maxLength: 20),
                        Email = c.String(maxLength: 500),
                        MainStreet = c.String(maxLength: 500),
                        OtherStreet = c.String(maxLength: 500),
                        PostalCode = c.String(maxLength: 20),
                        NationalCode = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliRegions", t => t.RegionInfoId)
                .ForeignKey("dbo.AnatoliRegions", t => t.RegionLevel1Id)
                .ForeignKey("dbo.AnatoliRegions", t => t.RegionLevel2Id)
                .ForeignKey("dbo.AnatoliRegions", t => t.RegionLevel3Id)
                .ForeignKey("dbo.AnatoliRegions", t => t.RegionLevel4Id)
                .Index(t => t.RegionInfoId)
                .Index(t => t.RegionLevel1Id)
                .Index(t => t.RegionLevel2Id)
                .Index(t => t.RegionLevel3Id)
                .Index(t => t.RegionLevel4Id);
            
            CreateTable(
                "dbo.AnatoliRegions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        GroupName = c.String(),
                        NLeft = c.Int(nullable: false),
                        NRight = c.Int(nullable: false),
                        NLevel = c.Int(nullable: false),
                        Priority = c.Int(),
                        AnatoliRegion2Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnatoliRegions", t => t.AnatoliRegion2Id)
                .Index(t => t.AnatoliRegion2Id);
            
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BankAccountNo = c.String(maxLength: 50),
                        BankAccountName = c.String(maxLength: 200),
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
                "dbo.BaseTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BaseTypeDesc = c.String(maxLength: 500),
                        BaseTypeName = c.String(maxLength: 100),
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
                "dbo.BaseValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BaseValueName = c.String(maxLength: 200),
                        BaseTypeId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.BaseTypes", t => t.BaseTypeId, cascadeDelete: true)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .Index(t => t.BaseTypeId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Clearances",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ClearanceDate = c.DateTime(),
                        ClearancePDate = c.String(maxLength: 10),
                        ClearanceTime = c.Time(precision: 7),
                        CashierId = c.Int(),
                        CashSessionId = c.Int(),
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
                "dbo.DiscountCodes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DiscountCodeUniqueId = c.Guid(nullable: false),
                        DiscountDesc = c.String(maxLength: 200),
                        DiscountTypeValueId = c.Long(),
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
                "dbo.CompanyRegionLevelTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyRegionLevelTypeName = c.String(maxLength: 100),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.CompanyCenters", t => t.CompanyCenterId, cascadeDelete: true)
                .ForeignKey("dbo.CompanyRegions", t => t.ParentId)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.CompanyRegionLevelTypes", t => t.CompanyRegionLevelTypeId)
                .Index(t => t.ParentId)
                .Index(t => t.CompanyRegionLevelTypeId)
                .Index(t => t.CompanyCenterId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.CompanyRegions", t => t.CompanyRegionId, cascadeDelete: true)
                .Index(t => t.CompanyRegionId)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        ApplicationOwnerId = c.Guid(nullable: false),
                        DataOwnerId = c.Guid(nullable: false),
                        DataOwnerCenterId = c.Guid(nullable: false),
                        AddedBy_Id = c.String(maxLength: 128),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        Manager_Id = c.Guid(),
                        Principal_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AddedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.ApplicationOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwners", t => t.DataOwnerId, cascadeDelete: false)
                .ForeignKey("dbo.DataOwnerCenters", t => t.DataOwnerCenterId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.Manager_Id)
                .ForeignKey("dbo.ApplicationOwners", t => t.Principal_Id, cascadeDelete: true)
                .Index(t => t.ApplicationOwnerId)
                .Index(t => t.DataOwnerId)
                .Index(t => t.DataOwnerCenterId)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Manager_Id)
                .Index(t => t.Principal_Id);
            
            CreateTable(
                "dbo.ItemImages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TokenId = c.String(maxLength: 50),
                        ImageName = c.String(maxLength: 100),
                        ImageType = c.String(maxLength: 50),
                        IsDefault = c.Boolean(nullable: false),
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
                "dbo.ProductSupplierGuarantees",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SupplierProductGuaranteeId = c.Int(nullable: false),
                        GuaranteeTypeValueId = c.Long(nullable: false),
                        GuaranteeDuration = c.Int(nullable: false),
                        GuaranteeDesc = c.String(maxLength: 200),
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
                "dbo.CharGroupTypes",
                c => new
                    {
                        CharTypeId = c.Guid(nullable: false),
                        CharGroupID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CharTypeId, t.CharGroupID })
                .ForeignKey("dbo.CharTypes", t => t.CharTypeId, cascadeDelete: true)
                .ForeignKey("dbo.CharGroups", t => t.CharGroupID, cascadeDelete: true)
                .Index(t => t.CharTypeId)
                .Index(t => t.CharGroupID);
            
            CreateTable(
                "dbo.ProductChars",
                c => new
                    {
                        ProductId = c.Guid(nullable: false),
                        CharValueID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.CharValueID })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.CharValues", t => t.CharValueID, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CharValueID);
            
            CreateTable(
                "dbo.StoreValidRegionInfoes",
                c => new
                    {
                        StoreId = c.Guid(nullable: false),
                        CityRegionID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.StoreId, t.CityRegionID })
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .ForeignKey("dbo.CityRegions", t => t.CityRegionID, cascadeDelete: true)
                .Index(t => t.StoreId)
                .Index(t => t.CityRegionID);
            
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
            
            CreateTable(
                "dbo.UsersStocks",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        StockID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.StockID })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockID, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.StockID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.ProductSupplierGuarantees", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductSupplierGuarantees", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.ProductSupplierGuarantees", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.ProductSupplierGuarantees", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.ProductSupplierGuarantees", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ItemImages", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ItemImages", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.ItemImages", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.ItemImages", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.ItemImages", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Groups", "Principal_Id", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Groups", "Manager_Id", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Groups", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Groups", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Groups", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.Groups", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Groups", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CompanyRegionLevelTypes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CompanyRegions", "CompanyRegionLevelTypeId", "dbo.CompanyRegionLevelTypes");
            DropForeignKey("dbo.CompanyRegions", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CompanyRegions", "ParentId", "dbo.CompanyRegions");
            DropForeignKey("dbo.CompanyRegionPolygons", "CompanyRegionId", "dbo.CompanyRegions");
            DropForeignKey("dbo.CompanyRegionPolygons", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CompanyRegionPolygons", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CompanyRegionPolygons", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CompanyRegionPolygons", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CompanyRegionPolygons", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CompanyRegions", "CompanyCenterId", "dbo.CompanyCenters");
            DropForeignKey("dbo.CompanyRegions", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CompanyRegions", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CompanyRegions", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CompanyRegions", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CompanyRegionLevelTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CompanyRegionLevelTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CompanyRegionLevelTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CompanyRegionLevelTypes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.DiscountCodes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.DiscountCodes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.DiscountCodes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.DiscountCodes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.DiscountCodes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Clearances", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Clearances", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Clearances", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.Clearances", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Clearances", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.BaseTypes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.BaseTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.BaseTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.BaseValues", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.BaseValues", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.BaseValues", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.BaseValues", "BaseTypeId", "dbo.BaseTypes");
            DropForeignKey("dbo.BaseValues", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.BaseValues", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.BaseTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.BaseTypes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.BankAccounts", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.BankAccounts", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.BankAccounts", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.BankAccounts", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.BankAccounts", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.AnatoliAccounts", "AnatoliPlaceId", "dbo.AnatoliPlaces");
            DropForeignKey("dbo.AnatoliPlaces", "RegionLevel4Id", "dbo.AnatoliRegions");
            DropForeignKey("dbo.AnatoliPlaces", "RegionLevel3Id", "dbo.AnatoliRegions");
            DropForeignKey("dbo.AnatoliPlaces", "RegionLevel2Id", "dbo.AnatoliRegions");
            DropForeignKey("dbo.AnatoliPlaces", "RegionLevel1Id", "dbo.AnatoliRegions");
            DropForeignKey("dbo.AnatoliPlaces", "RegionInfoId", "dbo.AnatoliRegions");
            DropForeignKey("dbo.AnatoliRegions", "AnatoliRegion2Id", "dbo.AnatoliRegions");
            DropForeignKey("dbo.AnatoliAccounts", "AnatoliContactId", "dbo.AnatoliContacts");
            DropForeignKey("dbo.UsersStocks", "StockID", "dbo.Stocks");
            DropForeignKey("dbo.UsersStocks", "UserId", "dbo.Users");
            DropForeignKey("dbo.Stocks", "StockTypeId", "dbo.StockTypes");
            DropForeignKey("dbo.StockProducts", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockProductRequests", "SupplyByStockId", "dbo.Stocks");
            DropForeignKey("dbo.StockProductRequests", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockOnHandSyncs", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Stocks", "RelatedSCMStock2Id", "dbo.Stocks");
            DropForeignKey("dbo.Stocks", "MainSCMStock2Id", "dbo.Stocks");
            DropForeignKey("dbo.Stocks", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Stocks", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Stocks", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.Stores", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Stocks", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Companies", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CompanyCenters", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Companies", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Companies", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.Customers", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Customers", "RegionLevel3Id", "dbo.CityRegions");
            DropForeignKey("dbo.Customers", "RegionLevel2Id", "dbo.CityRegions");
            DropForeignKey("dbo.Customers", "RegionLevel1Id", "dbo.CityRegions");
            DropForeignKey("dbo.Customers", "RegionInfoId", "dbo.CityRegions");
            DropForeignKey("dbo.Customers", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.IncompletePurchaseOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "DefauleStoreId", "dbo.Stores");
            DropForeignKey("dbo.Customers", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Customers", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CustomerShipAddresses", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Baskets", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Baskets", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Baskets", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Baskets", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.BasketNotes", "Basket_Id", "dbo.Baskets");
            DropForeignKey("dbo.BasketNotes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.BasketNotes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.BasketNotes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.BasketNotes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.BasketNotes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropForeignKey("dbo.ProductSupliers", "SuplierID", "dbo.Suppliers");
            DropForeignKey("dbo.ProductSupliers", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StoreActivePriceLists", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockProductRequestRules", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockProductRequestProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockHistoryOnHands", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockActiveOnHands", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PurchaseOrderLineItems", "FinalProductId", "dbo.Products");
            DropForeignKey("dbo.ProductTagValues", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductTagValues", "ProductTagId", "dbo.ProductTags");
            DropForeignKey("dbo.ProductTags", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductTags", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.ProductTags", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.ProductTags", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.ProductTags", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductTagValues", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductTagValues", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.ProductTagValues", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.ProductTagValues", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.ProductTagValues", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductRates", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductRates", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductRates", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.ProductRates", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.ProductRates", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.ProductRates", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductPictures", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductPictures", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductPictures", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.ProductPictures", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.ProductPictures", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.ProductPictures", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductGroups", "ProductGroup2Id", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductGroups", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductGroups", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.ProductGroups", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.ProductGroups", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.ProductGroups", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductComments", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductComments", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductComments", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.ProductComments", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.ProductComments", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.ProductComments", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "ManufactureId", "dbo.Manufactures");
            DropForeignKey("dbo.Manufactures", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Manufactures", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Manufactures", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.Manufactures", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Manufactures", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "MainSupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.StockProductRequestRules", "MainProductGroupId", "dbo.MainProductGroups");
            DropForeignKey("dbo.StockProductRequestRules", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.StockProductRequestRules", "StockProductRequestRuleTypeId", "dbo.StockProductRequestRuleTypes");
            DropForeignKey("dbo.StockProductRequestRuleTypes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequestRuleTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StockProductRequestRuleTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StockProductRequestRuleTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockProductRequestRuleTypes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequestRules", "StockProductRequestRuleCalcTypeId", "dbo.StockProductRequestRuleCalcTypes");
            DropForeignKey("dbo.StockProductRequestRuleCalcTypes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequestRuleCalcTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StockProductRequestRuleCalcTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StockProductRequestRuleCalcTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockProductRequestRuleCalcTypes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequestProductDetails", "StockProductRequestRuleId", "dbo.StockProductRequestRules");
            DropForeignKey("dbo.StockProductRequests", "PorductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.StockProductRequests", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Suppliers", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Suppliers", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Suppliers", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.Suppliers", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Suppliers", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequests", "StockTypeId", "dbo.StockTypes");
            DropForeignKey("dbo.StockTypes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StockTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StockTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockTypes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProducts", "StockProductRequestType_Id", "dbo.StockProductRequestTypes");
            DropForeignKey("dbo.StockProductRequests", "StockProductRequestTypeId", "dbo.StockProductRequestTypes");
            DropForeignKey("dbo.StockProductRequestTypes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequestTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StockProductRequestTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StockProductRequestTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockProductRequestTypes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProducts", "StockProductRequestSupplyTypeId", "dbo.StockProductRequestSupplyTypes");
            DropForeignKey("dbo.StockProducts", "ReorderCalcTypeId", "dbo.ReorderCalcTypes");
            DropForeignKey("dbo.StockProductRequestRules", "ReorderCalcTypeId", "dbo.ReorderCalcTypes");
            DropForeignKey("dbo.ReorderCalcTypes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ReorderCalcTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.ReorderCalcTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.ReorderCalcTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.ReorderCalcTypes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProducts", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProducts", "FiscalYearId", "dbo.FiscalYears");
            DropForeignKey("dbo.FiscalYears", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.FiscalYears", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.FiscalYears", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.FiscalYears", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.FiscalYears", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProducts", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StockProducts", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StockProducts", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockProducts", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequests", "StockProductRequestSupplyTypeId", "dbo.StockProductRequestSupplyTypes");
            DropForeignKey("dbo.StockProductRequestSupplyTypes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequestSupplyTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StockProductRequestSupplyTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StockProductRequestSupplyTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockProductRequestSupplyTypes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequests", "StockProductRequestStatusId", "dbo.StockProductRequestStatus");
            DropForeignKey("dbo.StockProductRequestStatus", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequestStatus", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StockProductRequestStatus", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StockProductRequestStatus", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockProductRequestStatus", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequestProducts", "StockProductRequestId", "dbo.StockProductRequests");
            DropForeignKey("dbo.StockProductRequestProductDetails", "StockProductRequestProductId", "dbo.StockProductRequestProducts");
            DropForeignKey("dbo.StockProductRequestProductDetails", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequestProductDetails", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StockProductRequestProductDetails", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StockProductRequestProductDetails", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockProductRequestProductDetails", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequestProducts", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequestProducts", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StockProductRequestProducts", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StockProductRequestProducts", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockProductRequestProducts", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequests", "StockOnHandSyncId", "dbo.StockOnHandSyncs");
            DropForeignKey("dbo.StockHistoryOnHands", "StockOnHandSyncId", "dbo.StockOnHandSyncs");
            DropForeignKey("dbo.StockHistoryOnHands", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockHistoryOnHands", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockHistoryOnHands", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StockHistoryOnHands", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StockHistoryOnHands", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockHistoryOnHands", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockActiveOnHands", "StockOnHandSyncId", "dbo.StockOnHandSyncs");
            DropForeignKey("dbo.StockActiveOnHands", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockActiveOnHands", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockActiveOnHands", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StockActiveOnHands", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StockActiveOnHands", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockActiveOnHands", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockOnHandSyncs", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockOnHandSyncs", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StockOnHandSyncs", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StockOnHandSyncs", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockOnHandSyncs", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequests", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequests", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StockProductRequests", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StockProductRequests", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockProductRequests", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequests", "Accept3ById", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockProductRequests", "Accept2ById", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockProductRequests", "Accept1ById", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockProductRequestRules", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.ProductTypes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ProductTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.ProductTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.ProductTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.ProductTypes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequestRules", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StockProductRequestRules", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StockProductRequestRules", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StockProductRequestRules", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StockProductRequestRules", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "MainProductGroupId", "dbo.MainProductGroups");
            DropForeignKey("dbo.MainProductGroups", "ProductGroup2Id", "dbo.MainProductGroups");
            DropForeignKey("dbo.MainProductGroups", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.MainProductGroups", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.MainProductGroups", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.MainProductGroups", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.MainProductGroups", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.IncompletePurchaseOrders", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "IncompletePurchaseOrderId", "dbo.IncompletePurchaseOrders");
            DropForeignKey("dbo.IncompletePurchaseOrders", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.IncompletePurchaseOrders", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CustomerShipAddresses", "RegionLevel4Id", "dbo.CityRegions");
            DropForeignKey("dbo.CustomerShipAddresses", "RegionLevel3Id", "dbo.CityRegions");
            DropForeignKey("dbo.CustomerShipAddresses", "RegionLevel2Id", "dbo.CityRegions");
            DropForeignKey("dbo.CustomerShipAddresses", "RegionLevel1Id", "dbo.CityRegions");
            DropForeignKey("dbo.CustomerShipAddresses", "RegionInfoId", "dbo.CityRegions");
            DropForeignKey("dbo.PurchaseOrders", "CustomerShipAddressId", "dbo.CustomerShipAddresses");
            DropForeignKey("dbo.PurchaseOrders", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.PurchaseOrderPayments", "PurchaseOrder_Id", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrderPayments", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PurchaseOrderPayments", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PurchaseOrderPayments", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PurchaseOrderPayments", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PurchaseOrderPayments", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PurchaseOrderLineItems", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrderLineItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PurchaseOrderLineItems", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PurchaseOrderLineItems", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PurchaseOrderLineItems", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PurchaseOrderLineItems", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PurchaseOrderLineItems", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PurchaseOrderStatusHistories", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrderStatusHistories", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PurchaseOrderStatusHistories", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PurchaseOrderStatusHistories", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PurchaseOrderStatusHistories", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PurchaseOrderStatusHistories", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PurchaseOrderClearances", "PurchaseOrder_Id", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrderClearances", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PurchaseOrderClearances", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PurchaseOrderClearances", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PurchaseOrderClearances", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PurchaseOrderClearances", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PurchaseOrders", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PurchaseOrders", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.PurchaseOrders", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.PurchaseOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.PurchaseOrders", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PurchaseOrders", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CustomerShipAddresses", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.IncompletePurchaseOrders", "CustomerShipAddressId", "dbo.CustomerShipAddresses");
            DropForeignKey("dbo.CustomerShipAddresses", "DefauleStore_Id", "dbo.Stores");
            DropForeignKey("dbo.CustomerShipAddresses", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CustomerShipAddresses", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CustomerShipAddresses", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CustomerShipAddresses", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StoreValidRegionInfoes", "CityRegionID", "dbo.CityRegions");
            DropForeignKey("dbo.StoreValidRegionInfoes", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Stocks", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.StoreDeliveryPersons", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.StoreDeliveryPersons", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StoreDeliveryPersons", "DeliveryPerson_Id", "dbo.DeliveryPersons");
            DropForeignKey("dbo.DeliveryPersons", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.DeliveryPersons", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.DeliveryPersons", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.DeliveryPersons", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.DeliveryPersons", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StoreDeliveryPersons", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StoreDeliveryPersons", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StoreDeliveryPersons", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StoreDeliveryPersons", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StoreCalendars", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.StoreCalendarHistories", "StoreCalendar_Id", "dbo.StoreCalendars");
            DropForeignKey("dbo.StoreCalendarHistories", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StoreCalendarHistories", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StoreCalendarHistories", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StoreCalendarHistories", "CalendarTemplate_Id", "dbo.CalendarTemplates");
            DropForeignKey("dbo.CalendarTemplates", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CalendarTemplates", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CalendarTemplates", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "CalendarTemplate_Id", "dbo.CalendarTemplates");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CalendarTemplateHolidays", "CalendarTemplate_Id", "dbo.CalendarTemplates");
            DropForeignKey("dbo.CalendarTemplateHolidays", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CalendarTemplateHolidays", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CalendarTemplateHolidays", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CalendarTemplateHolidays", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CalendarTemplateHolidays", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CalendarTemplates", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CalendarTemplates", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StoreCalendarHistories", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StoreCalendarHistories", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StoreCalendars", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StoreCalendars", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StoreCalendars", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StoreCalendars", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StoreCalendars", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StoreActivePriceLists", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.StoreActivePriceLists", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StoreActivePriceLists", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StoreActivePriceLists", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StoreActivePriceLists", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StoreActivePriceLists", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StoreActiveOnhands", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.StoreActiveOnhands", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StoreActiveOnhands", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StoreActiveOnhands", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StoreActiveOnhands", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StoreActiveOnhands", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StoreActiveOnhands", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StoreActions", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.StoreActions", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.StoreActions", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.StoreActions", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.StoreActions", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.StoreActions", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Stores", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.IncompletePurchaseOrders", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Stores", "CompanyCenterId", "dbo.CompanyCenters");
            DropForeignKey("dbo.CompanyCenters", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CompanyCenters", "ParentId", "dbo.CompanyCenters");
            DropForeignKey("dbo.Stocks", "CompanyCenterId", "dbo.CompanyCenters");
            DropForeignKey("dbo.CompanyCenters", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CompanyCenters", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CompanyCenters", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CompanyCenters", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Stores", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Stores", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.Stores", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Stores", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CityRegions", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.IncompletePurchaseOrders", "CityRegionId", "dbo.CityRegions");
            DropForeignKey("dbo.CityRegions", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CityRegions", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.Customers", "RegionLevel4Id", "dbo.CityRegions");
            DropForeignKey("dbo.CityRegions", "CityRegion2Id", "dbo.CityRegions");
            DropForeignKey("dbo.CityRegions", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CityRegions", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.IncompletePurchaseOrders", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.IncompletePurchaseOrders", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Products", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.ProductChars", "CharValueID", "dbo.CharValues");
            DropForeignKey("dbo.ProductChars", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CharValues", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CharValues", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CharValues", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CharTypes", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CharTypes", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CharTypes", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CharValues", "CharTypeId", "dbo.CharTypes");
            DropForeignKey("dbo.CharGroupTypes", "CharGroupID", "dbo.CharGroups");
            DropForeignKey("dbo.CharGroupTypes", "CharTypeId", "dbo.CharTypes");
            DropForeignKey("dbo.CharGroups", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CharGroups", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.CharGroups", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.CharGroups", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CharGroups", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CharTypes", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CharTypes", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.CharValues", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.CharValues", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.BasketItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Products", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.BasketItems", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.BasketItems", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.BasketItems", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.BasketItems", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.BasketItems", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Baskets", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Baskets", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Customers", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Customers", "AnatoliAccountId", "dbo.AnatoliAccounts");
            DropForeignKey("dbo.Customers", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Companies", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Companies", "AnatoliAccountId", "dbo.AnatoliAccounts");
            DropForeignKey("dbo.Companies", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Stocks", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Stocks", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Stocks", "Accept3ById", "dbo.Users");
            DropForeignKey("dbo.Stocks", "Accept2ById", "dbo.Users");
            DropForeignKey("dbo.Stocks", "Accept1ById", "dbo.Users");
            DropForeignKey("dbo.IdentityUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.PrincipalPermissions", "UserId", "dbo.Users");
            DropForeignKey("dbo.PrincipalPermissions", "Permission_Id", "dbo.Permissions");
            DropForeignKey("dbo.Permissions", "Parent_Id", "dbo.Permissions");
            DropForeignKey("dbo.Permissions", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Permissions", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.Permissions", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.Permissions", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Permissions", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PrincipalPermissions", "LastModifiedBy_Id", "dbo.Users");
            DropForeignKey("dbo.PrincipalPermissions", "DataOwnerCenterId", "dbo.DataOwnerCenters");
            DropForeignKey("dbo.DataOwnerCenters", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.DataOwnerCenters", "AnatoliContactId", "dbo.AnatoliContacts");
            DropForeignKey("dbo.PrincipalPermissions", "DataOwnerId", "dbo.DataOwners");
            DropForeignKey("dbo.DataOwners", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.DataOwners", "AnatoliContactId", "dbo.AnatoliContacts");
            DropForeignKey("dbo.PrincipalPermissions", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.PrincipalPermissions", "AddedBy_Id", "dbo.Users");
            DropForeignKey("dbo.IdentityUserLogins", "User_Id", "dbo.Users");
            DropForeignKey("dbo.IdentityUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "ApplicationOwnerId", "dbo.ApplicationOwners");
            DropForeignKey("dbo.Users", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.IdentityRoles", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.ApplicationOwners", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.ApplicationOwners", "AnatoliContactId", "dbo.AnatoliContacts");
            DropForeignKey("dbo.Users", "AnatoliContactId", "dbo.AnatoliContacts");
            DropForeignKey("dbo.AnatoliContacts", "AnatoliContactTypeId", "dbo.AnatoliContactTypes");
            DropIndex("dbo.UsersStocks", new[] { "StockID" });
            DropIndex("dbo.UsersStocks", new[] { "UserId" });
            DropIndex("dbo.ProductSupliers", new[] { "SuplierID" });
            DropIndex("dbo.ProductSupliers", new[] { "ProductId" });
            DropIndex("dbo.StoreValidRegionInfoes", new[] { "CityRegionID" });
            DropIndex("dbo.StoreValidRegionInfoes", new[] { "StoreId" });
            DropIndex("dbo.ProductChars", new[] { "CharValueID" });
            DropIndex("dbo.ProductChars", new[] { "ProductId" });
            DropIndex("dbo.CharGroupTypes", new[] { "CharGroupID" });
            DropIndex("dbo.CharGroupTypes", new[] { "CharTypeId" });
            DropIndex("dbo.ProductSupplierGuarantees", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductSupplierGuarantees", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductSupplierGuarantees", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.ProductSupplierGuarantees", new[] { "DataOwnerId" });
            DropIndex("dbo.ProductSupplierGuarantees", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.ItemImages", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ItemImages", new[] { "AddedBy_Id" });
            DropIndex("dbo.ItemImages", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.ItemImages", new[] { "DataOwnerId" });
            DropIndex("dbo.ItemImages", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.Groups", new[] { "Principal_Id" });
            DropIndex("dbo.Groups", new[] { "Manager_Id" });
            DropIndex("dbo.Groups", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Groups", new[] { "AddedBy_Id" });
            DropIndex("dbo.Groups", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Groups", new[] { "DataOwnerId" });
            DropIndex("dbo.Groups", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CompanyRegionPolygons", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CompanyRegionPolygons", new[] { "AddedBy_Id" });
            DropIndex("dbo.CompanyRegionPolygons", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CompanyRegionPolygons", new[] { "DataOwnerId" });
            DropIndex("dbo.CompanyRegionPolygons", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CompanyRegionPolygons", new[] { "CompanyRegionId" });
            DropIndex("dbo.CompanyRegions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CompanyRegions", new[] { "AddedBy_Id" });
            DropIndex("dbo.CompanyRegions", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CompanyRegions", new[] { "DataOwnerId" });
            DropIndex("dbo.CompanyRegions", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CompanyRegions", new[] { "CompanyCenterId" });
            DropIndex("dbo.CompanyRegions", new[] { "CompanyRegionLevelTypeId" });
            DropIndex("dbo.CompanyRegions", new[] { "ParentId" });
            DropIndex("dbo.CompanyRegionLevelTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CompanyRegionLevelTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.CompanyRegionLevelTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CompanyRegionLevelTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.CompanyRegionLevelTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.DiscountCodes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.DiscountCodes", new[] { "AddedBy_Id" });
            DropIndex("dbo.DiscountCodes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.DiscountCodes", new[] { "DataOwnerId" });
            DropIndex("dbo.DiscountCodes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.Clearances", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Clearances", new[] { "AddedBy_Id" });
            DropIndex("dbo.Clearances", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Clearances", new[] { "DataOwnerId" });
            DropIndex("dbo.Clearances", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.BaseValues", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.BaseValues", new[] { "AddedBy_Id" });
            DropIndex("dbo.BaseValues", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.BaseValues", new[] { "DataOwnerId" });
            DropIndex("dbo.BaseValues", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.BaseValues", new[] { "BaseTypeId" });
            DropIndex("dbo.BaseTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.BaseTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.BaseTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.BaseTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.BaseTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.BankAccounts", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.BankAccounts", new[] { "AddedBy_Id" });
            DropIndex("dbo.BankAccounts", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.BankAccounts", new[] { "DataOwnerId" });
            DropIndex("dbo.BankAccounts", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.AnatoliRegions", new[] { "AnatoliRegion2Id" });
            DropIndex("dbo.AnatoliPlaces", new[] { "RegionLevel4Id" });
            DropIndex("dbo.AnatoliPlaces", new[] { "RegionLevel3Id" });
            DropIndex("dbo.AnatoliPlaces", new[] { "RegionLevel2Id" });
            DropIndex("dbo.AnatoliPlaces", new[] { "RegionLevel1Id" });
            DropIndex("dbo.AnatoliPlaces", new[] { "RegionInfoId" });
            DropIndex("dbo.BasketNotes", new[] { "Basket_Id" });
            DropIndex("dbo.BasketNotes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.BasketNotes", new[] { "AddedBy_Id" });
            DropIndex("dbo.BasketNotes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.BasketNotes", new[] { "DataOwnerId" });
            DropIndex("dbo.BasketNotes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.ProductTags", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductTags", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductTags", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.ProductTags", new[] { "DataOwnerId" });
            DropIndex("dbo.ProductTags", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.ProductTagValues", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductTagValues", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductTagValues", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.ProductTagValues", new[] { "DataOwnerId" });
            DropIndex("dbo.ProductTagValues", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.ProductTagValues", new[] { "ProductId" });
            DropIndex("dbo.ProductTagValues", new[] { "ProductTagId" });
            DropIndex("dbo.ProductRates", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductRates", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductRates", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.ProductRates", new[] { "DataOwnerId" });
            DropIndex("dbo.ProductRates", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.ProductRates", new[] { "ProductId" });
            DropIndex("dbo.ProductPictures", new[] { "Product_Id" });
            DropIndex("dbo.ProductPictures", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductPictures", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductPictures", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.ProductPictures", new[] { "DataOwnerId" });
            DropIndex("dbo.ProductPictures", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.ProductGroups", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductGroups", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductGroups", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.ProductGroups", new[] { "DataOwnerId" });
            DropIndex("dbo.ProductGroups", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.ProductGroups", new[] { "ProductGroup2Id" });
            DropIndex("dbo.ProductComments", new[] { "Product_Id" });
            DropIndex("dbo.ProductComments", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductComments", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductComments", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.ProductComments", new[] { "DataOwnerId" });
            DropIndex("dbo.ProductComments", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.Manufactures", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Manufactures", new[] { "AddedBy_Id" });
            DropIndex("dbo.Manufactures", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Manufactures", new[] { "DataOwnerId" });
            DropIndex("dbo.Manufactures", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockProductRequestRuleTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestRuleTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestRuleTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StockProductRequestRuleTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.StockProductRequestRuleTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockProductRequestRuleCalcTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestRuleCalcTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestRuleCalcTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StockProductRequestRuleCalcTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.StockProductRequestRuleCalcTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.Suppliers", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Suppliers", new[] { "AddedBy_Id" });
            DropIndex("dbo.Suppliers", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Suppliers", new[] { "DataOwnerId" });
            DropIndex("dbo.Suppliers", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StockTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.StockTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockProductRequestTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StockProductRequestTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.StockProductRequestTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.ReorderCalcTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ReorderCalcTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.ReorderCalcTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.ReorderCalcTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.ReorderCalcTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.FiscalYears", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.FiscalYears", new[] { "AddedBy_Id" });
            DropIndex("dbo.FiscalYears", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.FiscalYears", new[] { "DataOwnerId" });
            DropIndex("dbo.FiscalYears", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockProducts", new[] { "StockProductRequestType_Id" });
            DropIndex("dbo.StockProducts", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProducts", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProducts", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StockProducts", new[] { "DataOwnerId" });
            DropIndex("dbo.StockProducts", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockProducts", new[] { "StockProductRequestSupplyTypeId" });
            DropIndex("dbo.StockProducts", new[] { "ReorderCalcTypeId" });
            DropIndex("dbo.StockProducts", new[] { "ProductId" });
            DropIndex("dbo.StockProducts", new[] { "FiscalYearId" });
            DropIndex("dbo.StockProducts", new[] { "StockId" });
            DropIndex("dbo.StockProductRequestSupplyTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestSupplyTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestSupplyTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StockProductRequestSupplyTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.StockProductRequestSupplyTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockProductRequestStatus", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestStatus", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestStatus", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StockProductRequestStatus", new[] { "DataOwnerId" });
            DropIndex("dbo.StockProductRequestStatus", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "DataOwnerId" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "StockProductRequestRuleId" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "StockProductRequestProductId" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "DataOwnerId" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "StockProductRequestId" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "ProductId" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "DataOwnerId" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "StockOnHandSyncId" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "ProductId" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "StockId" });
            DropIndex("dbo.StockActiveOnHands", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockActiveOnHands", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockActiveOnHands", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StockActiveOnHands", new[] { "DataOwnerId" });
            DropIndex("dbo.StockActiveOnHands", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockActiveOnHands", new[] { "StockOnHandSyncId" });
            DropIndex("dbo.StockActiveOnHands", new[] { "ProductId" });
            DropIndex("dbo.StockActiveOnHands", new[] { "StockId" });
            DropIndex("dbo.StockOnHandSyncs", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockOnHandSyncs", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockOnHandSyncs", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StockOnHandSyncs", new[] { "DataOwnerId" });
            DropIndex("dbo.StockOnHandSyncs", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockOnHandSyncs", new[] { "StockId" });
            DropIndex("dbo.StockProductRequests", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequests", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequests", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StockProductRequests", new[] { "DataOwnerId" });
            DropIndex("dbo.StockProductRequests", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockProductRequests", new[] { "StockProductRequestSupplyTypeId" });
            DropIndex("dbo.StockProductRequests", new[] { "StockOnHandSyncId" });
            DropIndex("dbo.StockProductRequests", new[] { "Accept3ById" });
            DropIndex("dbo.StockProductRequests", new[] { "Accept2ById" });
            DropIndex("dbo.StockProductRequests", new[] { "Accept1ById" });
            DropIndex("dbo.StockProductRequests", new[] { "PorductTypeId" });
            DropIndex("dbo.StockProductRequests", new[] { "StockProductRequestTypeId" });
            DropIndex("dbo.StockProductRequests", new[] { "SupplierId" });
            DropIndex("dbo.StockProductRequests", new[] { "StockTypeId" });
            DropIndex("dbo.StockProductRequests", new[] { "SupplyByStockId" });
            DropIndex("dbo.StockProductRequests", new[] { "StockId" });
            DropIndex("dbo.StockProductRequests", new[] { "StockProductRequestStatusId" });
            DropIndex("dbo.ProductTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.ProductTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.ProductTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestRules", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestRules", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "DataOwnerId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "StockProductRequestRuleTypeId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "StockProductRequestRuleCalcTypeId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "ReorderCalcTypeId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "SupplierId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "ProductTypeId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "MainProductGroupId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "ProductId" });
            DropIndex("dbo.MainProductGroups", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.MainProductGroups", new[] { "AddedBy_Id" });
            DropIndex("dbo.MainProductGroups", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.MainProductGroups", new[] { "DataOwnerId" });
            DropIndex("dbo.MainProductGroups", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.MainProductGroups", new[] { "ProductGroup2Id" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "PurchaseOrder_Id" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "AddedBy_Id" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "DataOwnerId" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "AddedBy_Id" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "DataOwnerId" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "PurchaseOrderId" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "FinalProductId" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "ProductId" });
            DropIndex("dbo.PurchaseOrderStatusHistories", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PurchaseOrderStatusHistories", new[] { "AddedBy_Id" });
            DropIndex("dbo.PurchaseOrderStatusHistories", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PurchaseOrderStatusHistories", new[] { "DataOwnerId" });
            DropIndex("dbo.PurchaseOrderStatusHistories", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PurchaseOrderStatusHistories", new[] { "PurchaseOrderId" });
            DropIndex("dbo.PurchaseOrderClearances", new[] { "PurchaseOrder_Id" });
            DropIndex("dbo.PurchaseOrderClearances", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PurchaseOrderClearances", new[] { "AddedBy_Id" });
            DropIndex("dbo.PurchaseOrderClearances", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PurchaseOrderClearances", new[] { "DataOwnerId" });
            DropIndex("dbo.PurchaseOrderClearances", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PurchaseOrders", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "AddedBy_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PurchaseOrders", new[] { "DataOwnerId" });
            DropIndex("dbo.PurchaseOrders", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PurchaseOrders", new[] { "CustomerShipAddressId" });
            DropIndex("dbo.PurchaseOrders", new[] { "CustomerId" });
            DropIndex("dbo.PurchaseOrders", new[] { "StoreId" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "AddedBy_Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "DataOwnerId" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "CustomerId" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "DefauleStore_Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "RegionLevel4Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "RegionLevel3Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "RegionLevel2Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "RegionLevel1Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "RegionInfoId" });
            DropIndex("dbo.DeliveryPersons", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.DeliveryPersons", new[] { "AddedBy_Id" });
            DropIndex("dbo.DeliveryPersons", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.DeliveryPersons", new[] { "DataOwnerId" });
            DropIndex("dbo.DeliveryPersons", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "Store_Id" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "DeliveryPerson_Id" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "DataOwnerId" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CalendarTemplateOpenTimes", new[] { "CalendarTemplate_Id" });
            DropIndex("dbo.CalendarTemplateOpenTimes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CalendarTemplateOpenTimes", new[] { "AddedBy_Id" });
            DropIndex("dbo.CalendarTemplateOpenTimes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CalendarTemplateOpenTimes", new[] { "DataOwnerId" });
            DropIndex("dbo.CalendarTemplateOpenTimes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CalendarTemplateHolidays", new[] { "CalendarTemplate_Id" });
            DropIndex("dbo.CalendarTemplateHolidays", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CalendarTemplateHolidays", new[] { "AddedBy_Id" });
            DropIndex("dbo.CalendarTemplateHolidays", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CalendarTemplateHolidays", new[] { "DataOwnerId" });
            DropIndex("dbo.CalendarTemplateHolidays", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CalendarTemplates", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CalendarTemplates", new[] { "AddedBy_Id" });
            DropIndex("dbo.CalendarTemplates", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CalendarTemplates", new[] { "DataOwnerId" });
            DropIndex("dbo.CalendarTemplates", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "StoreCalendar_Id" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "CalendarTemplate_Id" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "DataOwnerId" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StoreCalendars", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreCalendars", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreCalendars", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StoreCalendars", new[] { "DataOwnerId" });
            DropIndex("dbo.StoreCalendars", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StoreCalendars", new[] { "StoreId" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "DataOwnerId" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "ProductId" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "StoreId" });
            DropIndex("dbo.StoreActiveOnhands", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreActiveOnhands", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreActiveOnhands", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StoreActiveOnhands", new[] { "DataOwnerId" });
            DropIndex("dbo.StoreActiveOnhands", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.StoreActiveOnhands", new[] { "ProductId" });
            DropIndex("dbo.StoreActiveOnhands", new[] { "StoreId" });
            DropIndex("dbo.StoreActions", new[] { "Store_Id" });
            DropIndex("dbo.StoreActions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreActions", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreActions", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.StoreActions", new[] { "DataOwnerId" });
            DropIndex("dbo.StoreActions", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CompanyCenters", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CompanyCenters", new[] { "AddedBy_Id" });
            DropIndex("dbo.CompanyCenters", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CompanyCenters", new[] { "DataOwnerId" });
            DropIndex("dbo.CompanyCenters", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CompanyCenters", new[] { "CompanyId" });
            DropIndex("dbo.CompanyCenters", new[] { "ParentId" });
            DropIndex("dbo.Stores", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Stores", new[] { "AddedBy_Id" });
            DropIndex("dbo.Stores", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Stores", new[] { "DataOwnerId" });
            DropIndex("dbo.Stores", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.Stores", new[] { "CompanyId" });
            DropIndex("dbo.Stores", new[] { "CompanyCenterId" });
            DropIndex("dbo.CityRegions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CityRegions", new[] { "AddedBy_Id" });
            DropIndex("dbo.CityRegions", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CityRegions", new[] { "DataOwnerId" });
            DropIndex("dbo.CityRegions", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CityRegions", new[] { "CityRegion2Id" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "AddedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "DataOwnerId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "CustomerShipAddressId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "CustomerId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "CityRegionId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "StoreId" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "AddedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "DataOwnerId" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "IncompletePurchaseOrderId" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "ProductId" });
            DropIndex("dbo.CharGroups", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CharGroups", new[] { "AddedBy_Id" });
            DropIndex("dbo.CharGroups", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CharGroups", new[] { "DataOwnerId" });
            DropIndex("dbo.CharGroups", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CharTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CharTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.CharTypes", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CharTypes", new[] { "DataOwnerId" });
            DropIndex("dbo.CharTypes", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CharValues", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CharValues", new[] { "AddedBy_Id" });
            DropIndex("dbo.CharValues", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.CharValues", new[] { "DataOwnerId" });
            DropIndex("dbo.CharValues", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.CharValues", new[] { "CharTypeId" });
            DropIndex("dbo.Products", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Products", new[] { "AddedBy_Id" });
            DropIndex("dbo.Products", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Products", new[] { "DataOwnerId" });
            DropIndex("dbo.Products", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.Products", new[] { "ProductTypeId" });
            DropIndex("dbo.Products", new[] { "MainSupplierId" });
            DropIndex("dbo.Products", new[] { "ManufactureId" });
            DropIndex("dbo.Products", new[] { "MainProductGroupId" });
            DropIndex("dbo.Products", new[] { "ProductGroupId" });
            DropIndex("dbo.BasketItems", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.BasketItems", new[] { "AddedBy_Id" });
            DropIndex("dbo.BasketItems", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.BasketItems", new[] { "DataOwnerId" });
            DropIndex("dbo.BasketItems", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            DropIndex("dbo.BasketItems", new[] { "ProductId" });
            DropIndex("dbo.Baskets", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Baskets", new[] { "AddedBy_Id" });
            DropIndex("dbo.Baskets", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Baskets", new[] { "DataOwnerId" });
            DropIndex("dbo.Baskets", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.Baskets", new[] { "CustomerId" });
            DropIndex("dbo.Customers", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Customers", new[] { "AddedBy_Id" });
            DropIndex("dbo.Customers", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Customers", new[] { "DataOwnerId" });
            DropIndex("dbo.Customers", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.Customers", new[] { "AnatoliAccountId" });
            DropIndex("dbo.Customers", new[] { "CompanyId" });
            DropIndex("dbo.Customers", new[] { "DefauleStoreId" });
            DropIndex("dbo.Customers", new[] { "RegionLevel4Id" });
            DropIndex("dbo.Customers", new[] { "RegionLevel3Id" });
            DropIndex("dbo.Customers", new[] { "RegionLevel2Id" });
            DropIndex("dbo.Customers", new[] { "RegionLevel1Id" });
            DropIndex("dbo.Customers", new[] { "RegionInfoId" });
            DropIndex("dbo.Companies", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Companies", new[] { "AddedBy_Id" });
            DropIndex("dbo.Companies", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Companies", new[] { "DataOwnerId" });
            DropIndex("dbo.Companies", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.Companies", new[] { "AnatoliAccountId" });
            DropIndex("dbo.Stocks", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Stocks", new[] { "AddedBy_Id" });
            DropIndex("dbo.Stocks", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Stocks", new[] { "DataOwnerId" });
            DropIndex("dbo.Stocks", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.Stocks", new[] { "CompanyId" });
            DropIndex("dbo.Stocks", new[] { "RelatedSCMStock2Id" });
            DropIndex("dbo.Stocks", new[] { "MainSCMStock2Id" });
            DropIndex("dbo.Stocks", new[] { "StockTypeId" });
            DropIndex("dbo.Stocks", new[] { "Accept3ById" });
            DropIndex("dbo.Stocks", new[] { "Accept2ById" });
            DropIndex("dbo.Stocks", new[] { "Accept1ById" });
            DropIndex("dbo.Stocks", new[] { "CompanyCenterId" });
            DropIndex("dbo.Stocks", new[] { "StoreId" });
            DropIndex("dbo.Permissions", new[] { "Parent_Id" });
            DropIndex("dbo.Permissions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Permissions", new[] { "AddedBy_Id" });
            DropIndex("dbo.Permissions", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.Permissions", new[] { "DataOwnerId" });
            DropIndex("dbo.Permissions", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.DataOwnerCenters", new[] { "DataOwnerId" });
            DropIndex("dbo.DataOwnerCenters", new[] { "AnatoliContactId" });
            DropIndex("dbo.DataOwners", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.DataOwners", new[] { "AnatoliContactId" });
            DropIndex("dbo.PrincipalPermissions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PrincipalPermissions", new[] { "AddedBy_Id" });
            DropIndex("dbo.PrincipalPermissions", new[] { "DataOwnerCenterId" });
            DropIndex("dbo.PrincipalPermissions", new[] { "DataOwnerId" });
            DropIndex("dbo.PrincipalPermissions", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.PrincipalPermissions", new[] { "UserId" });
            DropIndex("dbo.PrincipalPermissions", new[] { "Permission_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "User_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "UserId" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "UserId" });
            DropIndex("dbo.IdentityRoles", new[] { "ApplicationId" });
            DropIndex("dbo.ApplicationOwners", new[] { "ApplicationId" });
            DropIndex("dbo.ApplicationOwners", new[] { "AnatoliContactId" });
            DropIndex("dbo.Users", new[] { "Group_Id" });
            DropIndex("dbo.Users", new[] { "ApplicationOwnerId" });
            DropIndex("dbo.Users", new[] { "AnatoliContactId" });
            DropIndex("dbo.Users", new[] { "ApplicationId" });
            DropIndex("dbo.AnatoliContacts", new[] { "AnatoliContactTypeId" });
            DropIndex("dbo.AnatoliAccounts", new[] { "AnatoliContactId" });
            DropIndex("dbo.AnatoliAccounts", new[] { "AnatoliPlaceId" });
            DropTable("dbo.UsersStocks");
            DropTable("dbo.ProductSupliers");
            DropTable("dbo.StoreValidRegionInfoes");
            DropTable("dbo.ProductChars");
            DropTable("dbo.CharGroupTypes");
            DropTable("dbo.ProductSupplierGuarantees");
            DropTable("dbo.ItemImages");
            DropTable("dbo.Groups");
            DropTable("dbo.CompanyRegionPolygons");
            DropTable("dbo.CompanyRegions");
            DropTable("dbo.CompanyRegionLevelTypes");
            DropTable("dbo.DiscountCodes");
            DropTable("dbo.Clearances");
            DropTable("dbo.BaseValues");
            DropTable("dbo.BaseTypes");
            DropTable("dbo.BankAccounts");
            DropTable("dbo.AnatoliRegions");
            DropTable("dbo.AnatoliPlaces");
            DropTable("dbo.BasketNotes");
            DropTable("dbo.ProductTags");
            DropTable("dbo.ProductTagValues");
            DropTable("dbo.ProductRates");
            DropTable("dbo.ProductPictures");
            DropTable("dbo.ProductGroups");
            DropTable("dbo.ProductComments");
            DropTable("dbo.Manufactures");
            DropTable("dbo.StockProductRequestRuleTypes");
            DropTable("dbo.StockProductRequestRuleCalcTypes");
            DropTable("dbo.Suppliers");
            DropTable("dbo.StockTypes");
            DropTable("dbo.StockProductRequestTypes");
            DropTable("dbo.ReorderCalcTypes");
            DropTable("dbo.FiscalYears");
            DropTable("dbo.StockProducts");
            DropTable("dbo.StockProductRequestSupplyTypes");
            DropTable("dbo.StockProductRequestStatus");
            DropTable("dbo.StockProductRequestProductDetails");
            DropTable("dbo.StockProductRequestProducts");
            DropTable("dbo.StockHistoryOnHands");
            DropTable("dbo.StockActiveOnHands");
            DropTable("dbo.StockOnHandSyncs");
            DropTable("dbo.StockProductRequests");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.StockProductRequestRules");
            DropTable("dbo.MainProductGroups");
            DropTable("dbo.PurchaseOrderPayments");
            DropTable("dbo.PurchaseOrderLineItems");
            DropTable("dbo.PurchaseOrderStatusHistories");
            DropTable("dbo.PurchaseOrderClearances");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.CustomerShipAddresses");
            DropTable("dbo.DeliveryPersons");
            DropTable("dbo.StoreDeliveryPersons");
            DropTable("dbo.CalendarTemplateOpenTimes");
            DropTable("dbo.CalendarTemplateHolidays");
            DropTable("dbo.CalendarTemplates");
            DropTable("dbo.StoreCalendarHistories");
            DropTable("dbo.StoreCalendars");
            DropTable("dbo.StoreActivePriceLists");
            DropTable("dbo.StoreActiveOnhands");
            DropTable("dbo.StoreActions");
            DropTable("dbo.CompanyCenters");
            DropTable("dbo.Stores");
            DropTable("dbo.CityRegions");
            DropTable("dbo.IncompletePurchaseOrders");
            DropTable("dbo.IncompletePurchaseOrderLineItems");
            DropTable("dbo.CharGroups");
            DropTable("dbo.CharTypes");
            DropTable("dbo.CharValues");
            DropTable("dbo.Products");
            DropTable("dbo.BasketItems");
            DropTable("dbo.Baskets");
            DropTable("dbo.Customers");
            DropTable("dbo.Companies");
            DropTable("dbo.Stocks");
            DropTable("dbo.Permissions");
            DropTable("dbo.DataOwnerCenters");
            DropTable("dbo.DataOwners");
            DropTable("dbo.PrincipalPermissions");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.ApplicationOwners");
            DropTable("dbo.Applications");
            DropTable("dbo.Users");
            DropTable("dbo.AnatoliContactTypes");
            DropTable("dbo.AnatoliContacts");
            DropTable("dbo.AnatoliAccounts");
        }
    }
}
