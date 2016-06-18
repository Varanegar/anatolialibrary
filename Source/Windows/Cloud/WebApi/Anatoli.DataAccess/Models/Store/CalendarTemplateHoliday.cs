namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;    
    public class CalendarTemplateHoliday : AnatoliBaseModel
    {
        public Nullable<DateTime> Date { get; set; }
        [StringLength(10)]
        public string PDate { get; set; }
        public Nullable<TimeSpan> FromTime { get; set; }
        public Nullable<TimeSpan> ToTime { get; set; }
    
        public virtual CalendarTemplate CalendarTemplate { get; set; }
    }
}
