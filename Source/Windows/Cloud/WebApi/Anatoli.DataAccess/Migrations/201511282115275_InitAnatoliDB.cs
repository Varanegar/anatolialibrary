namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitAnatoliDB : DbMigration
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BaseTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BaseTypeDesc = c.String(),
                        BaseTypeName = c.String(),
                        Number_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BaseValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BaseTypeId = c.Long(nullable: false),
                        BaseValueName = c.String(),
                        Number_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Int(),
                        Qty = c.Int(),
                        Comment = c.String(),
                        Number_ID = c.Int(nullable: false),
                        Basket_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.Basket_Id, cascadeDelete: true)
                .Index(t => t.Basket_Id);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BasketTypeValueId = c.Int(nullable: false),
                        BasketName = c.String(),
                        CustomerId = c.Int(),
                        Number_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Basket_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.Basket_Id, cascadeDelete: true)
                .Index(t => t.Basket_Id);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderUniqueId = c.Guid(nullable: false),
                        ActionSourceValueId = c.Long(nullable: false),
                        DeviceIMEI = c.String(),
                        OrderDate = c.DateTime(),
                        OrderPDate = c.String(),
                        OrderTime = c.Time(precision: 7),
                        CustomeId = c.Int(),
                        ShipAddressId = c.Int(),
                        StoreId = c.Int(),
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
                        Basket_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.Basket_Id)
                .Index(t => t.Basket_Id);
            
            CreateTable(
                "dbo.PurchaseOrderClearances",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeliveryPersonId = c.Int(nullable: false),
                        ClearanceStatusTypeId = c.Int(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        PurchaseOrder_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrder_Id, cascadeDelete: true)
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
                        PurchaseOrder_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrder_Id, cascadeDelete: true)
                .Index(t => t.PurchaseOrder_Id);
            
            CreateTable(
                "dbo.PurchaseOrderLineItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Int(),
                        FinalProductId = c.Int(nullable: false),
                        ProductBaseId = c.Int(),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Add1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Add2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsPrize = c.Byte(nullable: false),
                        Comment = c.String(),
                        AllowReplace = c.Byte(),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalUnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalAdd1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalAdd2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalIsPrize = c.Byte(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        PurchaseOrder_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrder_Id, cascadeDelete: true)
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
                        PurchaseOrder_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrder_Id, cascadeDelete: true)
                .Index(t => t.PurchaseOrder_Id);
            
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
                        CalendarTemplate_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CalendarTemplates", t => t.CalendarTemplate_Id)
                .Index(t => t.CalendarTemplate_Id);
            
            CreateTable(
                "dbo.CalendarTemplates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CalendarTemplateName = c.String(),
                        Number_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        CalendarTemplate_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CalendarTemplates", t => t.CalendarTemplate_Id)
                .Index(t => t.CalendarTemplate_Id);
            
            CreateTable(
                "dbo.StoreCalendarHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplyBy = c.Guid(),
                        ApplyDate = c.DateTime(),
                        Number_ID = c.Int(nullable: false),
                        StoreCalendar_Id = c.Guid(nullable: false),
                        CalendarTemplate_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StoreCalendars", t => t.StoreCalendar_Id, cascadeDelete: true)
                .ForeignKey("dbo.CalendarTemplates", t => t.CalendarTemplate_Id)
                .Index(t => t.StoreCalendar_Id)
                .Index(t => t.CalendarTemplate_Id);
            
            CreateTable(
                "dbo.StoreCalendars",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(),
                        PDate = c.String(),
                        FromTime = c.Time(precision: 7),
                        ToTime = c.Time(precision: 7),
                        CalendarTypeValueId = c.Guid(),
                        Description = c.String(),
                        Number_ID = c.Int(nullable: false),
                        Store_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.Store_Id, cascadeDelete: true)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StoreName = c.String(),
                        Address = c.String(),
                        Lat = c.Decimal(precision: 18, scale: 2),
                        Lng = c.Decimal(precision: 18, scale: 2),
                        HasDelivery = c.Byte(),
                        GradeValueId = c.Int(),
                        StoreTemplateId = c.Int(),
                        HasCourier = c.Byte(),
                        SupportAppOrder = c.Byte(),
                        SupportWebOrder = c.Byte(),
                        SupportCallCenterOrder = c.Byte(),
                        StoreStatusTypeId = c.Byte(),
                        Number_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Store_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.Store_Id, cascadeDelete: true)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.StoreActiveOnHands",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Number_ID = c.Int(nullable: false),
                        Store_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.Store_Id, cascadeDelete: true)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.StoreActivePriceLists",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Price = c.Decimal(precision: 18, scale: 2),
                        ProductId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        Store_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.Store_Id, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MainAppProductId = c.Int(nullable: false),
                        ProductCode = c.String(),
                        ProductName = c.String(),
                        StoreProductName = c.String(),
                        PackUnitValueId = c.Int(nullable: false),
                        ProductTypeValueId = c.Int(nullable: false),
                        PackVolume = c.Decimal(precision: 18, scale: 2),
                        PackWeight = c.Decimal(precision: 18, scale: 2),
                        TaxCategoryValueId = c.Long(),
                        MainSupplierId = c.Int(),
                        Number_ID = c.Int(nullable: false),
                        ProductBase_Id = c.Guid(nullable: false),
                        ProductGroup_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductBases", t => t.ProductBase_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroup_Id)
                .Index(t => t.ProductBase_Id)
                .Index(t => t.ProductGroup_Id);
            
            CreateTable(
                "dbo.CharValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CharValueText = c.String(),
                        CharValueFromAmount = c.Decimal(precision: 18, scale: 2),
                        CharValueToAmount = c.Decimal(precision: 18, scale: 2),
                        Number_ID = c.Int(nullable: false),
                        CharType_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CharTypes", t => t.CharType_Id, cascadeDelete: true)
                .Index(t => t.CharType_Id);
            
            CreateTable(
                "dbo.CharTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CharTypeDesc = c.String(),
                        DefaultCharValueID = c.Int(),
                        Number_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CharGroupTypeInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CharType_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CharTypes", t => t.CharType_Id)
                .Index(t => t.CharType_Id);
            
            CreateTable(
                "dbo.ProductBases",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductBaseUniqueId = c.Guid(nullable: false),
                        ProductBaseCode = c.String(),
                        ProductBaseName = c.String(),
                        StoreProductBaseName = c.String(),
                        PackUnitValueId = c.Int(nullable: false),
                        ProductTypeValueId = c.Int(nullable: false),
                        PackVolume = c.Decimal(precision: 18, scale: 2),
                        PackWeight = c.Decimal(precision: 18, scale: 2),
                        TaxCategoryValueId = c.Long(),
                        ProductGroupId = c.Int(),
                        MainSupplierId = c.Int(),
                        CharCategoryId = c.Int(nullable: false),
                        Number_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
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
                        CharGroupId = c.Int(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        ProductGroup2_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroup2_Id)
                .Index(t => t.ProductGroup2_Id);
            
            CreateTable(
                "dbo.ProductPictures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Picture = c.Binary(),
                        PictureTypeValueId = c.Int(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
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
                        Number_ID = c.Int(nullable: false),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.StoreDeliveryPersons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.Byte(),
                        Number_ID = c.Int(nullable: false),
                        DeliveryPerson_Id = c.Guid(nullable: false),
                        Store_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeliveryPersons", t => t.DeliveryPerson_Id, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.Store_Id, cascadeDelete: true)
                .Index(t => t.DeliveryPerson_Id)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.DeliveryPersons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Number_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StoreValidRegionInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CityRegion_Id = c.Guid(nullable: false),
                        Store_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CityRegions", t => t.CityRegion_Id, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.Store_Id, cascadeDelete: true)
                .Index(t => t.CityRegion_Id)
                .Index(t => t.Store_Id);
            
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
                        ParentId = c.Guid(),
                        Number_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CharCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CharCategoryName = c.String(),
                        ParentId = c.Int(nullable: false),
                        NLeft = c.Int(nullable: false),
                        NRight = c.Int(nullable: false),
                        NLevel = c.Int(nullable: false),
                        Priority = c.Int(),
                        Number_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CharGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CharGroupCode = c.Int(nullable: false),
                        CharGroupName = c.String(),
                        Number_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerCode = c.Long(),
                        CustomerName = c.String(),
                        BirthDay = c.DateTime(),
                        Phone = c.String(),
                        Mobile = c.Long(),
                        Email = c.String(),
                        StateId = c.Int(),
                        CityId = c.Int(),
                        ZoneId = c.Int(),
                        Address = c.String(),
                        PostalCode = c.String(),
                        CustomerMainAppId = c.Int(),
                        Username = c.String(),
                        Password = c.String(),
                        ActionSourceValueId = c.Long(),
                        DeviceIMEI = c.String(),
                        Number_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Customer_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.DiscountCodes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DiscountCodeUniqueId = c.Guid(nullable: false),
                        DiscountDesc = c.String(),
                        DiscountTypeValueId = c.Long(),
                        Number_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductSuppliers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Comment = c.String(),
                        Number_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SupplierUniqueId = c.Guid(),
                        SupplierName = c.String(),
                        SupplierMainAppId = c.Int(),
                        Number_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductBase_ProductMap",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerShipAddresses", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.StoreCalendarHistories", "CalendarTemplate_Id", "dbo.CalendarTemplates");
            DropForeignKey("dbo.StoreCalendarHistories", "StoreCalendar_Id", "dbo.StoreCalendars");
            DropForeignKey("dbo.StoreValidRegionInfoes", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.StoreValidRegionInfoes", "CityRegion_Id", "dbo.CityRegions");
            DropForeignKey("dbo.StoreDeliveryPersons", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.StoreDeliveryPersons", "DeliveryPerson_Id", "dbo.DeliveryPersons");
            DropForeignKey("dbo.StoreCalendars", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.StoreActivePriceLists", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.StoreActivePriceLists", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductRates", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductPictures", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductGroup_Id", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductGroups", "ProductGroup2_Id", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductComments", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductBase_Id", "dbo.ProductBases");
            DropForeignKey("dbo.ProductBase_ProductMap", "CharValueID", "dbo.CharValues");
            DropForeignKey("dbo.ProductBase_ProductMap", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CharValues", "CharType_Id", "dbo.CharTypes");
            DropForeignKey("dbo.CharGroupTypeInfoes", "CharType_Id", "dbo.CharTypes");
            DropForeignKey("dbo.StoreActiveOnHands", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.StoreActions", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "CalendarTemplate_Id", "dbo.CalendarTemplates");
            DropForeignKey("dbo.CalendarTemplateHolidays", "CalendarTemplate_Id", "dbo.CalendarTemplates");
            DropForeignKey("dbo.PurchaseOrderPayments", "PurchaseOrder_Id", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrderLineItems", "PurchaseOrder_Id", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrderHistories", "PurchaseOrder_Id", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrderClearances", "PurchaseOrder_Id", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrders", "Basket_Id", "dbo.Baskets");
            DropForeignKey("dbo.BasketNotes", "Basket_Id", "dbo.Baskets");
            DropForeignKey("dbo.BasketItems", "Basket_Id", "dbo.Baskets");
            DropIndex("dbo.ProductBase_ProductMap", new[] { "CharValueID" });
            DropIndex("dbo.ProductBase_ProductMap", new[] { "ProductId" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "Customer_Id" });
            DropIndex("dbo.StoreValidRegionInfoes", new[] { "Store_Id" });
            DropIndex("dbo.StoreValidRegionInfoes", new[] { "CityRegion_Id" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "Store_Id" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "DeliveryPerson_Id" });
            DropIndex("dbo.ProductRates", new[] { "Product_Id" });
            DropIndex("dbo.ProductPictures", new[] { "Product_Id" });
            DropIndex("dbo.ProductGroups", new[] { "ProductGroup2_Id" });
            DropIndex("dbo.ProductComments", new[] { "Product_Id" });
            DropIndex("dbo.CharGroupTypeInfoes", new[] { "CharType_Id" });
            DropIndex("dbo.CharValues", new[] { "CharType_Id" });
            DropIndex("dbo.Products", new[] { "ProductGroup_Id" });
            DropIndex("dbo.Products", new[] { "ProductBase_Id" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "Store_Id" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "ProductId" });
            DropIndex("dbo.StoreActiveOnHands", new[] { "Store_Id" });
            DropIndex("dbo.StoreActions", new[] { "Store_Id" });
            DropIndex("dbo.StoreCalendars", new[] { "Store_Id" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "CalendarTemplate_Id" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "StoreCalendar_Id" });
            DropIndex("dbo.CalendarTemplateOpenTimes", new[] { "CalendarTemplate_Id" });
            DropIndex("dbo.CalendarTemplateHolidays", new[] { "CalendarTemplate_Id" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "PurchaseOrder_Id" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "PurchaseOrder_Id" });
            DropIndex("dbo.PurchaseOrderHistories", new[] { "PurchaseOrder_Id" });
            DropIndex("dbo.PurchaseOrderClearances", new[] { "PurchaseOrder_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "Basket_Id" });
            DropIndex("dbo.BasketNotes", new[] { "Basket_Id" });
            DropIndex("dbo.BasketItems", new[] { "Basket_Id" });
            DropTable("dbo.ProductBase_ProductMap");
            DropTable("dbo.Suppliers");
            DropTable("dbo.ProductSuppliers");
            DropTable("dbo.ProductSupplierGuarantees");
            DropTable("dbo.DiscountCodes");
            DropTable("dbo.CustomerShipAddresses");
            DropTable("dbo.Customers");
            DropTable("dbo.Clearances");
            DropTable("dbo.CharGroups");
            DropTable("dbo.CharCategories");
            DropTable("dbo.CityRegions");
            DropTable("dbo.StoreValidRegionInfoes");
            DropTable("dbo.DeliveryPersons");
            DropTable("dbo.StoreDeliveryPersons");
            DropTable("dbo.ProductRates");
            DropTable("dbo.ProductPictures");
            DropTable("dbo.ProductGroups");
            DropTable("dbo.ProductComments");
            DropTable("dbo.ProductBases");
            DropTable("dbo.CharGroupTypeInfoes");
            DropTable("dbo.CharTypes");
            DropTable("dbo.CharValues");
            DropTable("dbo.Products");
            DropTable("dbo.StoreActivePriceLists");
            DropTable("dbo.StoreActiveOnHands");
            DropTable("dbo.StoreActions");
            DropTable("dbo.Stores");
            DropTable("dbo.StoreCalendars");
            DropTable("dbo.StoreCalendarHistories");
            DropTable("dbo.CalendarTemplateOpenTimes");
            DropTable("dbo.CalendarTemplates");
            DropTable("dbo.CalendarTemplateHolidays");
            DropTable("dbo.PurchaseOrderPayments");
            DropTable("dbo.PurchaseOrderLineItems");
            DropTable("dbo.PurchaseOrderHistories");
            DropTable("dbo.PurchaseOrderClearances");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.BasketNotes");
            DropTable("dbo.Baskets");
            DropTable("dbo.BasketItems");
            DropTable("dbo.BaseValues");
            DropTable("dbo.BaseTypes");
            DropTable("dbo.BankAccounts");
        }
    }
}
