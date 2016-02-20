using Anatoli.PMC.DataAccess.Helpers;
using Anatoli.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.DataAccess.DataAdapter
{
    public class FiscalYearAdapter : BaseAdapter
    {
        private static FiscalYearAdapter instance = null;
        public static FiscalYearAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FiscalYearAdapter();
                }
                return instance;
            }
        }
        FiscalYearAdapter() { }
        public List<FiscalYearViewModel> GetAllFiscalYear(DateTime lastUpload)
        {
            try
            {
                List<FiscalYearViewModel> storeList = new List<FiscalYearViewModel>();
                using (var context = new DataContext())
                {
                    string where = "";
                    if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    var data = context.All<FiscalYearViewModel>(DBQuery.Instance.GetFiscalYearQuery());
                    storeList = data.ToList();
                }

                return storeList;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                throw ex;
            }
        }
    }
}
