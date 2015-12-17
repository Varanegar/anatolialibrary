using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.ViewModels.Product
{
    public class StoreViewModel : BaseViewModel
    {
        public int StoreCode { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public long Lat { get; set; }
        public long Lng { get; set; }
        public bool HasDelivery { get; set; }
        public Guid GradeValueId { get; set; }
        public Guid StoreTemplateId { get; set; }
        public bool HasCourier { get; set; }
        public bool SupportAppOrder { get; set; }
        public bool SupportWebOrder { get; set; }
        public bool SupportCallCenterOrder { get; set; }
    }
}
