using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StoreModels
{
    public class StoreViewModel : BaseViewModel
    {
        public int CenterId { get; set; }
        public int StoreCode { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public long Lat { get; set; }
        public long Lng { get; set; }
        public byte HasDelivery { get; set; }
        public Guid GradeValueId { get; set; }
        public Guid StoreTemplateId { get; set; }
        public byte HasCourier { get; set; }
        public byte SupportAppOrder { get; set; }
        public byte SupportWebOrder { get; set; }
        public byte SupportCallCenterOrder { get; set; }

        public List<StoreCalendarViewModel> StoreCalendar { get; set; }
        public List<CityRegionViewModel> StoreValidRegionInfo { get; set; }
        //public bool IsRemoved { get; set; }

    }
}
