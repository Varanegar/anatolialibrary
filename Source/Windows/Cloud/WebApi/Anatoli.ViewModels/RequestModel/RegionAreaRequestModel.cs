using System;
using Anatoli.ViewModels.User;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.Route;

namespace Anatoli.ViewModels
{
    public class RegionAreaRequestModel : BaseRequestModel
    {
        public bool showCust { get; set; }
        public bool showCustRoute { get; set; }
        public bool showCustOtherRoute { get; set; }
        public bool showCustWithOutRoute { get; set; }
        public Guid regionAreaId { get; set; }

        public CustomerAreaViewModel customerAreadata { get; set; }
        public List<RegionAreaPointViewModel> regionAreaPointDataList { get; set; }
    }
}
