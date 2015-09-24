using Aantoli.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aantoli.Common.Entity.Gateway.Region;

namespace Aantoli.Common.Entity.Gateway.Store
{
    public class StoreEntity : BaseEntity
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
        public Guid StoreStatusTypeId { get; set; }
        public List<StoreOpenTimeInfoEntity> OpenTimeInfoList { get; set; }
        public List<StoreDeliveryTimeInfoEntity> DeliveryTimeInfoList { get; set; }
        public List<CityRegionEntity> CityRegionList { get; set; }
    }
}
