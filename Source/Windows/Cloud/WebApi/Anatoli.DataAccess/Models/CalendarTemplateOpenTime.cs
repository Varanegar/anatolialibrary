namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class CalendarTemplateOpenTime : BaseModel
    {
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public Nullable<int> WeekDay { get; set; }
        public Nullable<Guid> CalendarTypeValueId { get; set; }
        //public Nullable<Guid> CalendarTemplateId { get; set; }
        //public Guid CalendarTemplateOpenTimeId { get; set; }
    
        public virtual CalendarTemplate CalendarTemplate { get; set; }
    }
}
