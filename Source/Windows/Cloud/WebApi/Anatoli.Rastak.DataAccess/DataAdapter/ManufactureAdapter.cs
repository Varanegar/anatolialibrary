using Anatoli.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;
using Anatoli.Rastak.DataAccess.Helpers;

namespace Anatoli.Rastak.DataAccess.DataAdapter
{
    public class ManufactureAdapter : BaseAdapter
    {
        private static ManufactureAdapter instance = null;
        public static ManufactureAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManufactureAdapter();
                }
                return instance;
            }
        }
        ManufactureAdapter() { }
        public  List<ManufactureViewModel> GetAllManufactures(DateTime lastUpload)
        {
            try
            {
                List<ManufactureViewModel> manufacture = new List<ManufactureViewModel>();
                using (var context = new DataContext())
                {
                    string where = "";
                    if (lastUpload != DateTime.MinValue) where = " where ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                    var data = context.All<ManufactureViewModel>(DBQuery.Instance.GetManufacture());
                    manufacture = data.ToList();
                }
                return manufacture;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                return null;
            }
        }

    }
}
