using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Store
{
    public class StoreUpdateModel : BaseDataModel
    {
        public int centerId { get; set; }
        public int storeCode { get; set; }
        public string storeName { get; set; }
        public string address { get; set; }
        public string Phone { get; set; }
        public long lat { get; set; }
        public long lng { get; set; }
        public bool hasDelivery { get; set; }
        public string gradeValueId { get; set; }
        public string storeTemplateId { get; set; }
        public bool hasCourier { get; set; }
        public bool supportAppOrder { get; set; }
        public bool supportWebOrder { get; set; }
        public bool supportCallCenterOrder { get; set; }

        //public List<StoreCalendarViewModel> StoreCalendar { get; set; }
        //public List<CityRegionViewModel> StoreValidRegionInfo { get; set; }
    }
}
