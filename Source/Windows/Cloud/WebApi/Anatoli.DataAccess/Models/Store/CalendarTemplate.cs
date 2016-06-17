namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations; 
       
    public class CalendarTemplate : AnatoliBaseModel
    {
        [StringLength(200)]
        public string CalendarTemplateName { get; set; }
    
        public virtual ICollection<CalendarTemplateHoliday> CalendarTemplateHolidays { get; set; }
        public virtual ICollection<CalendarTemplateOpenTime> CalendarTemplateOpenTimes { get; set; }
        public virtual ICollection<StoreCalendarHistory> StoreCalendarHistories { get; set; }
    }
}
