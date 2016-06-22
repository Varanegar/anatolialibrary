using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.DMC.ViewModels.Area;

namespace Anatoli.DMC.ViewModels.Report
{
    public class DMCFinanceValueReportFilterModel : DMCFinanceReportFilterModel
    {
        public List<DMCRegionAreaPointViewModel> CustomPoint { set; get; }
        public int? FromOpenFactorCount { set; get; }
        public int? ToOpenFactorCount { set; get; }
        public decimal? FromOpenFactorAmount { set; get; }
        public decimal? ToOpenFactorAmount { set; get; }
        public int? FromOpenFactorDay { set; get; }
        public int? ToOpenFactorDay { set; get; }
        public int? FromRejectChequeCount { set; get; }
        public int? ToRejectChequeCount { set; get; }
        public decimal? FromRejectChequeAmount { set; get; }
        public decimal? ToRejectChequeAmount { set; get; }
        public int? FromInprocessChequeCoun { set; get; }
        public int? ToInprocessChequeCoun { set; get; }
        public decimal? FromInprocessChequeAmount { set; get; }
        public decimal? ToInprocessChequeAmount { set; get; }
        public decimal? FromFirstCredit { set; get; }
        public decimal? ToFirstCredit { set; get; }
        public decimal? FromRemainedCredit { set; get; }
        public decimal? ToRemainedCredit { set; get; }

        public decimal? FromFirstDebitCredit { set; get; }
        public decimal? ToFirstDebitCredit { set; get; }

        public decimal? FromRemainedDebitCredit { set; get; }
        public decimal? ToRemainedDebitCredit { set; get; }

    }
}
