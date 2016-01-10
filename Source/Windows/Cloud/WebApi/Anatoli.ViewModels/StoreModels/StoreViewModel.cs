using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StoreModels
{
    public class StoreViewModel : BaseViewModel
    {
        public int centerId { get; set; }
        public int storeCode { get; set; }
        public string storeName { get; set; }
        public string address { get; set; }
        public long lat { get; set; }
        public long lng { get; set; }
        public bool hasDelivery { get; set; }
        public Guid gradeValueId { get; set; }
        public Guid storeTemplateId { get; set; }
        public bool hasCourier { get; set; }
        public bool supportAppOrder { get; set; }
        public bool supportWebOrder { get; set; }
        public bool supportCallCenterOrder { get; set; }

        public List<StoreCalendarViewModel> StoreCalendar { get; set; }
        public List<CityRegionViewModel> StoreValidRegionInfo { get; set; }
    }
}
