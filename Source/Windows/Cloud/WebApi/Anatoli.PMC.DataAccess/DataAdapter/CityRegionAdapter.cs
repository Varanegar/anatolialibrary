using Anatoli.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;
using Anatoli.PMC.DataAccess.Helpers;

namespace Anatoli.PMC.DataAccess.DataAdapter
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
            List<CityRegionViewModel> productGroup = new List<CityRegionViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<CityRegionViewModel>(DBQuery.Instance.GetCityRegion());
                productGroup = data.ToList();
            }
            return productGroup;
        }

    }
}
