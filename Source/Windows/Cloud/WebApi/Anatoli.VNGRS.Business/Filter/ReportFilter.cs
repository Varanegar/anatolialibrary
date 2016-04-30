using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingMap.Service.Filter
{
    public class ReportFilter
    {

        public bool Order { set; get; }
        public bool Factor { set; get; } 
        public bool NotOrdered { set; get; }
        public bool Customer { set; get; }
        public bool NotVisit { set; get; }
        public bool Machin { set; get; }
        public bool MachinPath { set; get; }
        public bool Visitor { set; get; }
        public bool VisitorPath { set; get; }
        public bool Limited { set; get; }
        public bool Road { set; get; }
        public string FromDate { set; get; }
        public string ToDate { set; get; }
        public string FromTime { set; get; }
        public string ToTime { set; get; }
        public int UserGroupId { set; get; }
        public int UserId { set; get; }
        public int VisitorGroupId { set; get; }
        public int VisitorId { set; get; }
        public int CustomerGroupId { set; get; }
        public int CustomerId { set; get; }
        public int GoodGroupId { set; get; }
        public int GoodId { set; get; }
        public int DriverId { set; get; }
        public int DistributerId { set; get; }
        public int FromRoadId { set; get; }
        public int ToRoadId { set; get; }
        public string DistributArea { set; get; }
        public string Area { set; get; }

    }
}
