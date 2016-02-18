using Anatoli.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;
using Anatoli.Rastak.DataAccess.Helpers;

namespace Anatoli.Rastak.DataAccess.DataAdapter
{
    public class CityRegionAdapter : BaseAdapter
    {
        private static CityRegionAdapter instance = null;
        public static CityRegionAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CityRegionAdapter();
                }
                return instance;
            }
        }
        CityRegionAdapter() { }

        public List<CityRegionViewModel> GetAllCityRegions(DateTime lastUpload)
        {
            try
            {
                List<CityRegionViewModel> productGroup = new List<CityRegionViewModel>();
                using (var context = new DataContext())
                {
                    var data = context.All<CityRegionViewModel>(DBQuery.Instance.GetCityRegion());
                    productGroup = data.ToList();
                }
                return productGroup;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                return null;
            }
        }

    }
}
