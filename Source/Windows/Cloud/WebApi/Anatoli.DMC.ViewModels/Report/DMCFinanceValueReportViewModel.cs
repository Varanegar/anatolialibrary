using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DMC.ViewModels.Report
{
    public class DMCFinanceValueReportViewModel
    {
        public double? Latitude { set; get; }
        public double? Longitude { set; get; }
        public string Title { set; get; }

        public int CustRef { set; get; }
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
    }
}
