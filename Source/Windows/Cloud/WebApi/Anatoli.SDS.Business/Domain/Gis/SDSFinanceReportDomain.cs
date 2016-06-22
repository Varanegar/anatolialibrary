using System.Collections.Generic;
using Anatoli.SDS.DataAccess.DataAdapter;
using Anatoli.SDS.DataAccess.DataAdapter.Gis;
using Anatoli.SDS.ViewModels.GisReport;

namespace Anatoli.SDS.Business.Domain.Gis
{
    public class SDSFinanceReportDomain
    {
        #region Methods

        public List<SDSFinanceReportViewModel> ReloadReportData(SDSFinanceReportFilterModel filter)
        {
            return  SDSFinanceReportAdapter.Instance.ReloadReportData(filter);
        }

        #endregion

    }
}
