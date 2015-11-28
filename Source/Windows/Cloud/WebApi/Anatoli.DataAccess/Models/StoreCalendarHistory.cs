namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class StoreCalendarHistory : BaseModel
    {
        //public Guid StoreCalendarHistoryId { get; set; }
        //public Nullable<Guid> CanledarTemplateId { get; set; }
        //public Nullable<Guid> StoreCalendarId { get; set; }
        public Nullable<Guid> ApplyBy { get; set; }
        public Nullable<DateTime> ApplyDate { get; set; }
        //public Nullable<int> ID { get; set; }
    
        public virtual CalendarTemplate CalendarTemplate { get; set; }
        public virtual StoreCalendar StoreCalendar { get; set; }
    }
}
