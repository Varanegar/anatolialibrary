using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class CalendarTemplateConfig : EntityTypeConfiguration<CalendarTemplate>
    {
        public CalendarTemplateConfig()
        {
            this.HasMany<CalendarTemplateHoliday>(cr => cr.CalendarTemplateHolidays)
                .WithOptional(svr => svr.CalendarTemplate)
                .WillCascadeOnDelete(true);

            this.HasMany<CalendarTemplateOpenTime>(cr => cr.CalendarTemplateOpenTimes)
                .WithOptional(svr => svr.CalendarTemplate)
                .WillCascadeOnDelete(true);

            this.HasMany<StoreCalendarHistory>(cr => cr.StoreCalendarHistories)
                .WithOptional(svr => svr.CalendarTemplate)
                .WillCascadeOnDelete(false); 

        }
    }
}
