using System.Collections.Generic;
using Anatoli.SDS.DataAccess.DataAdapter;
using Anatoli.SDS.DataAccess.DataAdapter.Gis;
using Anatoli.SDS.ViewModels.GisReport;

namespace Anatoli.SDS.Business.Domain.Gis
{
    public class SDSProductReportDomain
    {
        #region Methods

        public List<SDSProductReportViewModel> ReloadReportData(SDSProductReportFilterModel filter)
        {
            return  SDSProductReportAdapter.Instance.ReloadReportData(filter);
        }

        #endregion

    }
}
