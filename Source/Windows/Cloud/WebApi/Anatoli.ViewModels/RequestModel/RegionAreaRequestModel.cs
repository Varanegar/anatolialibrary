using System;
using System.Collections.Generic;
using Anatoli.ViewModels.VnGisModels;

namespace Anatoli.ViewModels.RequestModel
{
    public class RegionAreaRequestModel : BaseRequestModel
    {
        public bool showCust { get; set; }
        public bool showCustRoute { get; set; }
        public bool showCustOtherRoute { get; set; }
        public bool showCustWithOutRoute { get; set; }
        public Guid regionAreaId { get; set; }
        public Guid customerId { get; set; }
        public Guid? regionAreaParentId { get; set; }
        public int regionAreaLevel { get; set; }
      //  public CustomerAreaViewModel customerAreadata { get; set; }
        public List<RegionAreaPointViewModel> regionAreaPointDataList { get; set; }
        public List<CustomerPointViewModel> customerPointDataList { get; set; }

        public List<Guid> regionAreaIds { get; set; }
    }
}
