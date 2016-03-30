using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.Route
{
    public class RegionAreaPointViewModel : BaseViewModel
    {
        public double Longitude { set; get; }
        public double Latitude { set; get; }
        public int Priority { set; get; }
        public Guid RegionAreaId { get; set; }


    }
}
