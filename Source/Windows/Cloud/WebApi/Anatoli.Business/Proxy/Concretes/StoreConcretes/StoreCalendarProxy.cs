using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.ViewModels.StoreModels;

namespace Anatoli.Business.Proxy.Concretes
{
    public class StoreCalendarProxy : AnatoliProxy<StoreCalendar, StoreCalendarViewModel>, IAnatoliProxy<StoreCalendar, StoreCalendarViewModel>
    {

        public override StoreCalendarViewModel Convert(StoreCalendar data)
        {
            return new StoreCalendarViewModel()
                {
                    ID = data.Number_ID,
                    UniqueId = data.Id,

                    PDate = data.PDate,
                    Date = data.Date,
                    FromTimeString = data.FromTime.ToString(),
                    ToTimeString = data.ToTime.ToString(),
                    Description = data.Description,
                };
        }

        public override StoreCalendar ReverseConvert(StoreCalendarViewModel data)
        {
            return new StoreCalendar()
                {
                    Number_ID = data.ID,
                    Id = data.UniqueId,

                    PDate = data.PDate,
                    Date = data.Date,
                    FromTime = data.FromTime,
                    ToTime = data.ToTime,
                    Description = data.Description,
                };
        }
    }
}
