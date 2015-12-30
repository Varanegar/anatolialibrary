namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class StoreCalendar : BaseModel
    {
      	public DateTime Date { get; set; }
        public string PDate { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public Nullable<Guid> CalendarTypeValueId { get; set; }
        public string Description { get; set; }
        [ForeignKey("Store")]
        public Guid StoreId { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<StoreCalendarHistory> StoreCalendarHistories { get; set; }
    }
}
