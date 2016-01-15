namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetStringLength : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockProductRequests", "ReorderCalcTypeId", "dbo.ReorderCalcTypes");
            RenameColumn(table: "dbo.Products", name: "MainSuppliereId", newName: "MainSupplierId");
            RenameIndex(table: "dbo.Products", name: "IX_MainSuppliereId", newName: "IX_MainSupplierId");
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
            
            AddColumn("dbo.StockProductRequestRules", "Qty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.StockProductRequestRules", "SupplierId", c => c.Guid());
            AddColumn("dbo.StockProductRequestRules", "ReorderCalcTypeId", c => c.Guid(nullable: false));
            AddColumn("dbo.StockProductRequestRules", "StockProductRequestRuleTypeId", c => c.Guid(nullable: false));
            AlterColumn("dbo.BankAccounts", "BankAccountNo", c => c.String(maxLength: 50));
            AlterColumn("dbo.BankAccounts", "BankAccountName", c => c.String(maxLength: 200));
            AlterColumn("dbo.BaseTypes", "BaseTypeDesc", c => c.String(maxLength: 500));
            AlterColumn("dbo.BaseTypes", "BaseTypeName", c => c.String(maxLength: 100));
            AlterColumn("dbo.BaseValues", "BaseValueName", c => c.String(maxLength: 200));
            AlterColumn("dbo.BasketItems", "Comment", c => c.String(maxLength: 500));
            AlterColumn("dbo.Baskets", "BasketName", c => c.String(maxLength: 200));
            AlterColumn("dbo.BasketNotes", "Comment", c => c.String(maxLength: 500));
            AlterColumn("dbo.BasketNotes", "FullText", c => c.String(maxLength: 500));
            AlterColumn("dbo.BasketNotes", "DuePDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.Customers", "CustomerName", c => c.String(maxLength: 200));
            AlterColumn("dbo.Customers", "Phone", c => c.String(maxLength: 20));
            AlterColumn("dbo.Customers", "Mobile", c => c.String(maxLength: 20));
            AlterColumn("dbo.Customers", "Email", c => c.String(maxLength: 500));
            AlterColumn("dbo.Customers", "Address", c => c.String(maxLength: 500));
            AlterColumn("dbo.Customers", "PostalCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.Customers", "NationalCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.CustomerShipAddresses", "AddressName", c => c.String(maxLength: 100));
            AlterColumn("dbo.CustomerShipAddresses", "Phone", c => c.String(maxLength: 20));
            AlterColumn("dbo.CustomerShipAddresses", "Mobile", c => c.String(maxLength: 20));
            AlterColumn("dbo.CustomerShipAddresses", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.CustomerShipAddresses", "MainStreet", c => c.String(maxLength: 50));
            AlterColumn("dbo.CustomerShipAddresses", "OtherStreet", c => c.String(maxLength: 50));
            AlterColumn("dbo.CustomerShipAddresses", "PostalCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.Stores", "StoreName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Stores", "Address", c => c.String(maxLength: 200));
            AlterColumn("dbo.StoreActions", "ActionPDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.StoreActions", "ActionDesc", c => c.String(maxLength: 100));
            AlterColumn("dbo.Products", "ProductName", c => c.String(maxLength: 200));
            AlterColumn("dbo.Products", "StoreProductName", c => c.String(maxLength: 200));
            AlterColumn("dbo.Products", "Desctription", c => c.String(maxLength: 500));
            AlterColumn("dbo.CharValues", "CharValueText", c => c.String(maxLength: 200));
            AlterColumn("dbo.CharGroups", "CharGroupName", c => c.String(maxLength: 200));
            AlterColumn("dbo.MainProductGroups", "GroupName", c => c.String(maxLength: 200));
            AlterColumn("dbo.StockProductRequestRules", "StockProductRequestRuleName", c => c.String(maxLength: 200));
            AlterColumn("dbo.StockProductRequestRules", "FromPDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.StockProductRequestRules", "ToPDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.ProductTypes", "ProductTypeName", c => c.String(maxLength: 100));
            AlterColumn("dbo.StockProductRequests", "RequestPDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.StockProductRequests", "Accept1PDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.StockProductRequests", "Accept2PDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.StockProductRequests", "Accept3PDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.StockProductRequests", "SendtoSourceStockDatePDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.StockProductRequests", "SrouceStockRequestNo", c => c.String(maxLength: 50));
            AlterColumn("dbo.StockProductRequests", "TargetStockIssueDatePDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.StockProductRequests", "TargetStockPaperNo", c => c.String(maxLength: 50));
            AlterColumn("dbo.ReorderCalcTypes", "ReorderTypeName", c => c.String(maxLength: 100));
            AlterColumn("dbo.FiscalYears", "FromPdate", c => c.String(maxLength: 10));
            AlterColumn("dbo.FiscalYears", "ToPdate", c => c.String(maxLength: 10));
            AlterColumn("dbo.Stocks", "StockName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Stocks", "Address", c => c.String(maxLength: 200));
            AlterColumn("dbo.StockOnHandSyncs", "SyncPDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.StockTypes", "StockTypeName", c => c.String(maxLength: 100));
            AlterColumn("dbo.StockProductRequestStatus", "StockProductRequestStatusName", c => c.String(maxLength: 100));
            AlterColumn("dbo.StockProductRequestTypes", "StockProductRequestTypeName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Manufactures", "ManufactureName", c => c.String(maxLength: 100));
            AlterColumn("dbo.ProductComments", "CommentByName", c => c.String(maxLength: 100));
            AlterColumn("dbo.ProductComments", "CommentByEmailAddress", c => c.String(maxLength: 50));
            AlterColumn("dbo.ProductGroups", "GroupName", c => c.String(maxLength: 200));
            AlterColumn("dbo.PurchaseOrderLineItems", "Comment", c => c.String(maxLength: 500));
            AlterColumn("dbo.PurchaseOrders", "Comment", c => c.String(maxLength: 500));
            AlterColumn("dbo.PurchaseOrders", "DeliveryPDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.PurchaseOrders", "CancelDesc", c => c.String(maxLength: 100));
            AlterColumn("dbo.PurchaseOrderHistories", "StatusPDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.PurchaseOrderPayments", "PayPDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.StoreCalendars", "PDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.StoreCalendars", "Description", c => c.String(maxLength: 200));
            AlterColumn("dbo.CalendarTemplates", "CalendarTemplateName", c => c.String(maxLength: 200));
            AlterColumn("dbo.CalendarTemplateHolidays", "PDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.DeliveryPersons", "LastName", c => c.String(maxLength: 100));
            AlterColumn("dbo.DeliveryPersons", "FirstName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Clearances", "ClearancePDate", c => c.String(maxLength: 10));
            AlterColumn("dbo.DiscountCodes", "DiscountDesc", c => c.String(maxLength: 200));
            AlterColumn("dbo.ItemImages", "TokenId", c => c.String(maxLength: 50));
            AlterColumn("dbo.ItemImages", "ImageName", c => c.String(maxLength: 100));
            AlterColumn("dbo.ItemImages", "ImageType", c => c.String(maxLength: 50));
            AlterColumn("dbo.ProductSupplierGuarantees", "GuaranteeDesc", c => c.String(maxLength: 200));
            CreateIndex("dbo.StockProductRequestRules", "SupplierId");
            CreateIndex("dbo.StockProductRequestRules", "ReorderCalcTypeId");
            CreateIndex("dbo.StockProductRequestRules", "StockProductRequestRuleTypeId");
            AddForeignKey("dbo.StockProductRequestRules", "ReorderCalcTypeId", "dbo.ReorderCalcTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StockProductRequestRules", "StockProductRequestRuleTypeId", "dbo.StockProductRequestRuleTypes", "Id");
            AddForeignKey("dbo.StockProductRequestRules", "SupplierId", "dbo.Suppliers", "Id");
            AddForeignKey("dbo.StockProductRequests", "ReorderCalcTypeId", "dbo.ReorderCalcTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockProductRequests", "ReorderCalcTypeId", "dbo.ReorderCalcTypes");
            DropForeignKey("dbo.StockProductRequestRules", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.StockProductRequestRules", "StockProductRequestRuleTypeId", "dbo.StockProductRequestRuleTypes");
            DropForeignKey("dbo.StockProductRequestRuleTypes", "PrivateLabelOwner_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestRuleTypes", "LastModifiedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestRuleTypes", "AddedBy_Id", "dbo.Principals");
            DropForeignKey("dbo.StockProductRequestRules", "ReorderCalcTypeId", "dbo.ReorderCalcTypes");
            DropIndex("dbo.StockProductRequestRuleTypes", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.StockProductRequestRuleTypes", new[] { "AddedBy_Id" });
            DropIndex("dbo.StockProductRequestRuleTypes", new[] { "PrivateLabelOwner_Id" });
            DropIndex("dbo.StockProductRequestRules", new[] { "StockProductRequestRuleTypeId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "ReorderCalcTypeId" });
            DropIndex("dbo.StockProductRequestRules", new[] { "SupplierId" });
            AlterColumn("dbo.ProductSupplierGuarantees", "GuaranteeDesc", c => c.String());
            AlterColumn("dbo.ItemImages", "ImageType", c => c.String());
            AlterColumn("dbo.ItemImages", "ImageName", c => c.String());
            AlterColumn("dbo.ItemImages", "TokenId", c => c.String());
            AlterColumn("dbo.DiscountCodes", "DiscountDesc", c => c.String());
            AlterColumn("dbo.Clearances", "ClearancePDate", c => c.String());
            AlterColumn("dbo.DeliveryPersons", "FirstName", c => c.String());
            AlterColumn("dbo.DeliveryPersons", "LastName", c => c.String());
            AlterColumn("dbo.CalendarTemplateHolidays", "PDate", c => c.String());
            AlterColumn("dbo.CalendarTemplates", "CalendarTemplateName", c => c.String());
            AlterColumn("dbo.StoreCalendars", "Description", c => c.String());
            AlterColumn("dbo.StoreCalendars", "PDate", c => c.String());
            AlterColumn("dbo.PurchaseOrderPayments", "PayPDate", c => c.String());
            AlterColumn("dbo.PurchaseOrderHistories", "StatusPDate", c => c.String());
            AlterColumn("dbo.PurchaseOrders", "CancelDesc", c => c.String());
            AlterColumn("dbo.PurchaseOrders", "DeliveryPDate", c => c.String());
            AlterColumn("dbo.PurchaseOrders", "Comment", c => c.String());
            AlterColumn("dbo.PurchaseOrderLineItems", "Comment", c => c.String());
            AlterColumn("dbo.ProductGroups", "GroupName", c => c.String());
            AlterColumn("dbo.ProductComments", "CommentByEmailAddress", c => c.String());
            AlterColumn("dbo.ProductComments", "CommentByName", c => c.String());
            AlterColumn("dbo.Manufactures", "ManufactureName", c => c.String());
            AlterColumn("dbo.StockProductRequestTypes", "StockProductRequestTypeName", c => c.String());
            AlterColumn("dbo.StockProductRequestStatus", "StockProductRequestStatusName", c => c.String());
            AlterColumn("dbo.StockTypes", "StockTypeName", c => c.String());
            AlterColumn("dbo.StockOnHandSyncs", "SyncPDate", c => c.String());
            AlterColumn("dbo.Stocks", "Address", c => c.String());
            AlterColumn("dbo.Stocks", "StockName", c => c.String());
            AlterColumn("dbo.FiscalYears", "ToPdate", c => c.String());
            AlterColumn("dbo.FiscalYears", "FromPdate", c => c.String());
            AlterColumn("dbo.ReorderCalcTypes", "ReorderTypeName", c => c.String());
            AlterColumn("dbo.StockProductRequests", "TargetStockPaperNo", c => c.String());
            AlterColumn("dbo.StockProductRequests", "TargetStockIssueDatePDate", c => c.String());
            AlterColumn("dbo.StockProductRequests", "SrouceStockRequestNo", c => c.String());
            AlterColumn("dbo.StockProductRequests", "SendtoSourceStockDatePDate", c => c.String());
            AlterColumn("dbo.StockProductRequests", "Accept3PDate", c => c.String());
            AlterColumn("dbo.StockProductRequests", "Accept2PDate", c => c.String());
            AlterColumn("dbo.StockProductRequests", "Accept1PDate", c => c.String());
            AlterColumn("dbo.StockProductRequests", "RequestPDate", c => c.String());
            AlterColumn("dbo.ProductTypes", "ProductTypeName", c => c.String());
            AlterColumn("dbo.StockProductRequestRules", "ToPDate", c => c.String());
            AlterColumn("dbo.StockProductRequestRules", "FromPDate", c => c.String());
            AlterColumn("dbo.StockProductRequestRules", "StockProductRequestRuleName", c => c.String());
            AlterColumn("dbo.MainProductGroups", "GroupName", c => c.String());
            AlterColumn("dbo.CharGroups", "CharGroupName", c => c.String());
            AlterColumn("dbo.CharValues", "CharValueText", c => c.String());
            AlterColumn("dbo.Products", "Desctription", c => c.String());
            AlterColumn("dbo.Products", "StoreProductName", c => c.String());
            AlterColumn("dbo.Products", "ProductName", c => c.String());
            AlterColumn("dbo.StoreActions", "ActionDesc", c => c.String());
            AlterColumn("dbo.StoreActions", "ActionPDate", c => c.String());
            AlterColumn("dbo.Stores", "Address", c => c.String());
            AlterColumn("dbo.Stores", "StoreName", c => c.String());
            AlterColumn("dbo.CustomerShipAddresses", "PostalCode", c => c.String());
            AlterColumn("dbo.CustomerShipAddresses", "OtherStreet", c => c.String());
            AlterColumn("dbo.CustomerShipAddresses", "MainStreet", c => c.String());
            AlterColumn("dbo.CustomerShipAddresses", "Email", c => c.String());
            AlterColumn("dbo.CustomerShipAddresses", "Mobile", c => c.Long());
            AlterColumn("dbo.CustomerShipAddresses", "Phone", c => c.String());
            AlterColumn("dbo.CustomerShipAddresses", "AddressName", c => c.String());
            AlterColumn("dbo.Customers", "NationalCode", c => c.String());
            AlterColumn("dbo.Customers", "PostalCode", c => c.String());
            AlterColumn("dbo.Customers", "Address", c => c.String());
            AlterColumn("dbo.Customers", "Email", c => c.String());
            AlterColumn("dbo.Customers", "Mobile", c => c.String());
            AlterColumn("dbo.Customers", "Phone", c => c.String());
            AlterColumn("dbo.Customers", "CustomerName", c => c.String());
            AlterColumn("dbo.BasketNotes", "DuePDate", c => c.String());
            AlterColumn("dbo.BasketNotes", "FullText", c => c.String());
            AlterColumn("dbo.BasketNotes", "Comment", c => c.String());
            AlterColumn("dbo.Baskets", "BasketName", c => c.String());
            AlterColumn("dbo.BasketItems", "Comment", c => c.String());
            AlterColumn("dbo.BaseValues", "BaseValueName", c => c.String());
            AlterColumn("dbo.BaseTypes", "BaseTypeName", c => c.String());
            AlterColumn("dbo.BaseTypes", "BaseTypeDesc", c => c.String());
            AlterColumn("dbo.BankAccounts", "BankAccountName", c => c.String());
            AlterColumn("dbo.BankAccounts", "BankAccountNo", c => c.String());
            DropColumn("dbo.StockProductRequestRules", "StockProductRequestRuleTypeId");
            DropColumn("dbo.StockProductRequestRules", "ReorderCalcTypeId");
            DropColumn("dbo.StockProductRequestRules", "SupplierId");
            DropColumn("dbo.StockProductRequestRules", "Qty");
            DropTable("dbo.StockProductRequestRuleTypes");
            RenameIndex(table: "dbo.Products", name: "IX_MainSupplierId", newName: "IX_MainSuppliereId");
            RenameColumn(table: "dbo.Products", name: "MainSupplierId", newName: "MainSuppliereId");
            AddForeignKey("dbo.StockProductRequests", "ReorderCalcTypeId", "dbo.ReorderCalcTypes", "Id", cascadeDelete: true);
        }
    }
}
