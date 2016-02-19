namespace Anatoli.ViewModels.StoreModels
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class StoreCalendarViewModel : BaseViewModel
    {
        public static string StoreOpenTime = "635126C3-D648-4575-A27C-F96C595CDAC5";
        public static string StoreActivedeliveryTime = "9CED6F7E-D08E-40D7-94BF-A6950EE23915";

      	public DateTime Date { get; set; }
        public string PDate { get; set; }
        public string FromTimeString { get; set; }
        public string ToTimeString { get; set; }
        public TimeSpan FromTime { 
            get {
                if (FromTimeString.Length == 5)
                {
                    var ts = TimeSpan.ParseExact(FromTimeString, @"h\:m",
                                 CultureInfo.InvariantCulture);
                    return ts;
                }
                else
                {
                    var ts = TimeSpan.ParseExact(FromTimeString, @"h\:m\:s",
                                 CultureInfo.InvariantCulture);
                    return ts;
                }
            }
        }
        public TimeSpan ToTime
        {
            get
            {
                if (ToTimeString.Length == 5)
                {
                    var ts = TimeSpan.ParseExact(ToTimeString, @"h\:m",
                                 CultureInfo.InvariantCulture);
                    return ts;
                }
                else
                {
                    var ts = TimeSpan.ParseExact(ToTimeString, @"h\:m\:s",
                                 CultureInfo.InvariantCulture);
                    return ts;
                }
            }
        }
        public Guid CalendarTypeValueId { get; set; }
        public string Description { get; set; }
    }
}
