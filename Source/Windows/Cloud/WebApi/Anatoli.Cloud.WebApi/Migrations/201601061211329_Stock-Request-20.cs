namespace Anatoli.Cloud.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockRequest20 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BankAccountNo = c.String(),
                        BankAccountName = c.String(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Principals",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrincipalPermissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Grant = c.Boolean(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        Permission_Id = c.Guid(nullable: false),
                        Principal_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.Principal_Id, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Permission_Id)
                .Index(t => t.Principal_Id);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Resource = c.String(),
                        Action = c.String(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.BaseTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BaseTypeDesc = c.String(),
                        BaseTypeName = c.String(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.BaseValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BaseValueName = c.String(),
                        BaseTypeId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.BaseTypes", t => t.BaseTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.BaseTypeId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        Qty = c.Int(),
                        Comment = c.String(),
                        BasketId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Baskets", t => t.BasketId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.BasketId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BasketTypeValueGuid = c.Guid(nullable: false),
                        BasketName = c.String(),
                        CustomerId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.BasketNotes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Comment = c.String(),
                        FullText = c.String(),
                        DueDate = c.DateTime(),
                        DuePDate = c.String(),
                        DueTime = c.Time(precision: 7),
                        IsCompleted = c.Byte(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        Basket_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Baskets", t => t.Basket_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Basket_Id);
            
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
                        CustomerName = c.String(),
                        BirthDay = c.DateTime(),
                        Phone = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        PostalCode = c.String(),
                        NationalCode = c.String(),
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
                .ForeignKey("dbo.CityRegions", t => t.RegionLevel4Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.CityRegions", t => t.RegionInfoId)
                .ForeignKey("dbo.CityRegions", t => t.RegionLevel1Id)
                .ForeignKey("dbo.CityRegions", t => t.RegionLevel2Id)
                .ForeignKey("dbo.CityRegions", t => t.RegionLevel3Id)
                .Index(t => t.RegionInfoId)
                .Index(t => t.RegionLevel1Id)
                .Index(t => t.RegionLevel2Id)
                .Index(t => t.RegionLevel3Id)
                .Index(t => t.RegionLevel4Id)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.CustomerShipAddresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AddressName = c.String(),
                        Phone = c.String(),
                        Mobile = c.Long(),
                        Email = c.String(),
                        StateId = c.Long(),
                        CityId = c.Long(),
                        ZoneId = c.Long(),
                        MainStreet = c.String(),
                        OtherStreet = c.String(),
                        PostalCode = c.String(),
                        IsDefault = c.Byte(),
                        DefauleStoreId = c.Int(),
                        Lat = c.Long(),
                        Lng = c.Long(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        Customer_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Customer_Id);
            
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
                        Transferee = c.String(),
                        Phone = c.String(),
                        DeliveryFromTime = c.DateTime(),
                        DeliveryToTime = c.DateTime(),
                        DeliveryDate = c.DateTime(),
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
                .ForeignKey("dbo.CityRegions", t => t.CityRegionId, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.StoreId)
                .Index(t => t.CityRegionId)
                .Index(t => t.CustomerId)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.CityRegions", t => t.CityRegion2Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.CityRegion2Id)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StoreCode = c.Int(nullable: false),
                        StoreName = c.String(),
                        Address = c.String(),
                        Lat = c.Long(nullable: false),
                        Lng = c.Long(nullable: false),
                        HasDelivery = c.Byte(nullable: false),
                        GradeValueId = c.Int(),
                        StoreTemplateId = c.Int(),
                        HasCourier = c.Byte(nullable: false),
                        SupportAppOrder = c.Byte(nullable: false),
                        SupportWebOrder = c.Byte(nullable: false),
                        SupportCallCenterOrder = c.Byte(nullable: false),
                        StoreStatusTypeId = c.Byte(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StoreActions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StoreActionValueId = c.Long(nullable: false),
                        ActionDate = c.DateTime(nullable: false),
                        ActionPDate = c.String(),
                        ActionTime = c.Time(precision: 7),
                        ActionDesc = c.String(),
                        ActionDataId = c.Int(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        Store_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.Store_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId)
                .Index(t => t.ProductId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductCode = c.String(),
                        ProductName = c.String(),
                        StoreProductName = c.String(),
                        PackVolume = c.Decimal(precision: 18, scale: 2),
                        PackWeight = c.Decimal(precision: 18, scale: 2),
                        QtyPerPack = c.Decimal(precision: 18, scale: 2),
                        TaxCategoryValueId = c.Long(),
                        Desctription = c.String(),
                        ProductGroupId = c.Guid(),
                        MainProductGroupId = c.Guid(),
                        ManufactureId = c.Guid(),
                        MainSuppliereId = c.Guid(),
                        ProductTypeId = c.Guid(),
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
                .ForeignKey("dbo.MainProductGroups", t => t.MainProductGroupId)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeId)
                .ForeignKey("dbo.Suppliers", t => t.MainSuppliereId)
                .ForeignKey("dbo.Manufactures", t => t.ManufactureId)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId)
                .Index(t => t.ProductGroupId)
                .Index(t => t.MainProductGroupId)
                .Index(t => t.ManufactureId)
                .Index(t => t.MainSuppliereId)
                .Index(t => t.ProductTypeId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.CharValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CharValueText = c.String(),
                        CharValueFromAmount = c.Decimal(precision: 18, scale: 2),
                        CharValueToAmount = c.Decimal(precision: 18, scale: 2),
                        CharTypeId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.CharTypes", t => t.CharTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.CharTypeId)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.CharGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CharGroupCode = c.Int(nullable: false),
                        CharGroupName = c.String(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.IncompletePurchaseOrders", t => t.IncompletePurchaseOrderId)
                .Index(t => t.ProductId)
                .Index(t => t.IncompletePurchaseOrderId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.MainProductGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GroupName = c.String(),
                        NLeft = c.Int(nullable: false),
                        NRight = c.Int(nullable: false),
                        NLevel = c.Int(nullable: false),
                        Priority = c.Int(),
                        ProductGroup2Id = c.Guid(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.MainProductGroups", t => t.ProductGroup2Id)
                .Index(t => t.ProductGroup2Id)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StockProductRequestRules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockProductRequestRuleName = c.String(),
                        FromDate = c.DateTime(nullable: false),
                        FromPDate = c.String(),
                        ToDate = c.DateTime(nullable: false),
                        ToPDate = c.String(),
                        ProductId = c.Guid(nullable: false),
                        MainProductGroupId = c.Guid(),
                        ProductTypeId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeId)
                .ForeignKey("dbo.MainProductGroups", t => t.MainProductGroupId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.MainProductGroupId)
                .Index(t => t.ProductTypeId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductTypeName = c.String(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StockProductRequests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        RequestPDate = c.String(),
                        Accept1Date = c.DateTime(),
                        Accept1PDate = c.String(),
                        Accept2Date = c.DateTime(),
                        Accept2PDate = c.String(),
                        Accept3Date = c.DateTime(),
                        Accept3PDate = c.String(),
                        SendtoSourceStockDate = c.DateTime(),
                        SendtoSourceStockDatePDate = c.String(),
                        SrouceStockRequestId = c.Guid(),
                        SrouceStockRequestNo = c.String(),
                        TargetStockIssueDate = c.DateTime(),
                        TargetStockIssueDatePDate = c.String(),
                        TargetStockPaperId = c.Guid(),
                        TargetStockPaperNo = c.String(),
                        StockProductRequestStatusId = c.Guid(nullable: false),
                        StockId = c.Guid(nullable: false),
                        StockTypeId = c.Guid(nullable: false),
                        SupplierId = c.Guid(),
                        ReorderCalcTypeId = c.Guid(nullable: false),
                        StockProductRequestTypeId = c.Guid(nullable: false),
                        PorductTypeId = c.Guid(nullable: false),
                        Accept1ById = c.Guid(nullable: false),
                        Accept2ById = c.Guid(),
                        Accept3ById = c.Guid(),
                        StockOnHandSyncId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.Accept1ById, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.Accept2ById)
                .ForeignKey("dbo.Principals", t => t.Accept3ById)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.ReorderCalcTypes", t => t.ReorderCalcTypeId, cascadeDelete: true)
                .ForeignKey("dbo.StockOnHandSyncs", t => t.StockOnHandSyncId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId)
                .ForeignKey("dbo.StockProductRequestStatus", t => t.StockProductRequestStatusId)
                .ForeignKey("dbo.StockProductRequestTypes", t => t.StockProductRequestTypeId)
                .ForeignKey("dbo.StockTypes", t => t.StockTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .ForeignKey("dbo.ProductTypes", t => t.PorductTypeId)
                .Index(t => t.StockProductRequestStatusId)
                .Index(t => t.StockId)
                .Index(t => t.StockTypeId)
                .Index(t => t.SupplierId)
                .Index(t => t.ReorderCalcTypeId)
                .Index(t => t.StockProductRequestTypeId)
                .Index(t => t.PorductTypeId)
                .Index(t => t.Accept1ById)
                .Index(t => t.Accept2ById)
                .Index(t => t.Accept3ById)
                .Index(t => t.StockOnHandSyncId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.ReorderCalcTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ReorderTypeName = c.String(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
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
                .ForeignKey("dbo.FiscalYears", t => t.FiscalYearId)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.ReorderCalcTypes", t => t.ReorderCalcTypeId)
                .ForeignKey("dbo.Stocks", t => t.StockId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.StockId)
                .Index(t => t.FiscalYearId)
                .Index(t => t.ProductId)
                .Index(t => t.ReorderCalcTypeId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.FiscalYears",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FromPdate = c.String(),
                        ToPdate = c.String(),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockCode = c.Int(nullable: false),
                        StockName = c.String(),
                        Address = c.String(),
                        StoreId = c.Guid(nullable: false),
                        Accept1ById = c.Guid(),
                        Accept2ById = c.Guid(),
                        Accept3ById = c.Guid(),
                        StockTypeId = c.Guid(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.Accept1ById)
                .ForeignKey("dbo.Principals", t => t.Accept2ById)
                .ForeignKey("dbo.Principals", t => t.Accept3ById)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.StockTypes", t => t.StockTypeId)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId)
                .Index(t => t.Accept1ById)
                .Index(t => t.Accept2ById)
                .Index(t => t.Accept3ById)
                .Index(t => t.StockTypeId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StockOnHandSyncs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SyncDate = c.DateTime(nullable: false),
                        SyncPDate = c.String(),
                        StockId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId)
                .Index(t => t.StockId)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .ForeignKey("dbo.StockOnHandSyncs", t => t.StockOnHandSyncId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.StockId)
                .Index(t => t.ProductId)
                .Index(t => t.StockOnHandSyncId)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .ForeignKey("dbo.StockOnHandSyncs", t => t.StockOnHandSyncId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.StockId)
                .Index(t => t.ProductId)
                .Index(t => t.StockOnHandSyncId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StockTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockTypeName = c.String(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.StockProductRequests", t => t.StockProductRequestId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.StockProductRequestId)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.StockProductRequestProducts", t => t.StockProductRequestProductId, cascadeDelete: true)
                .ForeignKey("dbo.StockProductRequestRules", t => t.StockProductRequestRuleId, cascadeDelete: true)
                .Index(t => t.StockProductRequestProductId)
                .Index(t => t.StockProductRequestRuleId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StockProductRequestStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockProductRequestStatusName = c.String(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StockProductRequestTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockPorductRequestTypeName = c.String(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Manufactures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ManufactureName = c.String(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        CommentByName = c.String(),
                        CommentByEmailAddress = c.String(),
                        IsApproved = c.Byte(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GroupName = c.String(),
                        NLeft = c.Int(nullable: false),
                        NRight = c.Int(nullable: false),
                        NLevel = c.Int(nullable: false),
                        Priority = c.Int(),
                        ProductGroup2Id = c.Guid(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroup2Id)
                .Index(t => t.ProductGroup2Id)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        Add1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Add2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsPrize = c.Byte(nullable: false),
                        Comment = c.String(),
                        AllowReplace = c.Byte(nullable: false),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalUnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalAdd1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalAdd2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalIsPrize = c.Byte(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        FinalProduct_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PurchaseOrder_Id = c.Guid(nullable: false),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Products", t => t.FinalProduct_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrder_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.FinalProduct_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PurchaseOrder_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ActionSourceValueId = c.Long(nullable: false),
                        DeviceIMEI = c.String(),
                        OrderDate = c.DateTime(),
                        OrderPDate = c.String(),
                        OrderTime = c.Time(precision: 7),
                        PaymentTypeValueId = c.Long(nullable: false),
                        DiscountCodeId = c.String(),
                        AppOrderNo = c.Long(nullable: false),
                        DiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Add1Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Add2Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherAdd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherSub = c.Decimal(precision: 18, scale: 2),
                        Comment = c.String(),
                        DeliveryTypeValueId = c.Long(nullable: false),
                        DeliveryDate = c.DateTime(),
                        DeliveryPDate = c.String(),
                        DeliveryFromTime = c.Time(precision: 7),
                        DeliveryToTime = c.Time(precision: 7),
                        PurchaseOrderStatusValueId = c.Long(nullable: false),
                        DiscountFinalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Add1FinalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Add2FinalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingFinalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalFinalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherFinalAdd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherFinalSub = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsCancelled = c.Byte(),
                        CancelReasonValueId = c.Long(),
                        CancelDesc = c.String(),
                        BackOfficeId = c.Int(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        Basket_Id = c.Guid(),
                        Customer_Id = c.Guid(),
                        CustomerShipAddress_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        Store_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Baskets", t => t.Basket_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.CustomerShipAddresses", t => t.CustomerShipAddress_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.Store_Id)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.Basket_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.CustomerShipAddress_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Store_Id);
            
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PurchaseOrder_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrder_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PurchaseOrder_Id);
            
            CreateTable(
                "dbo.PurchaseOrderHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PurchaseOrderStatusValueId = c.Long(),
                        StatusDate = c.DateTime(),
                        StatusPDate = c.String(),
                        StatusTime = c.Time(precision: 7),
                        PurchaseOrderStatusDataId = c.Int(),
                        CreateBy = c.Guid(),
                        CreateDate = c.DateTime(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PurchaseOrder_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrder_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PurchaseOrder_Id);
            
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
                        PayPDate = c.String(),
                        PayTime = c.Time(nullable: false, precision: 7),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PurchaseOrder_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrder_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PurchaseOrder_Id);
            
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId)
                .Index(t => t.ProductId)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.StoreCalendars",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        PDate = c.String(),
                        FromTime = c.Time(nullable: false, precision: 7),
                        ToTime = c.Time(nullable: false, precision: 7),
                        CalendarTypeValueId = c.Guid(),
                        Description = c.String(),
                        StoreId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        CalendarTemplate_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        StoreCalendar_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.CalendarTemplates", t => t.CalendarTemplate_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.StoreCalendars", t => t.StoreCalendar_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.CalendarTemplate_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.StoreCalendar_Id);
            
            CreateTable(
                "dbo.CalendarTemplates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CalendarTemplateName = c.String(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.CalendarTemplateHolidays",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(),
                        PDate = c.String(),
                        FromTime = c.Time(precision: 7),
                        ToTime = c.Time(precision: 7),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        CalendarTemplate_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.CalendarTemplates", t => t.CalendarTemplate_Id)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        CalendarTemplate_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.CalendarTemplates", t => t.CalendarTemplate_Id)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        DeliveryPerson_Id = c.Guid(nullable: false),
                        LastModifiedBy_Id = c.Guid(),
                        Store_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.DeliveryPersons", t => t.DeliveryPerson_Id, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.Store_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.DeliveryPerson_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.DeliveryPersons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LastName = c.String(),
                        FirstName = c.String(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.Clearances",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ClearanceDate = c.DateTime(),
                        ClearancePDate = c.String(),
                        ClearanceTime = c.Time(precision: 7),
                        CashierId = c.Int(),
                        CashSessionId = c.Int(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.DiscountCodes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DiscountCodeUniqueId = c.Guid(nullable: false),
                        DiscountDesc = c.String(),
                        DiscountTypeValueId = c.Long(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        Manager_Id = c.Guid(),
                        Principal_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.Manager_Id)
                .ForeignKey("dbo.Principals", t => t.Principal_Id, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Manager_Id)
                .Index(t => t.Principal_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        FullName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        LastEntry = c.DateTime(nullable: false),
                        LastEntryIp = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        AddedBy_Id = c.Guid(),
                        Group_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        Principal_Id = c.Guid(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(),
                        Role_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.Principal_Id, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.Group_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Principal_Id)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.Role_Id);
            
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
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        Principal_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.Principal_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Principal_Id);
            
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
                "dbo.ItemImages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TokenId = c.String(),
                        ImageName = c.String(),
                        ImageType = c.String(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
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
                        GuaranteeDesc = c.String(),
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
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id, cascadeDelete: true)
                .Index(t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.ProductSupplierGuarantees", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductSupplierGuarantees", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductSupplierGuarantees", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ItemImages", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ItemImages", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ItemImages", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.IdentityUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Roles", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Roles", "Principal_Id", "dbo.Principals");
            DropForeignKey("dbo.Roles", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Roles", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Users", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Users", "Principal_Id", "dbo.Principals");
            DropForeignKey("dbo.IdentityUserLogins", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Users", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.IdentityUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Groups", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Groups", "Principal_Id", "dbo.Principals");
            DropForeignKey("dbo.Groups", "Manager_Id", "dbo.Principals");
            DropForeignKey("dbo.Groups", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Groups", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.DiscountCodes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.DiscountCodes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.DiscountCodes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Clearances", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Clearances", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Clearances", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BasketItems", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.BasketItems", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Baskets", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Baskets", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Customers", "RegionLevel3Id", "dbo.CityRegions");
            DropForeignKey("dbo.Customers", "RegionLevel2Id", "dbo.CityRegions");
            DropForeignKey("dbo.Customers", "RegionLevel1Id", "dbo.CityRegions");
            DropForeignKey("dbo.Customers", "RegionInfoId", "dbo.CityRegions");
            DropForeignKey("dbo.Customers", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Customers", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.IncompletePurchaseOrders", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrders", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "IncompletePurchaseOrderId", "dbo.IncompletePurchaseOrders");
            DropForeignKey("dbo.StoreValidRegionInfoes", "CityRegionID", "dbo.CityRegions");
            DropForeignKey("dbo.StoreValidRegionInfoes", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Stocks", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.StoreDeliveryPersons", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.StoreDeliveryPersons", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreDeliveryPersons", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreDeliveryPersons", "DeliveryPerson_Id", "dbo.DeliveryPersons");
            DropForeignKey("dbo.DeliveryPersons", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.DeliveryPersons", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.DeliveryPersons", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreDeliveryPersons", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreCalendars", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.StoreCalendarHistories", "StoreCalendar_Id", "dbo.StoreCalendars");
            DropForeignKey("dbo.StoreCalendarHistories", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreCalendarHistories", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreCalendarHistories", "CalendarTemplate_Id", "dbo.CalendarTemplates");
            DropForeignKey("dbo.CalendarTemplates", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplates", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "CalendarTemplate_Id", "dbo.CalendarTemplates");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplateHolidays", "CalendarTemplate_Id", "dbo.CalendarTemplates");
            DropForeignKey("dbo.CalendarTemplateHolidays", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplateHolidays", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplateHolidays", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplates", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreCalendarHistories", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreCalendars", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreCalendars", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreCalendars", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActivePriceLists", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.StoreActiveOnhands", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.StoreActiveOnhands", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductSupliers", "SuplierID", "dbo.Suppliers");
            DropForeignKey("dbo.ProductSupliers", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StoreActivePriceLists", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StoreActivePriceLists", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActivePriceLists", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActivePriceLists", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockProductRequestRules", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockProductRequestProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockHistoryOnHands", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockActiveOnHands", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PurchaseOrderLineItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.PurchaseOrders", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.PurchaseOrderPayments", "PurchaseOrder_Id", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrderPayments", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderPayments", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderPayments", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderLineItems", "PurchaseOrder_Id", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrderHistories", "PurchaseOrder_Id", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrderHistories", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderHistories", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderHistories", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderClearances", "PurchaseOrder_Id", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrderClearances", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderClearances", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderClearances", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrders", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrders", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrders", "CustomerShipAddress_Id", "dbo.CustomerShipAddresses");
            DropForeignKey("dbo.PurchaseOrders", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.PurchaseOrders", "Basket_Id", "dbo.Baskets");
            DropForeignKey("dbo.PurchaseOrders", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderLineItems", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderLineItems", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderLineItems", "FinalProduct_Id", "dbo.Products");
            DropForeignKey("dbo.PurchaseOrderLineItems", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductRates", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductRates", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductRates", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductRates", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductPictures", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductPictures", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductPictures", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductPictures", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Products", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductGroups", "ProductGroup2Id", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductGroups", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductGroups", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductGroups", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductComments", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductComments", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductComments", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductComments", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Products", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Products", "ManufactureId", "dbo.Manufactures");
            DropForeignKey("dbo.Manufactures", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Manufactures", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Manufactures", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Products", "MainSuppliereId", "dbo.Suppliers");
            DropForeignKey("dbo.StockProductRequestRules", "MainProductGroupId", "dbo.MainProductGroups");
            DropForeignKey("dbo.StockProductRequestProductDetails", "StockProductRequestRuleId", "dbo.StockProductRequestRules");
            DropForeignKey("dbo.StockProductRequests", "PorductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.StockProductRequests", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Suppliers", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Suppliers", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Suppliers", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequests", "StockTypeId", "dbo.StockTypes");
            DropForeignKey("dbo.StockProductRequests", "StockProductRequestTypeId", "dbo.StockProductRequestTypes");
            DropForeignKey("dbo.StockProductRequestTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestTypes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestTypes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequests", "StockProductRequestStatusId", "dbo.StockProductRequestStatus");
            DropForeignKey("dbo.StockProductRequestStatus", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestStatus", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestStatus", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestProducts", "StockProductRequestId", "dbo.StockProductRequests");
            DropForeignKey("dbo.StockProductRequestProductDetails", "StockProductRequestProductId", "dbo.StockProductRequestProducts");
            DropForeignKey("dbo.StockProductRequestProductDetails", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestProductDetails", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestProductDetails", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestProducts", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestProducts", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestProducts", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Stocks", "StockTypeId", "dbo.StockTypes");
            DropForeignKey("dbo.StockTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockTypes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockTypes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProducts", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockProductRequests", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockOnHandSyncs", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockProductRequests", "StockOnHandSyncId", "dbo.StockOnHandSyncs");
            DropForeignKey("dbo.StockHistoryOnHands", "StockOnHandSyncId", "dbo.StockOnHandSyncs");
            DropForeignKey("dbo.StockHistoryOnHands", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockHistoryOnHands", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockHistoryOnHands", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockHistoryOnHands", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockActiveOnHands", "StockOnHandSyncId", "dbo.StockOnHandSyncs");
            DropForeignKey("dbo.StockActiveOnHands", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockActiveOnHands", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockActiveOnHands", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockActiveOnHands", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockOnHandSyncs", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockOnHandSyncs", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockOnHandSyncs", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Stocks", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Stocks", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Stocks", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Stocks", "Accept3ById", "dbo.Principals");
            DropForeignKey("dbo.Stocks", "Accept2ById", "dbo.Principals");
            DropForeignKey("dbo.Stocks", "Accept1ById", "dbo.Principals");
            DropForeignKey("dbo.StockProducts", "ReorderCalcTypeId", "dbo.ReorderCalcTypes");
            DropForeignKey("dbo.StockProducts", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProducts", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProducts", "FiscalYearId", "dbo.FiscalYears");
            DropForeignKey("dbo.FiscalYears", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.FiscalYears", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.FiscalYears", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProducts", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequests", "ReorderCalcTypeId", "dbo.ReorderCalcTypes");
            DropForeignKey("dbo.ReorderCalcTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ReorderCalcTypes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ReorderCalcTypes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequests", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequests", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequests", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequests", "Accept3ById", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequests", "Accept2ById", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequests", "Accept1ById", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestRules", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.ProductTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductTypes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductTypes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestRules", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestRules", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestRules", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Products", "MainProductGroupId", "dbo.MainProductGroups");
            DropForeignKey("dbo.MainProductGroups", "ProductGroup2Id", "dbo.MainProductGroups");
            DropForeignKey("dbo.MainProductGroups", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.MainProductGroups", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.MainProductGroups", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Products", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrderLineItems", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductChars", "CharValueID", "dbo.CharValues");
            DropForeignKey("dbo.ProductChars", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CharValues", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CharValues", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CharTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CharTypes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CharValues", "CharTypeId", "dbo.CharTypes");
            DropForeignKey("dbo.CharGroupTypes", "CharGroupID", "dbo.CharGroups");
            DropForeignKey("dbo.CharGroupTypes", "CharTypeId", "dbo.CharTypes");
            DropForeignKey("dbo.CharGroups", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CharGroups", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CharGroups", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CharTypes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CharValues", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BasketItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActiveOnhands", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActiveOnhands", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActiveOnhands", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActions", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.StoreActions", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActions", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActions", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Stores", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Stores", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrders", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Stores", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CityRegions", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CityRegions", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrders", "CityRegionId", "dbo.CityRegions");
            DropForeignKey("dbo.Customers", "RegionLevel4Id", "dbo.CityRegions");
            DropForeignKey("dbo.CityRegions", "CityRegion2Id", "dbo.CityRegions");
            DropForeignKey("dbo.CityRegions", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.IncompletePurchaseOrders", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CustomerShipAddresses", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.CustomerShipAddresses", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CustomerShipAddresses", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CustomerShipAddresses", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Baskets", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BasketNotes", "Basket_Id", "dbo.Baskets");
            DropForeignKey("dbo.BasketNotes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.BasketNotes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BasketNotes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropForeignKey("dbo.Baskets", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BasketItems", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BaseTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.BaseTypes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BaseValues", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.BaseValues", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BaseValues", "BaseTypeId", "dbo.BaseTypes");
            DropForeignKey("dbo.BaseValues", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BaseTypes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BankAccounts", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.BankAccounts", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BankAccounts", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PrincipalPermissions", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.PrincipalPermissions", "Principal_Id", "dbo.Principals");
            DropForeignKey("dbo.PrincipalPermissions", "Permission_Id", "dbo.Permissions");
            DropForeignKey("dbo.Permissions", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Permissions", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Permissions", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PrincipalPermissions", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PrincipalPermissions", "AddedBy_Id", "dbo.Principals");
            DropIndex("dbo.StoreValidRegionInfoes", new[] { "CityRegionID" });
            DropIndex("dbo.StoreValidRegionInfoes", new[] { "StoreId" });
            DropIndex("dbo.ProductSupliers", new[] { "SuplierID" });
            DropIndex("dbo.ProductSupliers", new[] { "ProductId" });
            DropIndex("dbo.ProductChars", new[] { "CharValueID" });
            DropIndex("dbo.ProductChars", new[] { "ProductId" });
            DropIndex("dbo.CharGroupTypes", new[] { "CharGroupID" });
            DropIndex("dbo.CharGroupTypes", new[] { "CharTypeId" });
            DropIndex("dbo.ProductSupplierGuarantees", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductSupplierGuarantees", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductSupplierGuarantees", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ItemImages", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ItemImages", new[] { "AddedBy_Id" });
            DropIndex("dbo.ItemImages", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", new[] { "Principal_Id" });
            DropIndex("dbo.Roles", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Roles", new[] { "AddedBy_Id" });
            DropIndex("dbo.Roles", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "User_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.Users", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Users", new[] { "Principal_Id" });
            DropIndex("dbo.Users", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Users", new[] { "Group_Id" });
            DropIndex("dbo.Users", new[] { "AddedBy_Id" });
            DropIndex("dbo.Groups", new[] { "Principal_Id" });
            DropIndex("dbo.Groups", new[] { "Manager_Id" });
            DropIndex("dbo.Groups", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Groups", new[] { "AddedBy_Id" });
            DropIndex("dbo.Groups", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.DiscountCodes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.DiscountCodes", new[] { "AddedBy_Id" });
            DropIndex("dbo.DiscountCodes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Clearances", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Clearances", new[] { "AddedBy_Id" });
            DropIndex("dbo.Clearances", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.DeliveryPersons", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.DeliveryPersons", new[] { "AddedBy_Id" });
            DropIndex("dbo.DeliveryPersons", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "Store_Id" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "DeliveryPerson_Id" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CalendarTemplateOpenTimes", new[] { "CalendarTemplate_Id" });
            DropIndex("dbo.CalendarTemplateOpenTimes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CalendarTemplateOpenTimes", new[] { "AddedBy_Id" });
            DropIndex("dbo.CalendarTemplateOpenTimes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CalendarTemplateHolidays", new[] { "CalendarTemplate_Id" });
            DropIndex("dbo.CalendarTemplateHolidays", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CalendarTemplateHolidays", new[] { "AddedBy_Id" });
            DropIndex("dbo.CalendarTemplateHolidays", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CalendarTemplates", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CalendarTemplates", new[] { "AddedBy_Id" });
            DropIndex("dbo.CalendarTemplates", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "StoreCalendar_Id" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "CalendarTemplate_Id" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StoreCalendars", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreCalendars", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreCalendars", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StoreCalendars", new[] { "StoreId" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "ProductId" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "StoreId" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "PurchaseOrder_Id" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "AddedBy_Id" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.PurchaseOrderHistories", new[] { "PurchaseOrder_Id" });
            DropIndex("dbo.PurchaseOrderHistories", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PurchaseOrderHistories", new[] { "AddedBy_Id" });
            DropIndex("dbo.PurchaseOrderHistories", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.PurchaseOrderClearances", new[] { "PurchaseOrder_Id" });
            DropIndex("dbo.PurchaseOrderClearances", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PurchaseOrderClearances", new[] { "AddedBy_Id" });
            DropIndex("dbo.PurchaseOrderClearances", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "Store_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "CustomerShipAddress_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "Customer_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "Basket_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "AddedBy_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "Product_Id" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "PurchaseOrder_Id" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "FinalProduct_Id" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "AddedBy_Id" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ProductRates", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductRates", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductRates", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ProductRates", new[] { "ProductId" });
            DropIndex("dbo.ProductPictures", new[] { "Product_Id" });
            DropIndex("dbo.ProductPictures", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductPictures", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductPictures", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ProductGroups", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductGroups", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductGroups", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ProductGroups", new[] { "ProductGroup2Id" });
            DropIndex("dbo.ProductComments", new[] { "Product_Id" });
            DropIndex("dbo.ProductComments", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductComments", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductComments", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Manufactures", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Manufactures", new[] { "AddedBy_Id" });
            DropIndex("dbo.Manufactures", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Suppliers", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Suppliers", new[] { "AddedBy_Id" });
            DropIndex("dbo.Suppliers", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequestTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequestStatus", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestStatus", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestStatus", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "StockProductRequestRuleId" });
            DropIndex("dbo.StockProductRequestProductDetails", new[] { "StockProductRequestProductId" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "StockProductRequestId" });
            DropIndex("dbo.StockProductRequestProducts", new[] { "ProductId" });
            DropIndex("dbo.StockTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "StockOnHandSyncId" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "ProductId" });
            DropIndex("dbo.StockHistoryOnHands", new[] { "StockId" });
            DropIndex("dbo.StockActiveOnHands", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockActiveOnHands", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockActiveOnHands", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockActiveOnHands", new[] { "StockOnHandSyncId" });
            DropIndex("dbo.StockActiveOnHands", new[] { "ProductId" });
            DropIndex("dbo.StockActiveOnHands", new[] { "StockId" });
            DropIndex("dbo.StockOnHandSyncs", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockOnHandSyncs", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockOnHandSyncs", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockOnHandSyncs", new[] { "StockId" });
            DropIndex("dbo.Stocks", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Stocks", new[] { "AddedBy_Id" });
            DropIndex("dbo.Stocks", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Stocks", new[] { "StockTypeId" });
            DropIndex("dbo.Stocks", new[] { "Accept3ById" });
            DropIndex("dbo.Stocks", new[] { "Accept2ById" });
            DropIndex("dbo.Stocks", new[] { "Accept1ById" });
            DropIndex("dbo.Stocks", new[] { "StoreId" });
            DropIndex("dbo.FiscalYears", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.FiscalYears", new[] { "AddedBy_Id" });
            DropIndex("dbo.FiscalYears", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProducts", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProducts", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProducts", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProducts", new[] { "ReorderCalcTypeId" });
            DropIndex("dbo.StockProducts", new[] { "ProductId" });
            DropIndex("dbo.StockProducts", new[] { "FiscalYearId" });
            DropIndex("dbo.StockProducts", new[] { "StockId" });
            DropIndex("dbo.ReorderCalcTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ReorderCalcTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.ReorderCalcTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequests", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequests", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequests", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequests", new[] { "StockOnHandSyncId" });
            DropIndex("dbo.StockProductRequests", new[] { "Accept3ById" });
            DropIndex("dbo.StockProductRequests", new[] { "Accept2ById" });
            DropIndex("dbo.StockProductRequests", new[] { "Accept1ById" });
            DropIndex("dbo.StockProductRequests", new[] { "PorductTypeId" });
            DropIndex("dbo.StockProductRequests", new[] { "StockProductRequestTypeId" });
            DropIndex("dbo.StockProductRequests", new[] { "ReorderCalcTypeId" });
            DropIndex("dbo.StockProductRequests", new[] { "SupplierId" });
            DropIndex("dbo.StockProductRequests", new[] { "StockTypeId" });
            DropIndex("dbo.StockProductRequests", new[] { "StockId" });
            DropIndex("dbo.StockProductRequests", new[] { "StockProductRequestStatusId" });
            DropIndex("dbo.ProductTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequestRules", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestRules", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestRules", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequestRules", new[] { "ProductTypeId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "MainProductGroupId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "ProductId" });
            DropIndex("dbo.MainProductGroups", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.MainProductGroups", new[] { "AddedBy_Id" });
            DropIndex("dbo.MainProductGroups", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.MainProductGroups", new[] { "ProductGroup2Id" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "AddedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "IncompletePurchaseOrderId" });
            DropIndex("dbo.IncompletePurchaseOrderLineItems", new[] { "ProductId" });
            DropIndex("dbo.CharGroups", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CharGroups", new[] { "AddedBy_Id" });
            DropIndex("dbo.CharGroups", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CharTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CharTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.CharTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CharValues", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CharValues", new[] { "AddedBy_Id" });
            DropIndex("dbo.CharValues", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CharValues", new[] { "CharTypeId" });
            DropIndex("dbo.Products", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Products", new[] { "AddedBy_Id" });
            DropIndex("dbo.Products", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Products", new[] { "ProductTypeId" });
            DropIndex("dbo.Products", new[] { "MainSuppliereId" });
            DropIndex("dbo.Products", new[] { "ManufactureId" });
            DropIndex("dbo.Products", new[] { "MainProductGroupId" });
            DropIndex("dbo.Products", new[] { "ProductGroupId" });
            DropIndex("dbo.StoreActiveOnhands", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreActiveOnhands", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreActiveOnhands", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StoreActiveOnhands", new[] { "ProductId" });
            DropIndex("dbo.StoreActiveOnhands", new[] { "StoreId" });
            DropIndex("dbo.StoreActions", new[] { "Store_Id" });
            DropIndex("dbo.StoreActions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreActions", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreActions", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Stores", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Stores", new[] { "AddedBy_Id" });
            DropIndex("dbo.Stores", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CityRegions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CityRegions", new[] { "AddedBy_Id" });
            DropIndex("dbo.CityRegions", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CityRegions", new[] { "CityRegion2Id" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "AddedBy_Id" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "CustomerId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "CityRegionId" });
            DropIndex("dbo.IncompletePurchaseOrders", new[] { "StoreId" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "Customer_Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "AddedBy_Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Customers", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Customers", new[] { "AddedBy_Id" });
            DropIndex("dbo.Customers", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Customers", new[] { "RegionLevel4Id" });
            DropIndex("dbo.Customers", new[] { "RegionLevel3Id" });
            DropIndex("dbo.Customers", new[] { "RegionLevel2Id" });
            DropIndex("dbo.Customers", new[] { "RegionLevel1Id" });
            DropIndex("dbo.Customers", new[] { "RegionInfoId" });
            DropIndex("dbo.BasketNotes", new[] { "Basket_Id" });
            DropIndex("dbo.BasketNotes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.BasketNotes", new[] { "AddedBy_Id" });
            DropIndex("dbo.BasketNotes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Baskets", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Baskets", new[] { "AddedBy_Id" });
            DropIndex("dbo.Baskets", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Baskets", new[] { "CustomerId" });
            DropIndex("dbo.BasketItems", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.BasketItems", new[] { "AddedBy_Id" });
            DropIndex("dbo.BasketItems", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            DropIndex("dbo.BasketItems", new[] { "ProductId" });
            DropIndex("dbo.BaseValues", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.BaseValues", new[] { "AddedBy_Id" });
            DropIndex("dbo.BaseValues", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.BaseValues", new[] { "BaseTypeId" });
            DropIndex("dbo.BaseTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.BaseTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.BaseTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Permissions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Permissions", new[] { "AddedBy_Id" });
            DropIndex("dbo.Permissions", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.PrincipalPermissions", new[] { "Principal_Id" });
            DropIndex("dbo.PrincipalPermissions", new[] { "Permission_Id" });
            DropIndex("dbo.PrincipalPermissions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PrincipalPermissions", new[] { "AddedBy_Id" });
            DropIndex("dbo.PrincipalPermissions", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.BankAccounts", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.BankAccounts", new[] { "AddedBy_Id" });
            DropIndex("dbo.BankAccounts", new[] { "PrivateLabelOwner_Id" });
            DropTable("dbo.StoreValidRegionInfoes");
            DropTable("dbo.ProductSupliers");
            DropTable("dbo.ProductChars");
            DropTable("dbo.CharGroupTypes");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.ProductSupplierGuarantees");
            DropTable("dbo.ItemImages");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Groups");
            DropTable("dbo.DiscountCodes");
            DropTable("dbo.Clearances");
            DropTable("dbo.DeliveryPersons");
            DropTable("dbo.StoreDeliveryPersons");
            DropTable("dbo.CalendarTemplateOpenTimes");
            DropTable("dbo.CalendarTemplateHolidays");
            DropTable("dbo.CalendarTemplates");
            DropTable("dbo.StoreCalendarHistories");
            DropTable("dbo.StoreCalendars");
            DropTable("dbo.StoreActivePriceLists");
            DropTable("dbo.PurchaseOrderPayments");
            DropTable("dbo.PurchaseOrderHistories");
            DropTable("dbo.PurchaseOrderClearances");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.PurchaseOrderLineItems");
            DropTable("dbo.ProductRates");
            DropTable("dbo.ProductPictures");
            DropTable("dbo.ProductGroups");
            DropTable("dbo.ProductComments");
            DropTable("dbo.Manufactures");
            DropTable("dbo.Suppliers");
            DropTable("dbo.StockProductRequestTypes");
            DropTable("dbo.StockProductRequestStatus");
            DropTable("dbo.StockProductRequestProductDetails");
            DropTable("dbo.StockProductRequestProducts");
            DropTable("dbo.StockTypes");
            DropTable("dbo.StockHistoryOnHands");
            DropTable("dbo.StockActiveOnHands");
            DropTable("dbo.StockOnHandSyncs");
            DropTable("dbo.Stocks");
            DropTable("dbo.FiscalYears");
            DropTable("dbo.StockProducts");
            DropTable("dbo.ReorderCalcTypes");
            DropTable("dbo.StockProductRequests");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.StockProductRequestRules");
            DropTable("dbo.MainProductGroups");
            DropTable("dbo.IncompletePurchaseOrderLineItems");
            DropTable("dbo.CharGroups");
            DropTable("dbo.CharTypes");
            DropTable("dbo.CharValues");
            DropTable("dbo.Products");
            DropTable("dbo.StoreActiveOnhands");
            DropTable("dbo.StoreActions");
            DropTable("dbo.Stores");
            DropTable("dbo.CityRegions");
            DropTable("dbo.IncompletePurchaseOrders");
            DropTable("dbo.CustomerShipAddresses");
            DropTable("dbo.Customers");
            DropTable("dbo.BasketNotes");
            DropTable("dbo.Baskets");
            DropTable("dbo.BasketItems");
            DropTable("dbo.BaseValues");
            DropTable("dbo.BaseTypes");
            DropTable("dbo.Permissions");
            DropTable("dbo.PrincipalPermissions");
            DropTable("dbo.Principals");
            DropTable("dbo.BankAccounts");
        }
    }
}
