namespace Anatoli.ViewModels.StoreModels
{
    using System;
    using System.Collections.Generic;

    public class StoreCalendarViewModel : BaseViewModel
    {
      	public DateTime Date { get; set; }
        public string PDate { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public Guid CalendarTypeValueId { get; set; }
        public string Description { get; set; }
    }
}
