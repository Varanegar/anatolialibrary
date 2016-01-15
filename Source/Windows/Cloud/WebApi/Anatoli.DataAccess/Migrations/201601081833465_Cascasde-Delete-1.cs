namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascasdeDelete1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrincipalPermissions", "Principal_Id", "dbo.Principals");
            DropForeignKey("dbo.PrincipalPermissions", "Permission_Id", "dbo.Permissions");
            DropForeignKey("dbo.BasketItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Stocks", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.StockProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StoreActivePriceLists", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CharValues", "CharTypeId", "dbo.CharTypes");
            DropForeignKey("dbo.StockProductRequestProductDetails", "StockProductRequestRuleId", "dbo.StockProductRequestRules");
            DropForeignKey("dbo.StockProductRequests", "StockOnHandSyncId", "dbo.StockOnHandSyncs");
            DropForeignKey("dbo.StockActiveOnHands", "StockOnHandSyncId", "dbo.StockOnHandSyncs");
            DropForeignKey("dbo.CalendarTemplateHolidays", "CalendarTemplate_Id", "dbo.CalendarTemplates");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "CalendarTemplate_Id", "dbo.CalendarTemplates");
            DropForeignKey("dbo.StoreDeliveryPersons", "DeliveryPerson_Id", "dbo.DeliveryPersons");
            AddForeignKey("dbo.PrincipalPermissions", "Principal_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.PrincipalPermissions", "Permission_Id", "dbo.Permissions", "Id");
            AddForeignKey("dbo.BasketItems", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.Stocks", "StoreId", "dbo.Stores", "Id");
            AddForeignKey("dbo.StockProducts", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.StoreActivePriceLists", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.CharValues", "CharTypeId", "dbo.CharTypes", "Id");
            AddForeignKey("dbo.StockProductRequestProductDetails", "StockProductRequestRuleId", "dbo.StockProductRequestRules", "Id");
            AddForeignKey("dbo.StockProductRequests", "StockOnHandSyncId", "dbo.StockOnHandSyncs", "Id");
            AddForeignKey("dbo.StockActiveOnHands", "StockOnHandSyncId", "dbo.StockOnHandSyncs", "Id");
            AddForeignKey("dbo.CalendarTemplateHolidays", "CalendarTemplate_Id", "dbo.CalendarTemplates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CalendarTemplateOpenTimes", "CalendarTemplate_Id", "dbo.CalendarTemplates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StoreDeliveryPersons", "DeliveryPerson_Id", "dbo.DeliveryPersons", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoreDeliveryPersons", "DeliveryPerson_Id", "dbo.DeliveryPersons");
            DropForeignKey("dbo.CalendarTemplateOpenTimes", "CalendarTemplate_Id", "dbo.CalendarTemplates");
            DropForeignKey("dbo.CalendarTemplateHolidays", "CalendarTemplate_Id", "dbo.CalendarTemplates");
            DropForeignKey("dbo.StockActiveOnHands", "StockOnHandSyncId", "dbo.StockOnHandSyncs");
            DropForeignKey("dbo.StockProductRequests", "StockOnHandSyncId", "dbo.StockOnHandSyncs");
            DropForeignKey("dbo.StockProductRequestProductDetails", "StockProductRequestRuleId", "dbo.StockProductRequestRules");
            DropForeignKey("dbo.CharValues", "CharTypeId", "dbo.CharTypes");
            DropForeignKey("dbo.StoreActivePriceLists", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Stocks", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.BasketItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PrincipalPermissions", "Permission_Id", "dbo.Permissions");
            DropForeignKey("dbo.PrincipalPermissions", "Principal_Id", "dbo.Principals");
            AddForeignKey("dbo.StoreDeliveryPersons", "DeliveryPerson_Id", "dbo.DeliveryPersons", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CalendarTemplateOpenTimes", "CalendarTemplate_Id", "dbo.CalendarTemplates", "Id");
            AddForeignKey("dbo.CalendarTemplateHolidays", "CalendarTemplate_Id", "dbo.CalendarTemplates", "Id");
            AddForeignKey("dbo.StockActiveOnHands", "StockOnHandSyncId", "dbo.StockOnHandSyncs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StockProductRequests", "StockOnHandSyncId", "dbo.StockOnHandSyncs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StockProductRequestProductDetails", "StockProductRequestRuleId", "dbo.StockProductRequestRules", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CharValues", "CharTypeId", "dbo.CharTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StoreActivePriceLists", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StockProducts", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Stocks", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BasketItems", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PrincipalPermissions", "Permission_Id", "dbo.Permissions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PrincipalPermissions", "Principal_Id", "dbo.Principals", "Id", cascadeDelete: true);
        }
    }
}
