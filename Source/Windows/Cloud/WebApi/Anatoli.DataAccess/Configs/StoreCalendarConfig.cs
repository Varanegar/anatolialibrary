using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class StoreCalendarConfig : EntityTypeConfiguration<StoreCalendar>
    {
        public StoreCalendarConfig()
        {
            this.HasMany<StoreCalendarHistory>(cr => cr.StoreCalendarHistories)
                .WithRequired(svr => svr.StoreCalendar)
                .WillCascadeOnDelete(true);
        }
    }
}