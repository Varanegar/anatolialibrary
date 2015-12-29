namespace Anatoli.ViewModels.StoreModels
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class StoreCalendarViewModel : BaseViewModel
    {
      	public DateTime Date { get; set; }
        public string PDate { get; set; }
        public string FromTimeString { get; set; }
        public string ToTimeString { get; set; }
        public TimeSpan FromTime { 
            get {
                var ts = TimeSpan.ParseExact(FromTimeString, @"h\:m\:s",
                             CultureInfo.InvariantCulture);
                return ts;
            }
        }
        public TimeSpan ToTime
        {
            get
            {
                var ts = TimeSpan.ParseExact(ToTimeString, @"h\:m\:s",
                             CultureInfo.InvariantCulture);
                return ts;
            }
        }
        public Guid CalendarTypeValueId { get; set; }
        public string Description { get; set; }
    }
}
