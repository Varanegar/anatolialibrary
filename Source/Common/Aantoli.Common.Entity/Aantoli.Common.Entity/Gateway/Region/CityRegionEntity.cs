using Aantoli.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Region
{
    public class CityRegionEntity : BaseEntity
    {
        public Guid CityId { get; set; }
        public Guid ZoneId { get; set; }
        public Guid ProvinceId { get; set; }
    }
}
