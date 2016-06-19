namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class CalendarTemplateOpenTime : AnatoliBaseModel
    {
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public Nullable<int> WeekDay { get; set; }
        public Nullable<Guid> CalendarTypeValueId { get; set; }
    
        public virtual CalendarTemplate CalendarTemplate { get; set; }
    }
}
