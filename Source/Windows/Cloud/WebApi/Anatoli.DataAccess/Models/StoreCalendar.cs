namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class StoreCalendar : BaseModel
    {
      	public Nullable<DateTime> Date { get; set; }
        public string PDate { get; set; }
        public Nullable<TimeSpan> FromTime { get; set; }
        public Nullable<TimeSpan> ToTime { get; set; }
        public Nullable<Guid> CalendarTypeValueId { get; set; }
        //public Guid StoreCalendarId { get; set; }
        public string Description { get; set; }
        //public Nullable<Guid> StoreId { get; set; }
    
        public virtual Store Store { get; set; }
        public virtual ICollection<StoreCalendarHistory> StoreCalendarHistories { get; set; }
    }
}
