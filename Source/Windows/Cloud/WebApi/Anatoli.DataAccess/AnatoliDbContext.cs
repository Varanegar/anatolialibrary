﻿using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using Anatoli.DataAccess.Configs;
using Anatoli.DataAccess.Models;

namespace Anatoli.DataAccess
{
    public class AnatoliDbContext : DbContext
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
        public DbSet<CharCategory> CharCategories { get; set; }
        public DbSet<CharGroup> CharGroups { get; set; }
        public DbSet<CharGroupTypeInfo> CharGroupTypeInfoes { get; set; }
        public DbSet<CharType> CharTypes { get; set; }
        public DbSet<CharValue> CharValues { get; set; }
        public DbSet<CityRegion> CityRegions { get; set; }
        public DbSet<Clearance> Clearances { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerShipAddress> CustomerShipAddresses { get; set; }
        public DbSet<DeliveryPerson> DeliveryPersons { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBase> ProductBases { get; set; }
        //public DbSet<ProductBaseProductMap> ProductBaseProductMaps { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<ProductRate> ProductRates { get; set; }
        public DbSet<ProductSupplier> ProductSuppliers { get; set; }
        public DbSet<ProductSupplierGuarantee> ProductSupplierGuarantees { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderHistory> PurchaseOrderHistories { get; set; }
        public DbSet<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
        public DbSet<PurchaseOrderPayment> PurchaseOrderPayments { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreAction> StoreActions { get; set; }
        public DbSet<StoreActiveOnHand> StoreActiveOnHands { get; set; }
        public DbSet<StoreActivePriceList> StoreActivePriceLists { get; set; }
        public DbSet<StoreCalendar> StoreCalendars { get; set; }
        public DbSet<StoreCalendarHistory> StoreCalendarHistories { get; set; }
        public DbSet<StoreDeliveryPerson> StoreDeliveryPersons { get; set; }
        public DbSet<StoreValidRegionInfo> StoreValidRegionInfoes { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<PurchaseOrderClearance> PurchaseOrderClearances { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        #endregion

        #region ctors
        static AnatoliDbContext()
        {
            Database.SetInitializer<AnatoliDbContext>(new MyContextInitializer());
        }

        public AnatoliDbContext()
            : base("Name=AnatoliConnectionString")//this is the connection string name
        {
        }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductConfig());
            modelBuilder.Configurations.Add(new BasketConfig());
            modelBuilder.Configurations.Add(new CalendarTemplateConfig());
            modelBuilder.Configurations.Add(new CharTypeConfig());
            modelBuilder.Configurations.Add(new CharValueConfig());
            modelBuilder.Configurations.Add(new CustomerConfig());
            modelBuilder.Configurations.Add(new DeliveryPersonConfig());
            modelBuilder.Configurations.Add(new ProductGroupConfig());
            modelBuilder.Configurations.Add(new PurchaseOrderConfig());
            modelBuilder.Configurations.Add(new StoreCalendarConfig());
            modelBuilder.Configurations.Add(new StoreConfig());
            modelBuilder.Configurations.Add(new StoreValidRegionInfoConfig());
        }
    }
}