namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;    
    public class CalendarTemplateHoliday : BaseModel
    {
        public Nullable<DateTime> Date { get; set; }
        public string PDate { get; set; }
        public Nullable<TimeSpan> FromTime { get; set; }
        public Nullable<TimeSpan> ToTime { get; set; }
        //public Guid CalendarTemplateHolidayId { get; set; }
        //public Nullable<Guid> CalendarTemplateId { get; set; }
    
        public virtual CalendarTemplate CalendarTemplate { get; set; }
    }
}
