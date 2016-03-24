using System;
using Anatoli.ViewModels.User;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.StoreModels;

namespace Anatoli.ViewModels
{
    public class StoreRequestModel : BaseRequestModel
    {
        public List<StoreActivePriceListViewModel> storeActivePriceListData { get; set; }
        public List<StoreActiveOnhandViewModel> storeActiveOnhandData { get; set; }
        public List<StoreCalendarViewModel> storeCalendarData { get; set; }
        public List<StoreViewModel> storeData { get; set; }
        public Guid storeId { get; set; }
    }
}
