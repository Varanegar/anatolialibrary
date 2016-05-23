using System.Collections.Generic;
using System.Linq;
using Anatoli.ViewModels.CommonModels;
using Thunderstruck;

namespace Anatoli.SDS.DataAccess.DataAdapter.Gis
{
    public class SDSProductReportFilterAdapter : SDSBaseAdapter
    {

        #region ctor
        private static SDSProductReportFilterAdapter instance = null;
        public static SDSProductReportFilterAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SDSProductReportFilterAdapter();
                }
                return instance;
            }
        }
        private SDSProductReportFilterAdapter() { }
        #endregion

        #region method
        public List<SelectListItemViewModel> GetComboData(string tblName, string textName, string valueName)
        {
            using (var context =  GetDataContext(Transaction.No))
            {

                var result =
                    context.All<SelectListItemViewModel>("SELECT "+ valueName +" as intId, " +
                                                         textName+" as Title " +
                                          "FROM "  + tblName
                        ).ToList();

                return result;
            }
        }

        public List<SelectListItemViewModel> GetAutoCompleteData(string tblName, string textName, string valueName, string searchTerm)
        {
            using (var context = GetDataContext(Transaction.No))
            {

                var result =
                    context.All<SelectListItemViewModel>("SELECT " + valueName + " as intId, " +
                                                         textName + " as Title " +
                                          "FROM " + tblName+
                                          " WHERE " + textName + " like '%" + searchTerm + "%'").ToList();
                return result;
            }
        }
        #endregion

    }
}
