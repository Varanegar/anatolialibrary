namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic; 
       
    public class CalendarTemplate : BaseModel
    {       
        public string CalendarTemplateName { get; set; }
        //public Guid CalendarTemplateId { get; set; }
    
        public virtual ICollection<CalendarTemplateHoliday> CalendarTemplateHolidays { get; set; }
        public virtual ICollection<CalendarTemplateOpenTime> CalendarTemplateOpenTimes { get; set; }
        public virtual ICollection<StoreCalendarHistory> StoreCalendarHistories { get; set; }
    }
}
