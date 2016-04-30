using System;

namespace TrackingMap.Common.ViewModel
{
    public class CustomerReportFilter
    {
        public Guid[] AreaIds { set; get; }
        public int Type { set; get; }
        public string FromDate { set; get; }
        public string ToDate { set; get; }
        public Guid? SaleOffice { set; get; }
        public Guid? Header { set; get; }
        public Guid? Seller { set; get; }
        public Guid? CustomerClass { set; get; }
        public Guid? CustomerActivity { set; get; }
        public Guid? CustomerDegree { set; get; }
        public Guid? GoodGroup { set; get; }
        public Guid? DynamicGroup { set; get; }
        public Guid? Good { set; get; }
        public string CommercialName { set; get; }
        public int? DayCount {set; get;}        
        public bool ActiveCustomerCount {set; get;}
        public bool VisitCount {set; get;}
        public bool LackOfVisitCount {set; get;}
        public bool LackOfSaleCount {set; get;}
        public bool NewCustomerCount {set; get;}
        public bool DuringCheck {set; get;}
        public bool RejectCheck {set; get;}
        
    }
    public class CustomerReportView
    {
        public Guid? AreaId { set; get; }
        public string Desc { set; get; }
        public int? ActiveCustomerCount { set; get; }
        public int? VisitCount { set; get; }
        public int? LackOfVisitCount { set; get; }
        public int? LackOfSaleCount { set; get; }
        public int? NewCustomerCount { set; get; }
        public int? DuringCheckCount { set; get; }
        public decimal? DuringCheckPrice { set; get; }
        public int? RejectCheckCount { set; get; }
        public decimal? RejectCheckPrice { set; get; }

    }


}
