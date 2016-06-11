using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.ViewModels.VnGisModels;

namespace Anatoli.ViewModels.RequestModel
{
    public class ProductReportRequestModel
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

        //value
        public List<RegionAreaPointViewModel> CustomPoint { set; get; }

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
}
