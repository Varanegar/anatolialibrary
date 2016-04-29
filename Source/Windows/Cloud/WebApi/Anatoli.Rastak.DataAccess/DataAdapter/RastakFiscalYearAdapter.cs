using Anatoli.Rastak.DataAccess.Helpers;
using Anatoli.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.Rastak.DataAccess.DataAdapter
{
    public class RastakFiscalYearAdapter : RastakBaseAdapter
    {
        private static RastakFiscalYearAdapter instance = null;
        public static RastakFiscalYearAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RastakFiscalYearAdapter();
                }
                return instance;
            }
        }
        RastakFiscalYearAdapter() { }
        public List<FiscalYearViewModel> GetAllFiscalYear(DateTime lastUpload)
        {
            try
            {
                List<FiscalYearViewModel> storeList = new List<FiscalYearViewModel>();
                using (var context = new DataContext())
                {
                    string where = "";
                    if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    var data = context.All<FiscalYearViewModel>(RastakDBQuery.Instance.GetFiscalYearQuery());
                    storeList = data.ToList();
                }

                return storeList;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                return null;
            }
        }
    }
}
