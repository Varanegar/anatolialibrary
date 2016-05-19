using System.Collections.Generic;
using Anatoli.SDS.DataAccess.DataAdapter;
using Anatoli.SDS.DataAccess.DataAdapter.Gis;
using Anatoli.ViewModels.CommonModels;

namespace Anatoli.SDS.Business.Domain.Gis
{
    public class SDSProductReportFilterDomain
    {
        public List<SelectListItemViewModel> GetComboData(string tblName, string textName, string valueName)
        {
            return SDSProductReportFilterAdapter.Instance.GetComboData(tblName, textName, valueName);
        }
        public List<SelectListItemViewModel> GetAutoCompleteData(string tblName, string textName, string valueName, string searchTerm)
        {
            return SDSProductReportFilterAdapter.Instance.GetAutoCompleteData(tblName, textName, valueName, searchTerm);
        }
    }
}
