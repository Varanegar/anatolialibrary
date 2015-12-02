namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAnatoliIdentityModels : DbMigration
    {
        public override void Up()
        {
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
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        Permission_Id = c.Guid(nullable: false),
                        Principal_Id = c.Guid(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.Principal_Id, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Permission_Id)
                .Index(t => t.Principal_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
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
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        PrivateLabelOwner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        Manager_Id = c.Guid(),
                        Principal_Id = c.Guid(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.Manager_Id)
                .ForeignKey("dbo.Principals", t => t.Principal_Id, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Manager_Id)
                .Index(t => t.Principal_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                        LastEntry = c.DateTime(nullable: false),
                        LastEntryIp = c.String(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
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
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                        Principal_Id = c.Guid(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Principals", t => t.AddedBy_Id)
                .ForeignKey("dbo.Principals", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Principals", t => t.Principal_Id, cascadeDelete: true)
                .ForeignKey("dbo.Principals", t => t.PrivateLabelOwner_Id)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Principal_Id)
                .Index(t => t.PrivateLabelOwner_Id);
            
            AddColumn("dbo.BankAccounts", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BankAccounts", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BankAccounts", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.BankAccounts", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.BankAccounts", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.BaseTypes", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BaseTypes", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BaseTypes", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.BaseTypes", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.BaseTypes", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.BaseValues", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BaseValues", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BaseValues", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.BaseValues", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.BaseValues", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.BasketItems", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BasketItems", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BasketItems", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.BasketItems", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.BasketItems", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.Baskets", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Baskets", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Baskets", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.Baskets", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.Baskets", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.BasketNotes", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BasketNotes", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BasketNotes", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.BasketNotes", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.BasketNotes", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrders", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseOrders", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseOrders", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrders", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrders", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrderClearances", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseOrderClearances", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseOrderClearances", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrderClearances", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrderClearances", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrderHistories", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseOrderHistories", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseOrderHistories", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrderHistories", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrderHistories", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrderLineItems", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseOrderLineItems", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseOrderLineItems", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrderLineItems", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrderLineItems", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrderPayments", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseOrderPayments", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseOrderPayments", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrderPayments", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrderPayments", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.CalendarTemplateHolidays", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CalendarTemplateHolidays", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CalendarTemplateHolidays", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.CalendarTemplateHolidays", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.CalendarTemplateHolidays", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.CalendarTemplates", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CalendarTemplates", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CalendarTemplates", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.CalendarTemplates", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.CalendarTemplates", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.CalendarTemplateOpenTimes", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CalendarTemplateOpenTimes", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CalendarTemplateOpenTimes", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.CalendarTemplateOpenTimes", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.CalendarTemplateOpenTimes", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.StoreCalendarHistories", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StoreCalendarHistories", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StoreCalendarHistories", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.StoreCalendarHistories", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.StoreCalendarHistories", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.StoreCalendars", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StoreCalendars", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StoreCalendars", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.StoreCalendars", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.StoreCalendars", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.Stores", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Stores", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Stores", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.Stores", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.Stores", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.StoreActions", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StoreActions", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StoreActions", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.StoreActions", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.StoreActions", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.StoreActiveOnHands", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StoreActiveOnHands", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StoreActiveOnHands", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.StoreActiveOnHands", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.StoreActiveOnHands", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.StoreActivePriceLists", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StoreActivePriceLists", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StoreActivePriceLists", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.StoreActivePriceLists", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.StoreActivePriceLists", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.Products", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.Products", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.Products", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.CharValues", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CharValues", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CharValues", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.CharValues", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.CharValues", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.CharTypes", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CharTypes", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CharTypes", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.CharTypes", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.CharTypes", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.CharGroupTypeInfoes", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CharGroupTypeInfoes", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CharGroupTypeInfoes", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.CharGroupTypeInfoes", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.CharGroupTypeInfoes", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.ProductBases", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductBases", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductBases", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.ProductBases", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.ProductBases", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.ProductComments", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductComments", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductComments", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.ProductComments", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.ProductComments", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.ProductGroups", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductGroups", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductGroups", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.ProductGroups", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.ProductGroups", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.ProductPictures", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductPictures", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductPictures", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.ProductPictures", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.ProductPictures", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.ProductRates", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductRates", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductRates", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.ProductRates", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.ProductRates", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.StoreDeliveryPersons", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StoreDeliveryPersons", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StoreDeliveryPersons", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.StoreDeliveryPersons", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.StoreDeliveryPersons", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.DeliveryPersons", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DeliveryPersons", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DeliveryPersons", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.DeliveryPersons", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.DeliveryPersons", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.StoreValidRegionInfoes", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StoreValidRegionInfoes", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StoreValidRegionInfoes", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.StoreValidRegionInfoes", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.StoreValidRegionInfoes", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.CityRegions", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CityRegions", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CityRegions", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.CityRegions", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.CityRegions", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.CharCategories", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CharCategories", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CharCategories", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.CharCategories", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.CharCategories", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.CharGroups", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CharGroups", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CharGroups", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.CharGroups", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.CharGroups", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.Clearances", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Clearances", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Clearances", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.Clearances", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.Clearances", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.Customers", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customers", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customers", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.Customers", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.Customers", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.CustomerShipAddresses", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CustomerShipAddresses", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CustomerShipAddresses", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.CustomerShipAddresses", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.CustomerShipAddresses", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.DiscountCodes", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DiscountCodes", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DiscountCodes", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.DiscountCodes", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.DiscountCodes", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.ProductSupplierGuarantees", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductSupplierGuarantees", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductSupplierGuarantees", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.ProductSupplierGuarantees", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.ProductSupplierGuarantees", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.ProductSuppliers", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductSuppliers", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductSuppliers", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.ProductSuppliers", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.ProductSuppliers", "PrivateLabelOwner_Id", c => c.Guid());
            AddColumn("dbo.Suppliers", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Suppliers", "LastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Suppliers", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.Suppliers", "LastModifiedBy_Id", c => c.Guid());
            AddColumn("dbo.Suppliers", "PrivateLabelOwner_Id", c => c.Guid());
            CreateIndex("dbo.BankAccounts", "AddedBy_Id");
            CreateIndex("dbo.BankAccounts", "LastModifiedBy_Id");
            CreateIndex("dbo.BankAccounts", "PrivateLabelOwner_Id");
            CreateIndex("dbo.BaseTypes", "AddedBy_Id");
            CreateIndex("dbo.BaseTypes", "LastModifiedBy_Id");
            CreateIndex("dbo.BaseTypes", "PrivateLabelOwner_Id");
            CreateIndex("dbo.BaseValues", "AddedBy_Id");
            CreateIndex("dbo.BaseValues", "LastModifiedBy_Id");
            CreateIndex("dbo.BaseValues", "PrivateLabelOwner_Id");
            CreateIndex("dbo.BasketItems", "AddedBy_Id");
            CreateIndex("dbo.BasketItems", "LastModifiedBy_Id");
            CreateIndex("dbo.BasketItems", "PrivateLabelOwner_Id");
            CreateIndex("dbo.Baskets", "AddedBy_Id");
            CreateIndex("dbo.Baskets", "LastModifiedBy_Id");
            CreateIndex("dbo.Baskets", "PrivateLabelOwner_Id");
            CreateIndex("dbo.BasketNotes", "AddedBy_Id");
            CreateIndex("dbo.BasketNotes", "LastModifiedBy_Id");
            CreateIndex("dbo.BasketNotes", "PrivateLabelOwner_Id");
            CreateIndex("dbo.PurchaseOrders", "AddedBy_Id");
            CreateIndex("dbo.PurchaseOrders", "LastModifiedBy_Id");
            CreateIndex("dbo.PurchaseOrders", "PrivateLabelOwner_Id");
            CreateIndex("dbo.PurchaseOrderClearances", "AddedBy_Id");
            CreateIndex("dbo.PurchaseOrderClearances", "LastModifiedBy_Id");
            CreateIndex("dbo.PurchaseOrderClearances", "PrivateLabelOwner_Id");
            CreateIndex("dbo.PurchaseOrderHistories", "AddedBy_Id");
            CreateIndex("dbo.PurchaseOrderHistories", "LastModifiedBy_Id");
            CreateIndex("dbo.PurchaseOrderHistories", "PrivateLabelOwner_Id");
            CreateIndex("dbo.PurchaseOrderLineItems", "AddedBy_Id");
            CreateIndex("dbo.PurchaseOrderLineItems", "LastModifiedBy_Id");
            CreateIndex("dbo.PurchaseOrderLineItems", "PrivateLabelOwner_Id");
            CreateIndex("dbo.PurchaseOrderPayments", "AddedBy_Id");
            CreateIndex("dbo.PurchaseOrderPayments", "LastModifiedBy_Id");
            CreateIndex("dbo.PurchaseOrderPayments", "PrivateLabelOwner_Id");
            CreateIndex("dbo.CalendarTemplateHolidays", "AddedBy_Id");
            CreateIndex("dbo.CalendarTemplateHolidays", "LastModifiedBy_Id");
            CreateIndex("dbo.CalendarTemplateHolidays", "PrivateLabelOwner_Id");
            CreateIndex("dbo.CalendarTemplates", "AddedBy_Id");
            CreateIndex("dbo.CalendarTemplates", "LastModifiedBy_Id");
            CreateIndex("dbo.CalendarTemplates", "PrivateLabelOwner_Id");
            CreateIndex("dbo.CalendarTemplateOpenTimes", "AddedBy_Id");
            CreateIndex("dbo.CalendarTemplateOpenTimes", "LastModifiedBy_Id");
            CreateIndex("dbo.CalendarTemplateOpenTimes", "PrivateLabelOwner_Id");
            CreateIndex("dbo.StoreCalendarHistories", "AddedBy_Id");
            CreateIndex("dbo.StoreCalendarHistories", "LastModifiedBy_Id");
            CreateIndex("dbo.StoreCalendarHistories", "PrivateLabelOwner_Id");
            CreateIndex("dbo.StoreCalendars", "AddedBy_Id");
            CreateIndex("dbo.StoreCalendars", "LastModifiedBy_Id");
            CreateIndex("dbo.StoreCalendars", "PrivateLabelOwner_Id");
            CreateIndex("dbo.Stores", "AddedBy_Id");
            CreateIndex("dbo.Stores", "LastModifiedBy_Id");
            CreateIndex("dbo.Stores", "PrivateLabelOwner_Id");
            CreateIndex("dbo.StoreActions", "AddedBy_Id");
            CreateIndex("dbo.StoreActions", "LastModifiedBy_Id");
            CreateIndex("dbo.StoreActions", "PrivateLabelOwner_Id");
            CreateIndex("dbo.StoreActiveOnHands", "AddedBy_Id");
            CreateIndex("dbo.StoreActiveOnHands", "LastModifiedBy_Id");
            CreateIndex("dbo.StoreActiveOnHands", "PrivateLabelOwner_Id");
            CreateIndex("dbo.StoreActivePriceLists", "AddedBy_Id");
            CreateIndex("dbo.StoreActivePriceLists", "LastModifiedBy_Id");
            CreateIndex("dbo.StoreActivePriceLists", "PrivateLabelOwner_Id");
            CreateIndex("dbo.Products", "AddedBy_Id");
            CreateIndex("dbo.Products", "LastModifiedBy_Id");
            CreateIndex("dbo.Products", "PrivateLabelOwner_Id");
            CreateIndex("dbo.CharValues", "AddedBy_Id");
            CreateIndex("dbo.CharValues", "LastModifiedBy_Id");
            CreateIndex("dbo.CharValues", "PrivateLabelOwner_Id");
            CreateIndex("dbo.CharTypes", "AddedBy_Id");
            CreateIndex("dbo.CharTypes", "LastModifiedBy_Id");
            CreateIndex("dbo.CharTypes", "PrivateLabelOwner_Id");
            CreateIndex("dbo.CharGroupTypeInfoes", "AddedBy_Id");
            CreateIndex("dbo.CharGroupTypeInfoes", "LastModifiedBy_Id");
            CreateIndex("dbo.CharGroupTypeInfoes", "PrivateLabelOwner_Id");
            CreateIndex("dbo.ProductBases", "AddedBy_Id");
            CreateIndex("dbo.ProductBases", "LastModifiedBy_Id");
            CreateIndex("dbo.ProductBases", "PrivateLabelOwner_Id");
            CreateIndex("dbo.ProductComments", "AddedBy_Id");
            CreateIndex("dbo.ProductComments", "LastModifiedBy_Id");
            CreateIndex("dbo.ProductComments", "PrivateLabelOwner_Id");
            CreateIndex("dbo.ProductGroups", "AddedBy_Id");
            CreateIndex("dbo.ProductGroups", "LastModifiedBy_Id");
            CreateIndex("dbo.ProductGroups", "PrivateLabelOwner_Id");
            CreateIndex("dbo.ProductPictures", "AddedBy_Id");
            CreateIndex("dbo.ProductPictures", "LastModifiedBy_Id");
            CreateIndex("dbo.ProductPictures", "PrivateLabelOwner_Id");
            CreateIndex("dbo.ProductRates", "AddedBy_Id");
            CreateIndex("dbo.ProductRates", "LastModifiedBy_Id");
            CreateIndex("dbo.ProductRates", "PrivateLabelOwner_Id");
            CreateIndex("dbo.StoreDeliveryPersons", "AddedBy_Id");
            CreateIndex("dbo.StoreDeliveryPersons", "LastModifiedBy_Id");
            CreateIndex("dbo.StoreDeliveryPersons", "PrivateLabelOwner_Id");
            CreateIndex("dbo.DeliveryPersons", "AddedBy_Id");
            CreateIndex("dbo.DeliveryPersons", "LastModifiedBy_Id");
            CreateIndex("dbo.DeliveryPersons", "PrivateLabelOwner_Id");
            CreateIndex("dbo.StoreValidRegionInfoes", "AddedBy_Id");
            CreateIndex("dbo.StoreValidRegionInfoes", "LastModifiedBy_Id");
            CreateIndex("dbo.StoreValidRegionInfoes", "PrivateLabelOwner_Id");
            CreateIndex("dbo.CityRegions", "AddedBy_Id");
            CreateIndex("dbo.CityRegions", "LastModifiedBy_Id");
            CreateIndex("dbo.CityRegions", "PrivateLabelOwner_Id");
            CreateIndex("dbo.CharCategories", "AddedBy_Id");
            CreateIndex("dbo.CharCategories", "LastModifiedBy_Id");
            CreateIndex("dbo.CharCategories", "PrivateLabelOwner_Id");
            CreateIndex("dbo.CharGroups", "AddedBy_Id");
            CreateIndex("dbo.CharGroups", "LastModifiedBy_Id");
            CreateIndex("dbo.CharGroups", "PrivateLabelOwner_Id");
            CreateIndex("dbo.Clearances", "AddedBy_Id");
            CreateIndex("dbo.Clearances", "LastModifiedBy_Id");
            CreateIndex("dbo.Clearances", "PrivateLabelOwner_Id");
            CreateIndex("dbo.Customers", "AddedBy_Id");
            CreateIndex("dbo.Customers", "LastModifiedBy_Id");
            CreateIndex("dbo.Customers", "PrivateLabelOwner_Id");
            CreateIndex("dbo.CustomerShipAddresses", "AddedBy_Id");
            CreateIndex("dbo.CustomerShipAddresses", "LastModifiedBy_Id");
            CreateIndex("dbo.CustomerShipAddresses", "PrivateLabelOwner_Id");
            CreateIndex("dbo.DiscountCodes", "AddedBy_Id");
            CreateIndex("dbo.DiscountCodes", "LastModifiedBy_Id");
            CreateIndex("dbo.DiscountCodes", "PrivateLabelOwner_Id");
            CreateIndex("dbo.ProductSupplierGuarantees", "AddedBy_Id");
            CreateIndex("dbo.ProductSupplierGuarantees", "LastModifiedBy_Id");
            CreateIndex("dbo.ProductSupplierGuarantees", "PrivateLabelOwner_Id");
            CreateIndex("dbo.ProductSuppliers", "AddedBy_Id");
            CreateIndex("dbo.ProductSuppliers", "LastModifiedBy_Id");
            CreateIndex("dbo.ProductSuppliers", "PrivateLabelOwner_Id");
            CreateIndex("dbo.Suppliers", "AddedBy_Id");
            CreateIndex("dbo.Suppliers", "LastModifiedBy_Id");
            CreateIndex("dbo.Suppliers", "PrivateLabelOwner_Id");
            AddForeignKey("dbo.BankAccounts", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.BankAccounts", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.BankAccounts", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.BaseTypes", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.BaseTypes", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.BaseTypes", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.BaseValues", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.BaseValues", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.BaseValues", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.BasketItems", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Baskets", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.BasketNotes", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.BasketNotes", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.BasketNotes", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Baskets", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Baskets", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PurchaseOrders", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PurchaseOrders", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PurchaseOrders", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PurchaseOrderClearances", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PurchaseOrderClearances", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PurchaseOrderClearances", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PurchaseOrderHistories", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PurchaseOrderHistories", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PurchaseOrderHistories", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PurchaseOrderLineItems", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PurchaseOrderLineItems", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PurchaseOrderLineItems", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PurchaseOrderPayments", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PurchaseOrderPayments", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PurchaseOrderPayments", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.BasketItems", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.BasketItems", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CalendarTemplateHolidays", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CalendarTemplates", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CalendarTemplateOpenTimes", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CalendarTemplateOpenTimes", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CalendarTemplateOpenTimes", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CalendarTemplates", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CalendarTemplates", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreCalendarHistories", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreCalendarHistories", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreCalendarHistories", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreCalendars", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreCalendars", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreCalendars", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Stores", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Stores", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Stores", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreActions", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreActions", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreActions", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreActiveOnHands", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreActiveOnHands", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreActiveOnHands", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreActivePriceLists", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreActivePriceLists", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreActivePriceLists", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Products", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CharValues", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CharTypes", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CharGroupTypeInfoes", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CharGroupTypeInfoes", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CharGroupTypeInfoes", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CharTypes", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CharTypes", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CharValues", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CharValues", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Products", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Products", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductBases", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductBases", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductBases", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductComments", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductComments", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductComments", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductGroups", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductGroups", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductGroups", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductPictures", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductPictures", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductPictures", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductRates", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductRates", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductRates", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreDeliveryPersons", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.DeliveryPersons", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.DeliveryPersons", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.DeliveryPersons", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreDeliveryPersons", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreDeliveryPersons", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreValidRegionInfoes", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CityRegions", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CityRegions", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CityRegions", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreValidRegionInfoes", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.StoreValidRegionInfoes", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CalendarTemplateHolidays", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CalendarTemplateHolidays", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CharCategories", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CharCategories", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CharCategories", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CharGroups", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CharGroups", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CharGroups", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Clearances", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Clearances", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Clearances", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Customers", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CustomerShipAddresses", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CustomerShipAddresses", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.CustomerShipAddresses", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Customers", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Customers", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.DiscountCodes", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.DiscountCodes", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.DiscountCodes", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductSupplierGuarantees", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductSupplierGuarantees", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductSupplierGuarantees", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductSuppliers", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductSuppliers", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.ProductSuppliers", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Suppliers", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Suppliers", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.Suppliers", "PrivateLabelOwner_Id", "dbo.Principals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Suppliers", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Suppliers", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Suppliers", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductSuppliers", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductSuppliers", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductSuppliers", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductSupplierGuarantees", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductSupplierGuarantees", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductSupplierGuarantees", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Roles", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Roles", "Principal_Id", "dbo.Principals");
            DropForeignKey("dbo.Roles", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Roles", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Users", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Users", "Principal_Id", "dbo.Principals");
            DropForeignKey("dbo.Users", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Users", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Users", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Groups", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Groups", "Principal_Id", "dbo.Principals");
            DropForeignKey("dbo.Groups", "Manager_Id", "dbo.Principals");
            DropForeignKey("dbo.Groups", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Groups", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.DiscountCodes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.DiscountCodes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.DiscountCodes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Customers", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Customers", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CustomerShipAddresses", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CustomerShipAddresses", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CustomerShipAddresses", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Customers", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Clearances", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Clearances", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Clearances", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CharGroups", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CharGroups", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CharGroups", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CharCategories", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CharCategories", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CharCategories", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplateHolidays", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplateHolidays", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreValidRegionInfoes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreValidRegionInfoes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CityRegions", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CityRegions", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CityRegions", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreValidRegionInfoes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreDeliveryPersons", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreDeliveryPersons", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.DeliveryPersons", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.DeliveryPersons", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.DeliveryPersons", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreDeliveryPersons", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductRates", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductRates", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductRates", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductPictures", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductPictures", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductPictures", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductGroups", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductGroups", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductGroups", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductComments", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductComments", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductComments", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductBases", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductBases", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.ProductBases", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Products", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Products", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CharValues", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CharValues", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CharTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CharTypes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CharGroupTypeInfoes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CharGroupTypeInfoes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CharGroupTypeInfoes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CharTypes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CharValues", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Products", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActivePriceLists", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActivePriceLists", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActivePriceLists", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActiveOnHands", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActiveOnHands", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActiveOnHands", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActions", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActions", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreActions", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Stores", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Stores", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Stores", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreCalendars", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreCalendars", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreCalendars", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreCalendarHistories", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreCalendarHistories", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StoreCalendarHistories", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplates", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplates", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplates", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.CalendarTemplateHolidays", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BasketItems", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.BasketItems", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderPayments", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderPayments", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderPayments", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderLineItems", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderLineItems", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderLineItems", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderHistories", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderHistories", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderHistories", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderClearances", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderClearances", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrderClearances", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrders", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrders", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.PurchaseOrders", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Baskets", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.Baskets", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BasketNotes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.BasketNotes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BasketNotes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.Baskets", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BasketItems", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BaseValues", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.BaseValues", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BaseValues", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.BaseTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.BaseTypes", "LastModifiedBy_Id", "dbo.Principals");
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
            DropIndex("dbo.Suppliers", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Suppliers", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Suppliers", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductSuppliers", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ProductSuppliers", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductSuppliers", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductSupplierGuarantees", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ProductSupplierGuarantees", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductSupplierGuarantees", new[] { "AddedBy_Id" });
            DropIndex("dbo.Roles", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Roles", new[] { "Principal_Id" });
            DropIndex("dbo.Roles", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Roles", new[] { "AddedBy_Id" });
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.Users", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Users", new[] { "Principal_Id" });
            DropIndex("dbo.Users", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Users", new[] { "Group_Id" });
            DropIndex("dbo.Users", new[] { "AddedBy_Id" });
            DropIndex("dbo.Groups", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Groups", new[] { "Principal_Id" });
            DropIndex("dbo.Groups", new[] { "Manager_Id" });
            DropIndex("dbo.Groups", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Groups", new[] { "AddedBy_Id" });
            DropIndex("dbo.DiscountCodes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.DiscountCodes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.DiscountCodes", new[] { "AddedBy_Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "AddedBy_Id" });
            DropIndex("dbo.Customers", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Customers", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Customers", new[] { "AddedBy_Id" });
            DropIndex("dbo.Clearances", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Clearances", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Clearances", new[] { "AddedBy_Id" });
            DropIndex("dbo.CharGroups", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CharGroups", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CharGroups", new[] { "AddedBy_Id" });
            DropIndex("dbo.CharCategories", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CharCategories", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CharCategories", new[] { "AddedBy_Id" });
            DropIndex("dbo.CityRegions", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CityRegions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CityRegions", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreValidRegionInfoes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StoreValidRegionInfoes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreValidRegionInfoes", new[] { "AddedBy_Id" });
            DropIndex("dbo.DeliveryPersons", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.DeliveryPersons", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.DeliveryPersons", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreDeliveryPersons", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductRates", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ProductRates", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductRates", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductPictures", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ProductPictures", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductPictures", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductGroups", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ProductGroups", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductGroups", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductComments", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ProductComments", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductComments", new[] { "AddedBy_Id" });
            DropIndex("dbo.ProductBases", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.ProductBases", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.ProductBases", new[] { "AddedBy_Id" });
            DropIndex("dbo.CharGroupTypeInfoes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CharGroupTypeInfoes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CharGroupTypeInfoes", new[] { "AddedBy_Id" });
            DropIndex("dbo.CharTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CharTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CharTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.CharValues", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CharValues", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CharValues", new[] { "AddedBy_Id" });
            DropIndex("dbo.Products", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Products", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Products", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreActivePriceLists", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreActiveOnHands", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StoreActiveOnHands", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreActiveOnHands", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreActions", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StoreActions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreActions", new[] { "AddedBy_Id" });
            DropIndex("dbo.Stores", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Stores", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Stores", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreCalendars", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StoreCalendars", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreCalendars", new[] { "AddedBy_Id" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StoreCalendarHistories", new[] { "AddedBy_Id" });
            DropIndex("dbo.CalendarTemplateOpenTimes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CalendarTemplateOpenTimes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CalendarTemplateOpenTimes", new[] { "AddedBy_Id" });
            DropIndex("dbo.CalendarTemplates", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CalendarTemplates", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CalendarTemplates", new[] { "AddedBy_Id" });
            DropIndex("dbo.CalendarTemplateHolidays", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.CalendarTemplateHolidays", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.CalendarTemplateHolidays", new[] { "AddedBy_Id" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PurchaseOrderPayments", new[] { "AddedBy_Id" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "AddedBy_Id" });
            DropIndex("dbo.PurchaseOrderHistories", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.PurchaseOrderHistories", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PurchaseOrderHistories", new[] { "AddedBy_Id" });
            DropIndex("dbo.PurchaseOrderClearances", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.PurchaseOrderClearances", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PurchaseOrderClearances", new[] { "AddedBy_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "AddedBy_Id" });
            DropIndex("dbo.BasketNotes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.BasketNotes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.BasketNotes", new[] { "AddedBy_Id" });
            DropIndex("dbo.Baskets", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Baskets", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Baskets", new[] { "AddedBy_Id" });
            DropIndex("dbo.BasketItems", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.BasketItems", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.BasketItems", new[] { "AddedBy_Id" });
            DropIndex("dbo.BaseValues", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.BaseValues", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.BaseValues", new[] { "AddedBy_Id" });
            DropIndex("dbo.BaseTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.BaseTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.BaseTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.Permissions", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.Permissions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Permissions", new[] { "AddedBy_Id" });
            DropIndex("dbo.PrincipalPermissions", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.PrincipalPermissions", new[] { "Principal_Id" });
            DropIndex("dbo.PrincipalPermissions", new[] { "Permission_Id" });
            DropIndex("dbo.PrincipalPermissions", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.PrincipalPermissions", new[] { "AddedBy_Id" });
            DropIndex("dbo.BankAccounts", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.BankAccounts", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.BankAccounts", new[] { "AddedBy_Id" });
            DropColumn("dbo.Suppliers", "PrivateLabelOwner_Id");
            DropColumn("dbo.Suppliers", "LastModifiedBy_Id");
            DropColumn("dbo.Suppliers", "AddedBy_Id");
            DropColumn("dbo.Suppliers", "LastUpdate");
            DropColumn("dbo.Suppliers", "CreatedDate");
            DropColumn("dbo.ProductSuppliers", "PrivateLabelOwner_Id");
            DropColumn("dbo.ProductSuppliers", "LastModifiedBy_Id");
            DropColumn("dbo.ProductSuppliers", "AddedBy_Id");
            DropColumn("dbo.ProductSuppliers", "LastUpdate");
            DropColumn("dbo.ProductSuppliers", "CreatedDate");
            DropColumn("dbo.ProductSupplierGuarantees", "PrivateLabelOwner_Id");
            DropColumn("dbo.ProductSupplierGuarantees", "LastModifiedBy_Id");
            DropColumn("dbo.ProductSupplierGuarantees", "AddedBy_Id");
            DropColumn("dbo.ProductSupplierGuarantees", "LastUpdate");
            DropColumn("dbo.ProductSupplierGuarantees", "CreatedDate");
            DropColumn("dbo.DiscountCodes", "PrivateLabelOwner_Id");
            DropColumn("dbo.DiscountCodes", "LastModifiedBy_Id");
            DropColumn("dbo.DiscountCodes", "AddedBy_Id");
            DropColumn("dbo.DiscountCodes", "LastUpdate");
            DropColumn("dbo.DiscountCodes", "CreatedDate");
            DropColumn("dbo.CustomerShipAddresses", "PrivateLabelOwner_Id");
            DropColumn("dbo.CustomerShipAddresses", "LastModifiedBy_Id");
            DropColumn("dbo.CustomerShipAddresses", "AddedBy_Id");
            DropColumn("dbo.CustomerShipAddresses", "LastUpdate");
            DropColumn("dbo.CustomerShipAddresses", "CreatedDate");
            DropColumn("dbo.Customers", "PrivateLabelOwner_Id");
            DropColumn("dbo.Customers", "LastModifiedBy_Id");
            DropColumn("dbo.Customers", "AddedBy_Id");
            DropColumn("dbo.Customers", "LastUpdate");
            DropColumn("dbo.Customers", "CreatedDate");
            DropColumn("dbo.Clearances", "PrivateLabelOwner_Id");
            DropColumn("dbo.Clearances", "LastModifiedBy_Id");
            DropColumn("dbo.Clearances", "AddedBy_Id");
            DropColumn("dbo.Clearances", "LastUpdate");
            DropColumn("dbo.Clearances", "CreatedDate");
            DropColumn("dbo.CharGroups", "PrivateLabelOwner_Id");
            DropColumn("dbo.CharGroups", "LastModifiedBy_Id");
            DropColumn("dbo.CharGroups", "AddedBy_Id");
            DropColumn("dbo.CharGroups", "LastUpdate");
            DropColumn("dbo.CharGroups", "CreatedDate");
            DropColumn("dbo.CharCategories", "PrivateLabelOwner_Id");
            DropColumn("dbo.CharCategories", "LastModifiedBy_Id");
            DropColumn("dbo.CharCategories", "AddedBy_Id");
            DropColumn("dbo.CharCategories", "LastUpdate");
            DropColumn("dbo.CharCategories", "CreatedDate");
            DropColumn("dbo.CityRegions", "PrivateLabelOwner_Id");
            DropColumn("dbo.CityRegions", "LastModifiedBy_Id");
            DropColumn("dbo.CityRegions", "AddedBy_Id");
            DropColumn("dbo.CityRegions", "LastUpdate");
            DropColumn("dbo.CityRegions", "CreatedDate");
            DropColumn("dbo.StoreValidRegionInfoes", "PrivateLabelOwner_Id");
            DropColumn("dbo.StoreValidRegionInfoes", "LastModifiedBy_Id");
            DropColumn("dbo.StoreValidRegionInfoes", "AddedBy_Id");
            DropColumn("dbo.StoreValidRegionInfoes", "LastUpdate");
            DropColumn("dbo.StoreValidRegionInfoes", "CreatedDate");
            DropColumn("dbo.DeliveryPersons", "PrivateLabelOwner_Id");
            DropColumn("dbo.DeliveryPersons", "LastModifiedBy_Id");
            DropColumn("dbo.DeliveryPersons", "AddedBy_Id");
            DropColumn("dbo.DeliveryPersons", "LastUpdate");
            DropColumn("dbo.DeliveryPersons", "CreatedDate");
            DropColumn("dbo.StoreDeliveryPersons", "PrivateLabelOwner_Id");
            DropColumn("dbo.StoreDeliveryPersons", "LastModifiedBy_Id");
            DropColumn("dbo.StoreDeliveryPersons", "AddedBy_Id");
            DropColumn("dbo.StoreDeliveryPersons", "LastUpdate");
            DropColumn("dbo.StoreDeliveryPersons", "CreatedDate");
            DropColumn("dbo.ProductRates", "PrivateLabelOwner_Id");
            DropColumn("dbo.ProductRates", "LastModifiedBy_Id");
            DropColumn("dbo.ProductRates", "AddedBy_Id");
            DropColumn("dbo.ProductRates", "LastUpdate");
            DropColumn("dbo.ProductRates", "CreatedDate");
            DropColumn("dbo.ProductPictures", "PrivateLabelOwner_Id");
            DropColumn("dbo.ProductPictures", "LastModifiedBy_Id");
            DropColumn("dbo.ProductPictures", "AddedBy_Id");
            DropColumn("dbo.ProductPictures", "LastUpdate");
            DropColumn("dbo.ProductPictures", "CreatedDate");
            DropColumn("dbo.ProductGroups", "PrivateLabelOwner_Id");
            DropColumn("dbo.ProductGroups", "LastModifiedBy_Id");
            DropColumn("dbo.ProductGroups", "AddedBy_Id");
            DropColumn("dbo.ProductGroups", "LastUpdate");
            DropColumn("dbo.ProductGroups", "CreatedDate");
            DropColumn("dbo.ProductComments", "PrivateLabelOwner_Id");
            DropColumn("dbo.ProductComments", "LastModifiedBy_Id");
            DropColumn("dbo.ProductComments", "AddedBy_Id");
            DropColumn("dbo.ProductComments", "LastUpdate");
            DropColumn("dbo.ProductComments", "CreatedDate");
            DropColumn("dbo.ProductBases", "PrivateLabelOwner_Id");
            DropColumn("dbo.ProductBases", "LastModifiedBy_Id");
            DropColumn("dbo.ProductBases", "AddedBy_Id");
            DropColumn("dbo.ProductBases", "LastUpdate");
            DropColumn("dbo.ProductBases", "CreatedDate");
            DropColumn("dbo.CharGroupTypeInfoes", "PrivateLabelOwner_Id");
            DropColumn("dbo.CharGroupTypeInfoes", "LastModifiedBy_Id");
            DropColumn("dbo.CharGroupTypeInfoes", "AddedBy_Id");
            DropColumn("dbo.CharGroupTypeInfoes", "LastUpdate");
            DropColumn("dbo.CharGroupTypeInfoes", "CreatedDate");
            DropColumn("dbo.CharTypes", "PrivateLabelOwner_Id");
            DropColumn("dbo.CharTypes", "LastModifiedBy_Id");
            DropColumn("dbo.CharTypes", "AddedBy_Id");
            DropColumn("dbo.CharTypes", "LastUpdate");
            DropColumn("dbo.CharTypes", "CreatedDate");
            DropColumn("dbo.CharValues", "PrivateLabelOwner_Id");
            DropColumn("dbo.CharValues", "LastModifiedBy_Id");
            DropColumn("dbo.CharValues", "AddedBy_Id");
            DropColumn("dbo.CharValues", "LastUpdate");
            DropColumn("dbo.CharValues", "CreatedDate");
            DropColumn("dbo.Products", "PrivateLabelOwner_Id");
            DropColumn("dbo.Products", "LastModifiedBy_Id");
            DropColumn("dbo.Products", "AddedBy_Id");
            DropColumn("dbo.Products", "LastUpdate");
            DropColumn("dbo.Products", "CreatedDate");
            DropColumn("dbo.StoreActivePriceLists", "PrivateLabelOwner_Id");
            DropColumn("dbo.StoreActivePriceLists", "LastModifiedBy_Id");
            DropColumn("dbo.StoreActivePriceLists", "AddedBy_Id");
            DropColumn("dbo.StoreActivePriceLists", "LastUpdate");
            DropColumn("dbo.StoreActivePriceLists", "CreatedDate");
            DropColumn("dbo.StoreActiveOnHands", "PrivateLabelOwner_Id");
            DropColumn("dbo.StoreActiveOnHands", "LastModifiedBy_Id");
            DropColumn("dbo.StoreActiveOnHands", "AddedBy_Id");
            DropColumn("dbo.StoreActiveOnHands", "LastUpdate");
            DropColumn("dbo.StoreActiveOnHands", "CreatedDate");
            DropColumn("dbo.StoreActions", "PrivateLabelOwner_Id");
            DropColumn("dbo.StoreActions", "LastModifiedBy_Id");
            DropColumn("dbo.StoreActions", "AddedBy_Id");
            DropColumn("dbo.StoreActions", "LastUpdate");
            DropColumn("dbo.StoreActions", "CreatedDate");
            DropColumn("dbo.Stores", "PrivateLabelOwner_Id");
            DropColumn("dbo.Stores", "LastModifiedBy_Id");
            DropColumn("dbo.Stores", "AddedBy_Id");
            DropColumn("dbo.Stores", "LastUpdate");
            DropColumn("dbo.Stores", "CreatedDate");
            DropColumn("dbo.StoreCalendars", "PrivateLabelOwner_Id");
            DropColumn("dbo.StoreCalendars", "LastModifiedBy_Id");
            DropColumn("dbo.StoreCalendars", "AddedBy_Id");
            DropColumn("dbo.StoreCalendars", "LastUpdate");
            DropColumn("dbo.StoreCalendars", "CreatedDate");
            DropColumn("dbo.StoreCalendarHistories", "PrivateLabelOwner_Id");
            DropColumn("dbo.StoreCalendarHistories", "LastModifiedBy_Id");
            DropColumn("dbo.StoreCalendarHistories", "AddedBy_Id");
            DropColumn("dbo.StoreCalendarHistories", "LastUpdate");
            DropColumn("dbo.StoreCalendarHistories", "CreatedDate");
            DropColumn("dbo.CalendarTemplateOpenTimes", "PrivateLabelOwner_Id");
            DropColumn("dbo.CalendarTemplateOpenTimes", "LastModifiedBy_Id");
            DropColumn("dbo.CalendarTemplateOpenTimes", "AddedBy_Id");
            DropColumn("dbo.CalendarTemplateOpenTimes", "LastUpdate");
            DropColumn("dbo.CalendarTemplateOpenTimes", "CreatedDate");
            DropColumn("dbo.CalendarTemplates", "PrivateLabelOwner_Id");
            DropColumn("dbo.CalendarTemplates", "LastModifiedBy_Id");
            DropColumn("dbo.CalendarTemplates", "AddedBy_Id");
            DropColumn("dbo.CalendarTemplates", "LastUpdate");
            DropColumn("dbo.CalendarTemplates", "CreatedDate");
            DropColumn("dbo.CalendarTemplateHolidays", "PrivateLabelOwner_Id");
            DropColumn("dbo.CalendarTemplateHolidays", "LastModifiedBy_Id");
            DropColumn("dbo.CalendarTemplateHolidays", "AddedBy_Id");
            DropColumn("dbo.CalendarTemplateHolidays", "LastUpdate");
            DropColumn("dbo.CalendarTemplateHolidays", "CreatedDate");
            DropColumn("dbo.PurchaseOrderPayments", "PrivateLabelOwner_Id");
            DropColumn("dbo.PurchaseOrderPayments", "LastModifiedBy_Id");
            DropColumn("dbo.PurchaseOrderPayments", "AddedBy_Id");
            DropColumn("dbo.PurchaseOrderPayments", "LastUpdate");
            DropColumn("dbo.PurchaseOrderPayments", "CreatedDate");
            DropColumn("dbo.PurchaseOrderLineItems", "PrivateLabelOwner_Id");
            DropColumn("dbo.PurchaseOrderLineItems", "LastModifiedBy_Id");
            DropColumn("dbo.PurchaseOrderLineItems", "AddedBy_Id");
            DropColumn("dbo.PurchaseOrderLineItems", "LastUpdate");
            DropColumn("dbo.PurchaseOrderLineItems", "CreatedDate");
            DropColumn("dbo.PurchaseOrderHistories", "PrivateLabelOwner_Id");
            DropColumn("dbo.PurchaseOrderHistories", "LastModifiedBy_Id");
            DropColumn("dbo.PurchaseOrderHistories", "AddedBy_Id");
            DropColumn("dbo.PurchaseOrderHistories", "LastUpdate");
            DropColumn("dbo.PurchaseOrderHistories", "CreatedDate");
            DropColumn("dbo.PurchaseOrderClearances", "PrivateLabelOwner_Id");
            DropColumn("dbo.PurchaseOrderClearances", "LastModifiedBy_Id");
            DropColumn("dbo.PurchaseOrderClearances", "AddedBy_Id");
            DropColumn("dbo.PurchaseOrderClearances", "LastUpdate");
            DropColumn("dbo.PurchaseOrderClearances", "CreatedDate");
            DropColumn("dbo.PurchaseOrders", "PrivateLabelOwner_Id");
            DropColumn("dbo.PurchaseOrders", "LastModifiedBy_Id");
            DropColumn("dbo.PurchaseOrders", "AddedBy_Id");
            DropColumn("dbo.PurchaseOrders", "LastUpdate");
            DropColumn("dbo.PurchaseOrders", "CreatedDate");
            DropColumn("dbo.BasketNotes", "PrivateLabelOwner_Id");
            DropColumn("dbo.BasketNotes", "LastModifiedBy_Id");
            DropColumn("dbo.BasketNotes", "AddedBy_Id");
            DropColumn("dbo.BasketNotes", "LastUpdate");
            DropColumn("dbo.BasketNotes", "CreatedDate");
            DropColumn("dbo.Baskets", "PrivateLabelOwner_Id");
            DropColumn("dbo.Baskets", "LastModifiedBy_Id");
            DropColumn("dbo.Baskets", "AddedBy_Id");
            DropColumn("dbo.Baskets", "LastUpdate");
            DropColumn("dbo.Baskets", "CreatedDate");
            DropColumn("dbo.BasketItems", "PrivateLabelOwner_Id");
            DropColumn("dbo.BasketItems", "LastModifiedBy_Id");
            DropColumn("dbo.BasketItems", "AddedBy_Id");
            DropColumn("dbo.BasketItems", "LastUpdate");
            DropColumn("dbo.BasketItems", "CreatedDate");
            DropColumn("dbo.BaseValues", "PrivateLabelOwner_Id");
            DropColumn("dbo.BaseValues", "LastModifiedBy_Id");
            DropColumn("dbo.BaseValues", "AddedBy_Id");
            DropColumn("dbo.BaseValues", "LastUpdate");
            DropColumn("dbo.BaseValues", "CreatedDate");
            DropColumn("dbo.BaseTypes", "PrivateLabelOwner_Id");
            DropColumn("dbo.BaseTypes", "LastModifiedBy_Id");
            DropColumn("dbo.BaseTypes", "AddedBy_Id");
            DropColumn("dbo.BaseTypes", "LastUpdate");
            DropColumn("dbo.BaseTypes", "CreatedDate");
            DropColumn("dbo.BankAccounts", "PrivateLabelOwner_Id");
            DropColumn("dbo.BankAccounts", "LastModifiedBy_Id");
            DropColumn("dbo.BankAccounts", "AddedBy_Id");
            DropColumn("dbo.BankAccounts", "LastUpdate");
            DropColumn("dbo.BankAccounts", "CreatedDate");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Groups");
            DropTable("dbo.Permissions");
            DropTable("dbo.PrincipalPermissions");
            DropTable("dbo.Principals");
        }
    }
}
