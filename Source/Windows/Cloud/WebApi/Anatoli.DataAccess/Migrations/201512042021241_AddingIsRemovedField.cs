namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingIsRemovedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.PrincipalPermissions", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.BaseTypes", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.BaseValues", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.BasketItems", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Baskets", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.BasketNotes", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.PurchaseOrders", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.PurchaseOrderClearances", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.PurchaseOrderHistories", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.PurchaseOrderLineItems", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.PurchaseOrderPayments", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.CalendarTemplateHolidays", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.CalendarTemplates", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.CalendarTemplateOpenTimes", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.StoreCalendarHistories", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.StoreCalendars", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stores", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.StoreActions", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.StoreActiveOnHands", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.StoreActivePriceLists", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.CharValues", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.CharTypes", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.CharGroupTypeInfoes", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductBases", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductComments", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductGroups", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPictures", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductRates", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Suppliers", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.StoreDeliveryPersons", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeliveryPersons", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.StoreValidRegionInfoes", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.CityRegions", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.CharCategories", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.CharGroups", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Clearances", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.CustomerShipAddresses", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.DiscountCodes", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Groups", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductSupplierGuarantees", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductSupplierGuarantees", "IsRemoved");
            DropColumn("dbo.Roles", "IsRemoved");
            DropColumn("dbo.Users", "IsRemoved");
            DropColumn("dbo.Groups", "IsRemoved");
            DropColumn("dbo.DiscountCodes", "IsRemoved");
            DropColumn("dbo.CustomerShipAddresses", "IsRemoved");
            DropColumn("dbo.Customers", "IsRemoved");
            DropColumn("dbo.Clearances", "IsRemoved");
            DropColumn("dbo.CharGroups", "IsRemoved");
            DropColumn("dbo.CharCategories", "IsRemoved");
            DropColumn("dbo.CityRegions", "IsRemoved");
            DropColumn("dbo.StoreValidRegionInfoes", "IsRemoved");
            DropColumn("dbo.DeliveryPersons", "IsRemoved");
            DropColumn("dbo.StoreDeliveryPersons", "IsRemoved");
            DropColumn("dbo.Suppliers", "IsRemoved");
            DropColumn("dbo.ProductRates", "IsRemoved");
            DropColumn("dbo.ProductPictures", "IsRemoved");
            DropColumn("dbo.ProductGroups", "IsRemoved");
            DropColumn("dbo.ProductComments", "IsRemoved");
            DropColumn("dbo.ProductBases", "IsRemoved");
            DropColumn("dbo.CharGroupTypeInfoes", "IsRemoved");
            DropColumn("dbo.CharTypes", "IsRemoved");
            DropColumn("dbo.CharValues", "IsRemoved");
            DropColumn("dbo.Products", "IsRemoved");
            DropColumn("dbo.StoreActivePriceLists", "IsRemoved");
            DropColumn("dbo.StoreActiveOnHands", "IsRemoved");
            DropColumn("dbo.StoreActions", "IsRemoved");
            DropColumn("dbo.Stores", "IsRemoved");
            DropColumn("dbo.StoreCalendars", "IsRemoved");
            DropColumn("dbo.StoreCalendarHistories", "IsRemoved");
            DropColumn("dbo.CalendarTemplateOpenTimes", "IsRemoved");
            DropColumn("dbo.CalendarTemplates", "IsRemoved");
            DropColumn("dbo.CalendarTemplateHolidays", "IsRemoved");
            DropColumn("dbo.PurchaseOrderPayments", "IsRemoved");
            DropColumn("dbo.PurchaseOrderLineItems", "IsRemoved");
            DropColumn("dbo.PurchaseOrderHistories", "IsRemoved");
            DropColumn("dbo.PurchaseOrderClearances", "IsRemoved");
            DropColumn("dbo.PurchaseOrders", "IsRemoved");
            DropColumn("dbo.BasketNotes", "IsRemoved");
            DropColumn("dbo.Baskets", "IsRemoved");
            DropColumn("dbo.BasketItems", "IsRemoved");
            DropColumn("dbo.BaseValues", "IsRemoved");
            DropColumn("dbo.BaseTypes", "IsRemoved");
            DropColumn("dbo.Permissions", "IsRemoved");
            DropColumn("dbo.PrincipalPermissions", "IsRemoved");
            DropColumn("dbo.BankAccounts", "IsRemoved");
        }
    }
}
