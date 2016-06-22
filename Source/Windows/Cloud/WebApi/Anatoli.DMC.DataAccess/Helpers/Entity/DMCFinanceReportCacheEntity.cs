using System;

namespace Anatoli.DMC.DataAccess.Helpers.Entity
{
    public class DMCFinanceReportCacheEntity
    {
        //  public DbGeometry CPoint { set; get; }
        public Guid UniqueId { get; set; }
        public double? Latitude { set; get; }
        public double? Longitude { set; get; }
        public Guid ClientId { set; get; }
        public string Desc { set; get; }
        public int? OpenFactorCount { set; get; }
        public decimal? OpenFactorAmount { set; get; }
        public decimal? OpenFactorDay { set; get; }
        public int? RejectChequeCount { set; get; }
        public decimal? RejectChequeAmount { set; get; }
        public int? InprocessChequeCount { set; get; }
        public decimal? InprocessChequeAmount { set; get; }
        public decimal? FirstCredit { set; get; }
        public decimal? RemainedCredit { set; get; }
        public decimal? FirstDebitCredit { set; get; }
        public decimal? RemainedDebitCredit { set; get; }
        public int? IntId { get; set; }

        public DMCFinanceReportCacheEntity()
        {
        }
    }


}
