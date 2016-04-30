using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace TrackingMap.Common.ViewModel
{
    public class GoodReportCustomerFilter
    {
        public Guid ClientId { set; get; }
        public List<Guid> AreaIds { set; get; }
    }



    public class GoodReportFilter
    {
        public Guid ClientId { set; get; }

        public bool ChangeFilter { set; get; }
        public List<Guid> AreaIds { set; get; }
        public int Type { set; get; }
        public string FromDate { set; get; }
        public string ToDate { set; get; }
        public string SaleOffice { set; get; }
        public string Header { set; get; }
        public string Seller { set; get; }
        public string CustomerClass { set; get; }
        public string CustomerActivity { set; get; }
        public string CustomerDegree { set; get; }
        public string GoodGroup { set; get; }
        public string DynamicGroup { set; get; }
        public string Good { set; get; }
        public string CommercialName { set; get; }
        public int? DayCount { set; get; }
        public bool RequestCount { set; get; }
        public bool FactorCount { set; get; }
        public bool RejectCount { set; get; }
        public bool SaleItemCount { set; get; }
        public bool RejectItemCount { set; get; }
        public bool SaleQty { set; get; }
        public bool RejectQty { set; get; }
        public bool SaleAmount { set; get; }
        public bool RejectAmount { set; get; }
        public bool SaleCarton { set; get; }
        public bool RejectCarton { set; get; }
        public bool SaleWeight { set; get; }
        public bool RejectWeight { set; get; }
        public bool SaleDiscount { set; get; }
        public bool RejectDiscount { set; get; }
        public bool BonusCount { set; get; }
        public bool BonusQty { set; get; }
        public bool BonusCarton { set; get; }
        public int DefaultField { set; get; }

    }
    public class GoodReportView
    {
        public double? Latitude { set; get; }
        public double? Longitude { set; get; }
        public string Title { set; get; }

        public int CustRef { set; get; }
        public DbGeometry CPoint { set; get; }	                                                                                                                                                                                                                                                           
        public int? OrderCount { set; get; }
        public int? SaleCount { set; get; }
        public int? RetSaleCount { set; get; }
        public int? SaleItemCount { set; get; }
        public int? RetSaleItemCount { set; get; }
        public double? SaleQty { set; get; }
        public double? SaleCarton { set; get; }
        public double? RetSaleQty { set; get; }
        public double? RetSaleCarton { set; get; }
        public decimal? SaleAmount { set; get; }
        public decimal? RetSaleAmount { set; get; }
        public double? SaleWeight { set; get; }
        public double? RetSaleWeight { set; get; }
        public decimal? SaleDiscount { set; get; }
        public decimal? RetSaleDiscount { set; get; }
        public int? SalePrizeCount { set; get; }
        public double? PrizeQty { set; get; }
        public double? PrizeCarton { set; get; }
        
    }

    public class GoodReportMapView
    {
        public Guid Id { get; set; }
        public string JDesc { get; set; }
        public string Lable { get; set; }
        public string Title { get; set; }
        
        public bool IsLeaf { get; set; }
    }

}
