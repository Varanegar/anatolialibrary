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
    public class RastakCityRegionAdapter : RastakBaseAdapter
    {
        private static RastakCityRegionAdapter instance = null;
        public static RastakCityRegionAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RastakCityRegionAdapter();
                }
                return instance;
            }
        }
        RastakCityRegionAdapter() { }

        public List<CityRegionViewModel> GetAllCityRegions(DateTime lastUpload)
        {
            try
            {
                List<CityRegionViewModel> productGroup = new List<CityRegionViewModel>();
                using (var context = new DataContext())
                {
                    var data = context.All<CityRegionViewModel>(RastakDBQuery.Instance.GetCityRegion());
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
