using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class StoreConfig : EntityTypeConfiguration<Store>
    {
        public StoreConfig()
        {
            this.HasMany<StoreDeliveryPerson>(cr => cr.StoreDeliveryPersons)
                .WithRequired(svr => svr.Store)
                .WillCascadeOnDelete(true);

            this.HasMany<CityRegion>(s => s.StoreValidRegionInfoes)
                .WithMany(c => c.StoreValidRegionInfoes)
                .Map(cs =>
                {
                    cs.MapLeftKey("StoreId");
                    cs.MapRightKey("CityRegionID");
                    cs.ToTable("StoreValidRegionInfoes");
                });

            this.HasMany<StoreActivePriceList>(cr => cr.StoreActivePriceLists)
                .WithRequired(svr => svr.Store)
                .WillCascadeOnDelete(true);

            this.HasMany<IncompletePurchaseOrder>(cr => cr.IncompletePurchaseOrders)
                .WithRequired(svr => svr.Store)
                .WillCascadeOnDelete(false);

            this.HasMany<Stock>(cr => cr.StoreStocks)
                .WithOptional(svr => svr.Store)
                .WillCascadeOnDelete(false);

            this.HasMany<StoreActiveOnhand>(cr => cr.StoreActiveOnhands)
                .WithRequired(svr => svr.Store)
                .WillCascadeOnDelete(true);

            this.HasMany<StoreAction>(cr => cr.StoreActions)
                .WithRequired(svr => svr.Store)
                .WillCascadeOnDelete(true);

            this.HasMany<StoreCalendar>(cr => cr.StoreCalendars)
                .WithRequired(svr => svr.Store)
                .WillCascadeOnDelete(true);
        }
    }
}