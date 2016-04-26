﻿using System.Data.Entity;
using Anatoli.DataAccess.Configs;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Anatoli.DataAccess.Models.PersonnelAcitvity;
using Anatoli.DataAccess.Models.Route;

namespace Anatoli.DataAccess
{
    public class AnatoliDbContext : IdentityDbContext<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        #region Properties
        public DbSet<BaseType> BaseTypes { get; set; }
        public DbSet<BaseValue> BaseValues { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<BasketNote> BasketNotes { get; set; }
        public DbSet<CalendarTemplate> CalendarTemplates { get; set; }
        public DbSet<CalendarTemplateHoliday> CalendarTemplateHolidays { get; set; }
        public DbSet<CalendarTemplateOpenTime> CalendarTemplateOpenTimes { get; set; }
        public DbSet<CharGroup> CharGroups { get; set; }
        public DbSet<CharType> CharTypes { get; set; }
        public DbSet<CharValue> CharValues { get; set; }
        public DbSet<CityRegion> CityRegions { get; set; }
        public DbSet<Clearance> Clearances { get; set; }
        public DbSet<CustomerArea> CustomerAreas { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerShipAddress> CustomerShipAddresses { get; set; }
        public DbSet<DeliveryPerson> DeliveryPersons { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<CompanyOrgChart> CompanyOrgCharts { get; set; }
        public DbSet<CompanyCenter> CompanyCenters { get; set; }
        public DbSet<RegionArea> RegionAreas { get; set; }
        public DbSet<RegionAreaLevelType> RegionAreaLevelTypes { get; set; }
        public DbSet<FiscalYear> FiscalYears { get; set; }
        public DbSet<IncompletePurchaseOrder> IncompletePurchaseOrders { get; set; }
        public DbSet<IncompletePurchaseOrderLineItem> IncompletePurchaseOrderLineItems { get; set; }
        public DbSet<MainProductGroup> MainProductGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PersonnelDailyActivityCommentType> PersonnelDailyActivityCommentTypes { get; set; }
        public DbSet<PersonnelDailyActivityDayArea> PersonnelDailyActivityDayAreas { get; set; }
        public DbSet<PersonnelDailyActivityEvent> PersonnelDailyActivityEvents { get; set; }
        public DbSet<PersonnelDailyActivityEventComment> PersonnelDailyActivityEventComments { get; set; }
        public DbSet<PersonnelDailyActivityEventType> PersonnelDailyActivityEventTypes { get; set; }
        public DbSet<PersonnelDailyActivityPoint> PersonnelDailyActivityPoints { get; set; }
        public DbSet<PersonnelDailyActivityVisitType> PersonnelDailyActivityVisitTypes { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<ProductRate> ProductRates { get; set; }
        public DbSet<ProductSupplierGuarantee> ProductSupplierGuarantees { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<ProductTag> ProductTag { get; set; }
        public DbSet<ProductTagValue> ProductTagValue { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderStatusHistory> PurchaseOrderHistories { get; set; }
        public DbSet<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
        public DbSet<PurchaseOrderPayment> PurchaseOrderPayments { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreAction> StoreActions { get; set; }
        public DbSet<StoreActiveOnhand> StoreActiveOnhands { get; set; }
        public DbSet<StoreActivePriceList> StoreActivePriceLists { get; set; }
        public DbSet<StoreCalendar> StoreCalendars { get; set; }
        public DbSet<StoreCalendarHistory> StoreCalendarHistories { get; set; }
        public DbSet<StoreDeliveryPerson> StoreDeliveryPersons { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<PurchaseOrderClearance> PurchaseOrderClearances { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<ItemImage> Images { get; set; }
        public DbSet<ReorderCalcType> ReorderCalcTypes { get; set; }
        public DbSet<RegionAreaPoint> RegionAreaPoints { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockActiveOnHand> StockActiveOnHands { get; set; }
        public DbSet<StockHistoryOnHand> StockHistoryOnHands { get; set; }
        public DbSet<StockOnHandSync> StockOnHandSyncs { get; set; }
        public DbSet<StockProduct> StockProducts { get; set; }
        public DbSet<StockProductRequest> StockProductRequests { get; set; }
        public DbSet<StockProductRequestProduct> StockProductRequestProducts { get; set; }
        public DbSet<StockProductRequestProductDetail> StockProductRequestProductDetails { get; set; }
        public DbSet<StockProductRequestRule> StockProductRequestRules { get; set; }
        public DbSet<StockProductRequestRuleType> StockProductRequestRuleTypes { get; set; }
        public DbSet<StockProductRequestRuleCalcType> StockProductRequestRuleCalcTypes { get; set; }
        public DbSet<StockProductRequestStatus> StockProductRequestStatuses { get; set; }
        public DbSet<StockProductRequestType> StockProductRequestTypes { get; set; }
        public DbSet<StockProductRequestSupplyType> StockProductRequestSupplyTypes { get; set; }
        public DbSet<StockType> StockTypes { get; set; }
        public DbSet<CompanyPersonnel> CompanyPersonnels { get; set; }
        public DbSet<CompanyPersonnelRoute> CompanyPersonnelRoutes { get; set; }


        #region Identity
        public DbSet<Company> Companies { get; set; }
        public DbSet<ApplicationOwner> ApplicationOwners { get; set; }
        public DbSet<DataOwner> DataOwners { get; set; }
        public DbSet<DataOwnerCenter> DataOwnerCenters { get; set; }
        public DbSet<AnatoliAccount> AnatoliAccounts { get; set; }
        public DbSet<AnatoliContact> AnatoliContacts { get; set; }
        public DbSet<AnatoliContactType> AnatoliContactTypes { get; set; }
        public DbSet<AnatoliPlace> AnatoliPlaces { get; set; }
        public DbSet<AnatoliRegion> AnatoliRegions { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<IdentityUserRole> UserRoles { get; set; }
        public DbSet<Principal> Principals { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionAction> PermissionActions { get; set; }
        public DbSet<ApplicationModuleResource> ApplicationModuleResources { get; set; }
        public DbSet<ApplicationModule> ApplicationModules { get; set; }
        public DbSet<PrincipalPermission> PrincipalPermissions { get; set; }
        #endregion

        #endregion

        #region ctors

        public AnatoliDbContext()
            : base("Name=AnatoliConnectionString")
        {
        }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BasketConfig());
            modelBuilder.Configurations.Add(new CalendarTemplateConfig());
            modelBuilder.Configurations.Add(new CharGroupConfig());
            modelBuilder.Configurations.Add(new CharTypeConfig());
            modelBuilder.Configurations.Add(new CharValueConfig());
            modelBuilder.Configurations.Add(new CityRegionConfig());
            modelBuilder.Configurations.Add(new CompanyConfig());
            modelBuilder.Configurations.Add(new CustomerConfig());
            modelBuilder.Configurations.Add(new CustomerShipAddressConfig());
            modelBuilder.Configurations.Add(new DeliveryPersonConfig());
            modelBuilder.Configurations.Add(new CompanyCenterConfig());
            modelBuilder.Configurations.Add(new RegionAreaConfig());
            modelBuilder.Configurations.Add(new RegionAreaLevelTypeConfig());
            modelBuilder.Configurations.Add(new FiscalYearConfig());
            modelBuilder.Configurations.Add(new IncompletePurchaseOrderConfig());
            modelBuilder.Configurations.Add(new MainProductGroupConfig());
            modelBuilder.Configurations.Add(new ManufactureConfig());
            modelBuilder.Configurations.Add(new ProductConfig());
            modelBuilder.Configurations.Add(new ProductTagConfig());
            modelBuilder.Configurations.Add(new ProductGroupConfig());
            modelBuilder.Configurations.Add(new ProductTypeConfig());
            modelBuilder.Configurations.Add(new PurchaseOrderConfig());
            modelBuilder.Configurations.Add(new ReorderCalcTypeConfig());
            modelBuilder.Configurations.Add(new StockConfig());
            modelBuilder.Configurations.Add(new StockProductConfig());
            modelBuilder.Configurations.Add(new StockProductRequestConfig());
            modelBuilder.Configurations.Add(new StockProductRequestProductConfig());
            modelBuilder.Configurations.Add(new StockProductRequestRuleConfig());
            modelBuilder.Configurations.Add(new StockProductRequestRuleTypeConfig());
            modelBuilder.Configurations.Add(new StockProductRequestStatusConfig());
            modelBuilder.Configurations.Add(new StockProductRequestTypeSupplyConfig());
            modelBuilder.Configurations.Add(new StockProductRequestTypeConfig());
            modelBuilder.Configurations.Add(new StockOnHandSyncConfig());
            modelBuilder.Configurations.Add(new StoreCalendarConfig());
            modelBuilder.Configurations.Add(new StoreConfig());



            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new PrincipalConfig());
            modelBuilder.Configurations.Add(new GroupConfig());
           // modelBuilder.Configurations.Add(new RoleConfig());
            modelBuilder.Configurations.Add(new PrincipalPermissionConfig());

            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        public static AnatoliDbContext Create()
        {
            return new AnatoliDbContext();
        }
    }
}
