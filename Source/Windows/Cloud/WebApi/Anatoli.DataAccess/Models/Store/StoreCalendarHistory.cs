namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class StoreCalendarHistory : AnatoliBaseModel
    {
        public Nullable<Guid> ApplyBy { get; set; }
        public Nullable<DateTime> ApplyDate { get; set; }
    
        public virtual CalendarTemplate CalendarTemplate { get; set; }
        public virtual StoreCalendar StoreCalendar { get; set; }
    }
}
