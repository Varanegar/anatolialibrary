using Anatoli.DMC.DataAccess.DataAdapter;
using Anatoli.ViewModels.VnGisModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DMC.Business.Domain
{
    public class DMCGisReportDomain
    {
        public List<ReportListViewModel> LoadReportList(string reportName)
        {
            return DMCGisReportAdapter.Instance.LoadReportList(reportName);
        }

    }
}
