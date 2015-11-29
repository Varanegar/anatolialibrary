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
                .WithRequired(svr => svr.Store);

            this.HasMany<StoreValidRegionInfo>(cr => cr.StoreValidRegionInfoes)
                .WithRequired(svr => svr.Store);

            this.HasMany<StoreActivePriceList>(cr => cr.StoreActivePriceLists)
               .WithRequired(svr => svr.Store);

            this.HasMany<StoreActiveOnHand>(cr => cr.StoreActiveOnHands)
               .WithRequired(svr => svr.Store);

            this.HasMany<StoreAction>(cr => cr.StoreActions)
               .WithRequired(svr => svr.Store);

            this.HasMany<StoreCalendar>(cr => cr.StoreCalendars)
               .WithRequired(svr => svr.Store);
        }
    }
}