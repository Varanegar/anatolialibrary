using System;

namespace TrackingMap.Common.ViewModel
{
    public class GoodByValueReportFilter : GoodReportFilter
    {
        
        public int? FromRequestCount { set; get; }
        public int? ToRequestCount { set; get; }

        public int? FromFactorCount { set; get; }
        public int? ToFactorCount { set; get; }

        public int? FromRejectCount { set; get; }
        public int? ToRejectCount { set; get; }

        public int? FromSaleItemCount { set; get; }
        public int? ToSaleItemCount { set; get; }

        public int? FromRejectItemCount { set; get; }
        public int? ToRejectItemCount { set; get; }

        public decimal? FromSaleAmount { set; get; }
        public decimal? ToSaleAmount { set; get; }

        public decimal? FromRejectAmount { set; get; }
        public decimal? ToRejectAmount { set; get; }

        public float? FromSaleQty { set; get; }
        public float? ToSaleQty { set; get; }
        public float? FromSaleCarton { set; get; }
        public float? ToSaleCarton { set; get; }

        public float? FromRejectQty { set; get; }
        public float? ToRejectQty { set; get; }

        public float? FromRejectCarton { set; get; }
        public float? ToRejectCarton { set; get; }

        public decimal? FromSaleWeight { set; get; }
        public decimal? ToSaleWeight { set; get; }

        public decimal? FromRejectWeight { set; get; }
        public decimal? ToRejectWeight { set; get; }

        public decimal? FromSaleDiscount { set; get; }
        public decimal? ToSaleDiscount { set; get; }

        public decimal? FromRejectDiscount { set; get; }
        public decimal? ToRejectDiscount { set; get; }

        public int? FromBonusCount { set; get; }
        public int? ToBonusCount { set; get; }

        public float? FromBonusCarton { set; get; }
        public float? ToBonusCarton { set; get; }

        public float? FromBonusQty { set; get; }
        public float? ToBonusQty { set; get; }

        public decimal? FromBonusAmount { set; get; }
        public decimal? ToBonusAmount { set; get; }

    }

    public class GoodByValueReportView
    {
        public Guid Id { set; get; }
        public string Desc { set; get; }
        public double Longitude { set; get; }
        public double Latitude { set; get; }
    }
}
