using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.ViewModels.VnGisModels;
using Thunderstruck;

namespace Anatoli.DMC.DataAccess.DataAdapter
{
    public class DMCGisReportAdapter : DMCBaseAdapter
    {
                #region ctor

        private static DMCGisReportAdapter instance = null;

        public static DMCGisReportAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DMCGisReportAdapter();
                }
                return instance;
            }
        }

        private DMCGisReportAdapter()
        {
        }

        #endregion

        #region method

        public List<ReportListViewModel> LoadReportList(string reportName)
        {
            using (var context = GetDataContext(Transaction.No))
            {
                var list =
                    context.All<ReportListViewModel>("SELECT	ReportFileName, ReportTitle " +
                                    "FROM GisReportList " +
                                    "WHERE ( [ReportName] = '" + reportName + "') "

                    ).ToList();
                return list;
            }
        }
        #endregion





    }
}
