using Anatoli.Framework.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Store
{
    public class StoreDataModel : BaseDataModel
    {

        public int centerId { get; set; }
        public int storeCode { get; set; }
        public string storeName { get; set; }
        //public string address { get; set; }
        //public long lat { get; set; }
        //public long lng { get; set; }
        //public bool hasDelivery { get; set; }
        //public Guid gradeValueId { get; set; }
        //public Guid storeTemplateId { get; set; }
        //public bool hasCourier { get; set; }
        //public bool supportAppOrder { get; set; }
        //public bool supportWebOrder { get; set; }
        //public bool supportCallCenterOrder { get; set; }

        //public List<StoreCalendarViewModel> StoreCalendar { get; set; }
        //public List<CityRegionViewModel> StoreValidRegionInfo { get; set; }

        public string store_name { get; set; }
        public string store_address { get; set; }
        public string store_zone { get; set; }
        public string store_city { get; set; }
        public string store_province { get; set; }
        public int store_id { get; set; }
        public int selected { get; set; }
        public string store_tel { get; set; }
        public string location { get; set; }
        public float distance { get; set; }
    }
}
