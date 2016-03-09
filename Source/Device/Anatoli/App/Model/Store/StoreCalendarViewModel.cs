using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Store
{
    public class StoreCalendarViewModel : BaseViewModel
    {
        public static string StoreOpenTime = "E4A73D47-8AC7-41D1-8EEA-21EDFBA90424";
        public static string StoreActivedeliveryTime = "D5C5E5BF-9235-48D8-B026-B7EB8DB14100";
        public DateTime Date { get; set; }
        public string PDate { get; set; }
        public string FromTimeString { get; set; }
        public string ToTimeString { get; set; }
        public TimeSpan FromTime
        {
            get
            {
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
        string _storeId;
        public string StoreId { get { return _storeId == null ? null : _storeId.ToUpper(); } set { _storeId = value == null ? null : value.ToUpper(); } }
        public string CalendarTypeValueId { get; set; }
        public string Description { get; set; }
    }
}
