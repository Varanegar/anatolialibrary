using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DMC.ViewModels.Report
{
    public class DMCFinanceReportFilterModel
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
        public string UnSoldGoodGroup { set; get; }
        public string UnSoldGood { set; get; }

        public int? DayCount { set; get; }

        public bool OpenFactorCount { set; get; }
        public bool OpenFactorAmount { set; get; }
        public bool OpenFactorDay { set; get; }
        public bool RejectChequeCount { set; get; }
        public bool RejectChequeAmount { set; get; }
        public bool InprocessChequeCount { set; get; }
        public bool InprocessChequeAmount { set; get; }
        public bool FirstCredit { set; get; }
        public bool RemainedCredit { set; get; }
        public bool FirstDebitCredit { set; get; }
        public bool RemainedDebitCredit { set; get; }
        public int DefaultField { set; get; }

    }
}
