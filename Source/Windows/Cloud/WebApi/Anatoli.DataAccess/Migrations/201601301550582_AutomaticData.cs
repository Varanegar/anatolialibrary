namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutomaticData : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PurchaseOrderHistories", newName: "PurchaseOrderStatusHistories");
            DropForeignKey("dbo.PurchaseOrders", "Basket_Id", "dbo.Baskets");
            DropForeignKey("dbo.PurchaseOrderLineItems", "FinalProduct_Id", "dbo.Products");
            DropForeignKey("dbo.PurchaseOrders", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.PurchaseOrders", "Store_Id", "dbo.Stores");
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "FinalProduct_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "Basket_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "Customer_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "CustomerShipAddress_Id" });
            DropIndex("dbo.PurchaseOrders", new[] { "Store_Id" });
            RenameColumn(table: "dbo.CustomerShipAddresses", name: "Customer_Id", newName: "CustomerId");
            RenameColumn(table: "dbo.PurchaseOrderLineItems", name: "Product_Id", newName: "ProductId");
            RenameColumn(table: "dbo.PurchaseOrderLineItems", name: "FinalProduct_Id", newName: "FinalProductId");
            RenameColumn(table: "dbo.PurchaseOrderLineItems", name: "PurchaseOrder_Id", newName: "PurchaseOrderId");
            RenameColumn(table: "dbo.PurchaseOrders", name: "Customer_Id", newName: "CustomerId");
            RenameColumn(table: "dbo.PurchaseOrders", name: "CustomerShipAddress_Id", newName: "CustomerShipAddressId");
            RenameColumn(table: "dbo.PurchaseOrderStatusHistories", name: "PurchaseOrder_Id", newName: "PurchaseOrderId");
            RenameColumn(table: "dbo.PurchaseOrders", name: "Store_Id", newName: "StoreId");
            RenameIndex(table: "dbo.CustomerShipAddresses", name: "IX_Customer_Id", newName: "IX_CustomerId");
            RenameIndex(table: "dbo.PurchaseOrderLineItems", name: "IX_Product_Id", newName: "IX_ProductId");
            RenameIndex(table: "dbo.PurchaseOrderLineItems", name: "IX_PurchaseOrder_Id", newName: "IX_PurchaseOrderId");
            RenameIndex(table: "dbo.PurchaseOrderStatusHistories", name: "IX_PurchaseOrder_Id", newName: "IX_PurchaseOrderId");
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
            
            AddColumn("dbo.Permissions", "PersianTitle", c => c.String());
            AddColumn("dbo.Permissions", "Parent_Id", c => c.Guid());
            AddColumn("dbo.Customers", "FirstName", c => c.String(maxLength: 200));
            AddColumn("dbo.Customers", "LastName", c => c.String(maxLength: 200));
            AddColumn("dbo.Customers", "MainStreet", c => c.String(maxLength: 500));
            AddColumn("dbo.Customers", "OtherStreet", c => c.String(maxLength: 500));
            AddColumn("dbo.Customers", "DefauleStoreId", c => c.Guid());
            AddColumn("dbo.CustomerShipAddresses", "RegionInfoId", c => c.Guid());
            AddColumn("dbo.CustomerShipAddresses", "RegionLevel1Id", c => c.Guid());
            AddColumn("dbo.CustomerShipAddresses", "RegionLevel2Id", c => c.Guid());
            AddColumn("dbo.CustomerShipAddresses", "RegionLevel3Id", c => c.Guid());
            AddColumn("dbo.CustomerShipAddresses", "RegionLevel4Id", c => c.Guid());
            AddColumn("dbo.CustomerShipAddresses", "Transferee", c => c.String(maxLength: 100));
            AddColumn("dbo.CustomerShipAddresses", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.CustomerShipAddresses", "DefauleStore_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrderLineItems", "TaxAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderLineItems", "ChargeAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderLineItems", "NetAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderLineItems", "FinalTaxAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderLineItems", "FinalChargeAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderLineItems", "FinalNetAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "ActionSourceId", c => c.Guid(nullable: false));
            AddColumn("dbo.PurchaseOrders", "PaymentTypeId", c => c.Guid(nullable: false));
            AddColumn("dbo.PurchaseOrders", "DiscountAmount2", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "TaxAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "ChargeAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "NetAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "DeliveryTypeId", c => c.Guid(nullable: false));
            AddColumn("dbo.PurchaseOrders", "PurchaseOrderStatusId", c => c.Guid(nullable: false));
            AddColumn("dbo.PurchaseOrders", "Discount2FinalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "ChargeFinalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "TaxFinalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "FinalNetAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "CancelReasonId", c => c.Guid());
            AddColumn("dbo.PurchaseOrderStatusHistories", "StatusValueId", c => c.Guid(nullable: false));
            AddColumn("dbo.PurchaseOrderStatusHistories", "Comment", c => c.String(maxLength: 100));
            AlterColumn("dbo.CustomerShipAddresses", "IsDefault", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CustomerShipAddresses", "Lat", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.CustomerShipAddresses", "Lng", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseOrderLineItems", "IsPrize", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PurchaseOrderLineItems", "AllowReplace", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PurchaseOrderLineItems", "FinalIsPrize", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PurchaseOrderLineItems", "FinalProductId", c => c.Guid(nullable: false));
            AlterColumn("dbo.PurchaseOrders", "OtherSub", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseOrders", "IsCancelled", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PurchaseOrders", "CustomerId", c => c.Guid(nullable: false));
            AlterColumn("dbo.PurchaseOrders", "CustomerShipAddressId", c => c.Guid(nullable: false));
            AlterColumn("dbo.PurchaseOrders", "StoreId", c => c.Guid(nullable: false));
            AlterColumn("dbo.PurchaseOrderStatusHistories", "StatusDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Permissions", "Parent_Id");
            CreateIndex("dbo.Customers", "DefauleStoreId");
            CreateIndex("dbo.CustomerShipAddresses", "RegionInfoId");
            CreateIndex("dbo.CustomerShipAddresses", "RegionLevel1Id");
            CreateIndex("dbo.CustomerShipAddresses", "RegionLevel2Id");
            CreateIndex("dbo.CustomerShipAddresses", "RegionLevel3Id");
            CreateIndex("dbo.CustomerShipAddresses", "RegionLevel4Id");
            CreateIndex("dbo.CustomerShipAddresses", "DefauleStore_Id");
            CreateIndex("dbo.PurchaseOrderLineItems", "FinalProductId");
            CreateIndex("dbo.PurchaseOrders", "StoreId");
            CreateIndex("dbo.PurchaseOrders", "CustomerId");
            CreateIndex("dbo.PurchaseOrders", "CustomerShipAddressId");
            AddForeignKey("dbo.Permissions", "Parent_Id", "dbo.Permissions", "Id");
            AddForeignKey("dbo.CustomerShipAddresses", "DefauleStore_Id", "dbo.Stores", "Id");
            AddForeignKey("dbo.CustomerShipAddresses", "RegionInfoId", "dbo.CityRegions", "Id");
            AddForeignKey("dbo.CustomerShipAddresses", "RegionLevel1Id", "dbo.CityRegions", "Id");
            AddForeignKey("dbo.CustomerShipAddresses", "RegionLevel2Id", "dbo.CityRegions", "Id");
            AddForeignKey("dbo.CustomerShipAddresses", "RegionLevel3Id", "dbo.CityRegions", "Id");
            AddForeignKey("dbo.CustomerShipAddresses", "RegionLevel4Id", "dbo.CityRegions", "Id");
            AddForeignKey("dbo.Customers", "DefauleStoreId", "dbo.Stores", "Id");
            AddForeignKey("dbo.PurchaseOrderLineItems", "FinalProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PurchaseOrders", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PurchaseOrders", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
            DropColumn("dbo.Customers", "Address");
            DropColumn("dbo.CustomerShipAddresses", "StateId");
            DropColumn("dbo.CustomerShipAddresses", "CityId");
            DropColumn("dbo.CustomerShipAddresses", "ZoneId");
            DropColumn("dbo.CustomerShipAddresses", "DefauleStoreId");
            DropColumn("dbo.PurchaseOrderLineItems", "Add1");
            DropColumn("dbo.PurchaseOrderLineItems", "Add2");
            DropColumn("dbo.PurchaseOrderLineItems", "FinalAdd1");
            DropColumn("dbo.PurchaseOrderLineItems", "FinalAdd2");
            DropColumn("dbo.PurchaseOrders", "ActionSourceValueId");
            DropColumn("dbo.PurchaseOrders", "PaymentTypeValueId");
            DropColumn("dbo.PurchaseOrders", "Add1Amount");
            DropColumn("dbo.PurchaseOrders", "Add2Amount");
            DropColumn("dbo.PurchaseOrders", "DeliveryTypeValueId");
            DropColumn("dbo.PurchaseOrders", "PurchaseOrderStatusValueId");
            DropColumn("dbo.PurchaseOrders", "Add1FinalAmount");
            DropColumn("dbo.PurchaseOrders", "Add2FinalAmount");
            DropColumn("dbo.PurchaseOrders", "CancelReasonValueId");
            DropColumn("dbo.PurchaseOrders", "Basket_Id");
            DropColumn("dbo.PurchaseOrderStatusHistories", "PurchaseOrderStatusValueId");
            DropColumn("dbo.PurchaseOrderStatusHistories", "StatusTime");
            DropColumn("dbo.PurchaseOrderStatusHistories", "PurchaseOrderStatusDataId");
            DropColumn("dbo.PurchaseOrderStatusHistories", "CreateBy");
            DropColumn("dbo.PurchaseOrderStatusHistories", "CreateDate");

        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.IncompleteOrderLineItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Guid(nullable: false),
                        IncompleteOrderId = c.Guid(nullable: false),
                        Number_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        PrivateLabelOwner_Id = c.Guid(nullable: false),
                        AddedBy_Id = c.Guid(),
                        LastModifiedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IncompleteOrders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StoreId = c.Guid(nullable: false),
                        CityRegionId = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        DeliveryTypeId = c.Guid(nullable: false),
                        PaymentTypeId = c.Guid(),
                        OrderShipAddress = c.String(),
                        Transferee = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 20),
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PurchaseOrderStatusHistories", "CreateDate", c => c.DateTime());
            AddColumn("dbo.PurchaseOrderStatusHistories", "CreateBy", c => c.Guid());
            AddColumn("dbo.PurchaseOrderStatusHistories", "PurchaseOrderStatusDataId", c => c.Int());
            AddColumn("dbo.PurchaseOrderStatusHistories", "StatusTime", c => c.Time(precision: 7));
            AddColumn("dbo.PurchaseOrderStatusHistories", "PurchaseOrderStatusValueId", c => c.Long());
            AddColumn("dbo.PurchaseOrders", "Basket_Id", c => c.Guid());
            AddColumn("dbo.PurchaseOrders", "CancelReasonValueId", c => c.Long());
            AddColumn("dbo.PurchaseOrders", "Add2FinalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "Add1FinalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "PurchaseOrderStatusValueId", c => c.Long(nullable: false));
            AddColumn("dbo.PurchaseOrders", "DeliveryTypeValueId", c => c.Long(nullable: false));
            AddColumn("dbo.PurchaseOrders", "Add2Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "Add1Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "PaymentTypeValueId", c => c.Long(nullable: false));
            AddColumn("dbo.PurchaseOrders", "ActionSourceValueId", c => c.Long(nullable: false));
            AddColumn("dbo.PurchaseOrderLineItems", "FinalAdd2", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderLineItems", "FinalAdd1", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderLineItems", "Add2", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderLineItems", "Add1", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CustomerShipAddresses", "DefauleStoreId", c => c.Int());
            AddColumn("dbo.CustomerShipAddresses", "ZoneId", c => c.Long());
            AddColumn("dbo.CustomerShipAddresses", "CityId", c => c.Long());
            AddColumn("dbo.CustomerShipAddresses", "StateId", c => c.Long());
            AddColumn("dbo.Customers", "Address", c => c.String(maxLength: 500));
            DropForeignKey("dbo.PurchaseOrders", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.PurchaseOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.PurchaseOrderLineItems", "FinalProductId", "dbo.Products");
            DropForeignKey("dbo.Customers", "DefauleStoreId", "dbo.Stores");
            DropForeignKey("dbo.CustomerShipAddresses", "RegionLevel4Id", "dbo.CityRegions");
            DropForeignKey("dbo.CustomerShipAddresses", "RegionLevel3Id", "dbo.CityRegions");
            DropForeignKey("dbo.CustomerShipAddresses", "RegionLevel2Id", "dbo.CityRegions");
            DropForeignKey("dbo.CustomerShipAddresses", "RegionLevel1Id", "dbo.CityRegions");
            DropForeignKey("dbo.CustomerShipAddresses", "RegionInfoId", "dbo.CityRegions");
            DropForeignKey("dbo.CustomerShipAddresses", "DefauleStore_Id", "dbo.Stores");
            DropForeignKey("dbo.UsersStocks", "StockID", "dbo.Stocks");
            DropForeignKey("dbo.UsersStocks", "UserId", "dbo.Users");
            DropForeignKey("dbo.Permissions", "Parent_Id", "dbo.Permissions");
            DropIndex("dbo.UsersStocks", new[] { "StockID" });
            DropIndex("dbo.UsersStocks", new[] { "UserId" });
            DropIndex("dbo.PurchaseOrders", new[] { "CustomerShipAddressId" });
            DropIndex("dbo.PurchaseOrders", new[] { "CustomerId" });
            DropIndex("dbo.PurchaseOrders", new[] { "StoreId" });
            DropIndex("dbo.PurchaseOrderLineItems", new[] { "FinalProductId" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "DefauleStore_Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "RegionLevel4Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "RegionLevel3Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "RegionLevel2Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "RegionLevel1Id" });
            DropIndex("dbo.CustomerShipAddresses", new[] { "RegionInfoId" });
            DropIndex("dbo.Customers", new[] { "DefauleStoreId" });
            DropIndex("dbo.Permissions", new[] { "Parent_Id" });
            AlterColumn("dbo.PurchaseOrderStatusHistories", "StatusDate", c => c.DateTime());
            AlterColumn("dbo.PurchaseOrders", "StoreId", c => c.Guid());
            AlterColumn("dbo.PurchaseOrders", "CustomerShipAddressId", c => c.Guid());
            AlterColumn("dbo.PurchaseOrders", "CustomerId", c => c.Guid());
            AlterColumn("dbo.PurchaseOrders", "IsCancelled", c => c.Byte());
            AlterColumn("dbo.PurchaseOrders", "OtherSub", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseOrderLineItems", "FinalProductId", c => c.Guid());
            AlterColumn("dbo.PurchaseOrderLineItems", "FinalIsPrize", c => c.Byte(nullable: false));
            AlterColumn("dbo.PurchaseOrderLineItems", "AllowReplace", c => c.Byte(nullable: false));
            AlterColumn("dbo.PurchaseOrderLineItems", "IsPrize", c => c.Byte(nullable: false));
            AlterColumn("dbo.CustomerShipAddresses", "Lng", c => c.Long());
            AlterColumn("dbo.CustomerShipAddresses", "Lat", c => c.Long());
            AlterColumn("dbo.CustomerShipAddresses", "IsDefault", c => c.Byte());
            DropColumn("dbo.PurchaseOrderStatusHistories", "Comment");
            DropColumn("dbo.PurchaseOrderStatusHistories", "StatusValueId");
            DropColumn("dbo.PurchaseOrders", "CancelReasonId");
            DropColumn("dbo.PurchaseOrders", "FinalNetAmount");
            DropColumn("dbo.PurchaseOrders", "TaxFinalAmount");
            DropColumn("dbo.PurchaseOrders", "ChargeFinalAmount");
            DropColumn("dbo.PurchaseOrders", "Discount2FinalAmount");
            DropColumn("dbo.PurchaseOrders", "PurchaseOrderStatusId");
            DropColumn("dbo.PurchaseOrders", "DeliveryTypeId");
            DropColumn("dbo.PurchaseOrders", "NetAmount");
            DropColumn("dbo.PurchaseOrders", "ChargeAmount");
            DropColumn("dbo.PurchaseOrders", "TaxAmount");
            DropColumn("dbo.PurchaseOrders", "DiscountAmount2");
            DropColumn("dbo.PurchaseOrders", "PaymentTypeId");
            DropColumn("dbo.PurchaseOrders", "ActionSourceId");
            DropColumn("dbo.PurchaseOrderLineItems", "FinalNetAmount");
            DropColumn("dbo.PurchaseOrderLineItems", "FinalChargeAmount");
            DropColumn("dbo.PurchaseOrderLineItems", "FinalTaxAmount");
            DropColumn("dbo.PurchaseOrderLineItems", "NetAmount");
            DropColumn("dbo.PurchaseOrderLineItems", "ChargeAmount");
            DropColumn("dbo.PurchaseOrderLineItems", "TaxAmount");
            DropColumn("dbo.CustomerShipAddresses", "DefauleStore_Id");
            DropColumn("dbo.CustomerShipAddresses", "IsActive");
            DropColumn("dbo.CustomerShipAddresses", "Transferee");
            DropColumn("dbo.CustomerShipAddresses", "RegionLevel4Id");
            DropColumn("dbo.CustomerShipAddresses", "RegionLevel3Id");
            DropColumn("dbo.CustomerShipAddresses", "RegionLevel2Id");
            DropColumn("dbo.CustomerShipAddresses", "RegionLevel1Id");
            DropColumn("dbo.CustomerShipAddresses", "RegionInfoId");
            DropColumn("dbo.Customers", "DefauleStoreId");
            DropColumn("dbo.Customers", "OtherStreet");
            DropColumn("dbo.Customers", "MainStreet");
            DropColumn("dbo.Customers", "LastName");
            DropColumn("dbo.Customers", "FirstName");
            DropColumn("dbo.Permissions", "Parent_Id");
            DropColumn("dbo.Permissions", "PersianTitle");
            DropTable("dbo.UsersStocks");
            RenameIndex(table: "dbo.PurchaseOrderStatusHistories", name: "IX_PurchaseOrderId", newName: "IX_PurchaseOrder_Id");
            RenameIndex(table: "dbo.PurchaseOrderLineItems", name: "IX_PurchaseOrderId", newName: "IX_PurchaseOrder_Id");
            RenameIndex(table: "dbo.PurchaseOrderLineItems", name: "IX_ProductId", newName: "IX_Product_Id");
            RenameIndex(table: "dbo.CustomerShipAddresses", name: "IX_CustomerId", newName: "IX_Customer_Id");
            RenameColumn(table: "dbo.PurchaseOrders", name: "StoreId", newName: "Store_Id");
            RenameColumn(table: "dbo.PurchaseOrderStatusHistories", name: "PurchaseOrderId", newName: "PurchaseOrder_Id");
            RenameColumn(table: "dbo.PurchaseOrders", name: "CustomerShipAddressId", newName: "CustomerShipAddress_Id");
            RenameColumn(table: "dbo.PurchaseOrders", name: "CustomerId", newName: "Customer_Id");
            RenameColumn(table: "dbo.PurchaseOrderLineItems", name: "PurchaseOrderId", newName: "PurchaseOrder_Id");
            RenameColumn(table: "dbo.PurchaseOrderLineItems", name: "FinalProductId", newName: "FinalProduct_Id");
            RenameColumn(table: "dbo.PurchaseOrderLineItems", name: "ProductId", newName: "Product_Id");
            RenameColumn(table: "dbo.CustomerShipAddresses", name: "CustomerId", newName: "Customer_Id");
            CreateIndex("dbo.PurchaseOrders", "Store_Id");
            CreateIndex("dbo.PurchaseOrders", "CustomerShipAddress_Id");
            CreateIndex("dbo.PurchaseOrders", "Customer_Id");
            CreateIndex("dbo.PurchaseOrders", "Basket_Id");
            CreateIndex("dbo.PurchaseOrderLineItems", "FinalProduct_Id");
            CreateIndex("dbo.IncompleteOrderLineItems", "LastModifiedBy_Id");
            CreateIndex("dbo.IncompleteOrderLineItems", "AddedBy_Id");
            CreateIndex("dbo.IncompleteOrderLineItems", "PrivateLabelOwner_Id");
            CreateIndex("dbo.IncompleteOrderLineItems", "IncompleteOrderId");
            CreateIndex("dbo.IncompleteOrderLineItems", "ProductId");
            CreateIndex("dbo.IncompleteOrders", "LastModifiedBy_Id");
            CreateIndex("dbo.IncompleteOrders", "AddedBy_Id");
            CreateIndex("dbo.IncompleteOrders", "PrivateLabelOwner_Id");
            CreateIndex("dbo.IncompleteOrders", "CustomerId");
            CreateIndex("dbo.IncompleteOrders", "CityRegionId");
            CreateIndex("dbo.IncompleteOrders", "StoreId");
            AddForeignKey("dbo.PurchaseOrders", "Store_Id", "dbo.Stores", "Id");
            AddForeignKey("dbo.PurchaseOrders", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.PurchaseOrderLineItems", "FinalProduct_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.IncompleteOrders", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IncompleteOrders", "PrivateLabelOwner_Id", "dbo.Principals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IncompleteOrders", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.IncompleteOrderLineItems", "IncompleteOrderId", "dbo.IncompleteOrders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IncompleteOrders", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IncompleteOrders", "CityRegionId", "dbo.CityRegions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PurchaseOrders", "Basket_Id", "dbo.Baskets", "Id");
            AddForeignKey("dbo.IncompleteOrderLineItems", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IncompleteOrderLineItems", "PrivateLabelOwner_Id", "dbo.Principals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IncompleteOrderLineItems", "LastModifiedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.IncompleteOrderLineItems", "AddedBy_Id", "dbo.Principals", "Id");
            AddForeignKey("dbo.IncompleteOrders", "AddedBy_Id", "dbo.Principals", "Id");
            RenameTable(name: "dbo.PurchaseOrderStatusHistories", newName: "PurchaseOrderHistories");
        }
    }
}
