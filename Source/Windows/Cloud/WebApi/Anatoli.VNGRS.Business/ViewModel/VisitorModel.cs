using System;
using System.Collections.Generic;
using TrackingMap.Common.ViewModel;


namespace TrackingMap.Common.ViewModel
{


    public class VisitorPathCondition
    {
        public VisitorPathCondition()
        {
            VisitorIds = new List<Guid>();

        }
        public List<Guid> VisitorIds { get; set; }
        public string Date { get; set; }
        public bool DailyPath { get; set; }
        public bool VisitorPath { get; set; }
    }

    public class VisitorMarkerCondition
    {
        public VisitorMarkerCondition()
        {
            VisitorIds = new List<Guid>();
        }
        public List<Guid> VisitorIds { get; set; }
        public string Date { get; set; }
        public bool Order { get; set; }
        public bool LackOrder { get; set; }
        public bool LackVisit { get; set; }
        public bool StopWithoutCustomer { get; set; }
        public bool StopWithoutActivity { get; set; }
    }

}