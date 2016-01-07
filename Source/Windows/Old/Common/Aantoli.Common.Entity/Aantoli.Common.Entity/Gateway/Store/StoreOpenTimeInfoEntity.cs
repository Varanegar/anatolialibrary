using Aantoli.Framework.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Store
{
    public class StoreOpenTimeInfoEntity : BaseEntity
    {
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public DateTime GDate { get; set; }
        public string PDate { get; set; }
        public string Description { get; set; }
        public bool IsHoliday { get; set; }
    }
}
